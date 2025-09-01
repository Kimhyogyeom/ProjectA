using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Collections;
public class PlayerSkillController : MonoBehaviour
{
    [Serializable]
    public class Skill
    {
        public Image skillImage;
        public TextMeshProUGUI skillText;
        public KeyCode skillKeyCode;
        public float cooldown;

        [HideInInspector] public bool isCoolingDown = false;
        [HideInInspector] public float currentCooldown = 0f;

        public GameObject skillRangeQuadParent;

        public GameObject skillObject;
        [HideInInspector] public Vector3 skillStartPos;
        [HideInInspector] public bool skillIsMoving = false;
        public float skillMoveDistance = 5f;
        public float skillMoveSpeed = 2f;
        [HideInInspector] public Vector3 skillMoveDirection = Vector3.zero;
    }
    [SerializeField] private PlayerMpBar playerMpBar;
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private NavMeshAgent playerNav;
    [SerializeField] private List<Skill> skills;
    private Vector3? skillETargetPos = null;

    [SerializeField] private Skill fireObjectInpomation;

    public bool isSkill01 = false;
    public bool isSkill02 = false;
    public bool isSkill03 = false;
    public bool isSkill04 = false;

    [SerializeField] private MessageController messageController;

    [SerializeField] private GameObject skill03Particle;
    void Start()
    {
        foreach (var skill in skills)
        {
            if (skill.skillImage != null)
                skill.skillImage.fillAmount = 0f;
            if (skill.skillText != null)
                skill.skillText.text = "";
            if (skill.skillRangeQuadParent != null)
                skill.skillRangeQuadParent.gameObject.SetActive(false);

            if (skill.skillObject != null)
            {
                // skill.skillStartPos = skill.skillObject.transform.position;
                skill.skillObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Pause || GameManager.Instance.gameState == GameManager.GameState.Stop)
        {
            return;
        }
        foreach (var skill in skills)
        {
            HandleSkillInput(skill);
            HandleSkillCooldown(skill);
            HandleSkillMovement(skill);
        }
    }

    private void HandleSkillInput(Skill skill)
    {

        if (skill == skills[0] && playerStats.playerMp - playerStats.playerMpQConsumption < 0f)
        {
            // print("Q 마나 부족");
            if (Input.GetKeyDown(KeyCode.Q))
            {
                messageController.MessageSetting("Not enough mana. Q");
            }

            playerMpBar.MpBarLimit(0);
            if (skill.skillRangeQuadParent.activeSelf)
                skill.skillRangeQuadParent.gameObject.SetActive(false);
            return;
        }
        else if (skill == skills[0] && playerStats.playerMp - playerStats.playerMpQConsumption >= 0f)
        {
            playerMpBar.MpBarLimitClear(0);
        }

        if (skill == skills[1] && playerStats.playerMp - playerStats.playerMpWConsumption < 0f)
        {
            // print("W 마나 부족");
            if (Input.GetKeyDown(KeyCode.W))
            {
                messageController.MessageSetting("Not enough mana. W");
            }
            playerMpBar.MpBarLimit(1);
            if (skill.skillRangeQuadParent.activeSelf)
                skill.skillRangeQuadParent.gameObject.SetActive(false);
            return;
        }
        else if (skill == skills[1] && playerStats.playerMp - playerStats.playerMpWConsumption >= 0f)
        {
            playerMpBar.MpBarLimitClear(1);
        }

        if (skill == skills[2] && playerStats.playerMp - playerStats.playerMpEConsumption < 0f)
        {
            // print("E 마나 부족");
            if (Input.GetKeyDown(KeyCode.E))
            {
                messageController.MessageSetting("Not enough mana. E");
            }
            playerMpBar.MpBarLimit(2);
            if (skill.skillRangeQuadParent.activeSelf)
                skill.skillRangeQuadParent.gameObject.SetActive(false);
            return;
        }
        else if (skill == skills[2] && playerStats.playerMp - playerStats.playerMpEConsumption >= 0f)
        {
            playerMpBar.MpBarLimitClear(2);
        }

        if (skill == skills[3] && playerStats.playerMp - playerStats.playerMpRConsumption < 0f)
        {
            // print("R 마나 부족");
            if (Input.GetKeyDown(KeyCode.R))
            {
                messageController.MessageSetting("Not enough mana. R");
            }
            playerMpBar.MpBarLimit(3);
            if (skill.skillRangeQuadParent.activeSelf)
                skill.skillRangeQuadParent.gameObject.SetActive(false);
            return;
        }
        else if (skill == skills[3] && playerStats.playerMp - playerStats.playerMpRConsumption >= 0f)
        {
            playerMpBar.MpBarLimitClear(3);
        }

        if (Input.GetKey(skill.skillKeyCode) && !skill.isCoolingDown)
        {
            if (!skill.skillRangeQuadParent.activeSelf)
                skill.skillRangeQuadParent.gameObject.SetActive(true);

            if (skill == skills[2])
            {
                Plane groundPlane = new Plane(Vector3.up, playerTransform.position);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float enter;

                if (groundPlane.Raycast(ray, out enter))
                {
                    Vector3 hitPoint = ray.GetPoint(enter);

                    Vector3 playerPos = playerTransform.position;
                    playerPos.y = 0;

                    Vector3 targetPos = hitPoint;
                    targetPos.y = 0;

                    Vector3 dir = targetPos - playerPos;

                    // 최대 거리 제한
                    if (dir.magnitude > skill.skillMoveDistance)
                    {
                        dir = dir.normalized * skill.skillMoveDistance;
                        targetPos = playerPos + dir;
                    }

                    if (!skill.skillRangeQuadParent.activeSelf)
                        skill.skillRangeQuadParent.SetActive(true);

                    if (skill.skillObject != null)
                    {
                        skill.skillObject.SetActive(true);
                        skill.skillObject.transform.position = new Vector3(targetPos.x, playerTransform.position.y + 0.01f, targetPos.z);

                        skillETargetPos = skill.skillObject.transform.position;
                    }
                }
            }
            else if (skill == skills[0] || skill == skills[1] || skill == skills[3])
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    Vector3 dir = hit.point - skill.skillRangeQuadParent.transform.position;
                    dir.y = 0;
                    skill.skillRangeQuadParent.transform.rotation = Quaternion.LookRotation(dir);
                }
            }
        }

        if (Input.GetKeyUp(skill.skillKeyCode) && !skill.isCoolingDown)
        {
            if (skill == skills[0])
            {
                playerStats.playerMp -= playerStats.playerMpQConsumption;
                // playerMpBar.MpBarUpdate(playerStats.playerMp, playerStats.playerMaxMp);
                SoundManager.Instance.PlaySfx(SoundManager.Instance.soundDatabase.playerSkill01Clip);
            }
            else if (skill == skills[1])
            {
                playerStats.playerMp -= playerStats.playerMpWConsumption;
                // playerMpBar.MpBarUpdate(playerStats.playerMp, playerStats.playerMaxMp);
                SoundManager.Instance.PlaySfx(SoundManager.Instance.soundDatabase.playerSkill02Clip);
            }
            else if (skill == skills[2])
            {
                playerStats.playerMp -= playerStats.playerMpEConsumption;
                // playerMpBar.MpBarUpdate(playerStats.playerMp, playerStats.playerMaxMp);
                SoundManager.Instance.PlaySfx(SoundManager.Instance.soundDatabase.playerSkill03Clip);
            }
            else if (skill == skills[3])
            {
                playerStats.playerMp -= playerStats.playerMpRConsumption;
                // playerMpBar.MpBarUpdate(playerStats.playerMp, playerStats.playerMaxMp);
                SoundManager.Instance.PlaySfx(SoundManager.Instance.soundDatabase.playerSkill04Clip);
            }

            skill.skillRangeQuadParent.gameObject.SetActive(false);
            skill.isCoolingDown = true;
            skill.currentCooldown = skill.cooldown;

            if (skill == skills[2])
            {
                if (skillETargetPos.HasValue)
                {
                    Vector3 teleportPos = skillETargetPos.Value;
                    teleportPos.y = playerTransform.position.y;

                    Vector3 lookDir = (teleportPos - playerTransform.position);
                    lookDir.y = 0;
                    if (lookDir.sqrMagnitude > 0.01f)
                    {
                        playerTransform.rotation = Quaternion.LookRotation(lookDir.normalized);
                    }

                    if (playerNav != null)
                    {
                        StartCoroutine(SkillEDelay(teleportPos));
                    }
                    else
                    {
                        playerTransform.position = teleportPos;
                    }
                    skillETargetPos = null;
                }

                if (skill.skillObject != null)
                    skill.skillObject.SetActive(false);
            }
            else if (skill == skills[0] || skill == skills[1] || skill == skills[3])
            {
                if (playerTransform != null)
                {
                    Vector3 lookDir = skill.skillRangeQuadParent.transform.forward;
                    lookDir.y = 0;
                    if (lookDir.sqrMagnitude > 0.01f)
                    {
                        playerTransform.rotation = Quaternion.LookRotation(lookDir);
                        skill.skillMoveDirection = lookDir.normalized;
                    }
                }
                if (skill.skillObject != null)
                {

                }
                if (skill != null)
                {
                    if (skill == skills[0] || skill == skills[1])
                    {
                        if (skill == skills[0])
                        {
                            playerAnim.SetBool("IsSkillQ", true);
                        }
                        else if (skill == skills[1])
                        {
                            playerAnim.SetBool("IsSkillW", true);
                        }
                        ObjectGen(skill);
                    }
                    else if (skill == skills[3])
                    {
                        if (skill == skills[3])
                        {
                            StartCoroutine(SkillRDelay(skill));
                        }
                    }
                }
            }
        }
    }
    private IEnumerator SkillEDelay(Vector3 pos)
    {
        isSkill03 = true;
        playerAnim.SetTrigger("IsSkillE");
        skill03Particle.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        playerNav.Warp(pos);

        yield return new WaitForSeconds(0.7f);
        skill03Particle.SetActive(false);
        isSkill03 = false;
    }
    private IEnumerator SkillRDelay(Skill skill)
    {
        isSkill04 = true;
        playerAnim.SetTrigger("IsSkillR");

        yield return new WaitForSeconds(1.0f);
        ObjectGen(skill);

        yield return new WaitForSeconds(1.0f);
        isSkill04 = false;
    }
    private void ObjectGen(Skill skill)
    {
        skill.skillObject.SetActive(true);
        skill.skillIsMoving = true;

        skill.skillObject.transform.position = playerTransform.position + playerTransform.TransformDirection(new Vector3(0f, 0.5f, 0.4f));

        skill.skillMoveDirection = playerTransform.forward.normalized;

        skill.skillStartPos = skill.skillObject.transform.position;

        skill.skillObject.transform.rotation = Quaternion.LookRotation(skill.skillMoveDirection);
    }

    private void HandleSkillCooldown(Skill skill)
    {
        if (skill.isCoolingDown)
        {
            skill.currentCooldown -= Time.deltaTime;

            if (skill.currentCooldown <= 0f)
            {
                skill.isCoolingDown = false;
                skill.currentCooldown = 0f;

                if (skill.skillImage != null)
                    skill.skillImage.fillAmount = 0f;
                if (skill.skillText != null)
                    skill.skillText.text = "";
            }
            else
            {
                if (Input.GetKeyDown(skill.skillKeyCode))
                {
                    messageController.MessageSetting($"Skill not ready. {skill.skillKeyCode}");
                }
                if (skill.skillImage != null)
                    skill.skillImage.fillAmount = skill.currentCooldown / skill.cooldown;
                if (skill.skillText != null)
                    skill.skillText.text = Mathf.Ceil(skill.currentCooldown).ToString();
            }
        }
    }

    private void HandleSkillMovement(Skill skill)
    {
        if (!skill.skillIsMoving || skill.skillObject == null)
            return;
        fireObjectInpomation = skill;
        skill.skillObject.transform.position += skill.skillMoveDirection * skill.skillMoveSpeed * Time.deltaTime;

        float traveledDist = Vector3.Distance(skill.skillObject.transform.position, skill.skillStartPos);

        if (traveledDist >= skill.skillMoveDistance)
        {
            ResetSkillObject();
        }
    }

    public void ResetSkillObject()
    {
        if (fireObjectInpomation.skillObject == null)
            return;

        // skill.skillObject.transform.position = skill.skillStartPos;
        if (fireObjectInpomation.skillObject != null)
        {
            fireObjectInpomation.skillObject.SetActive(false);
        }
        fireObjectInpomation.skillIsMoving = false;
        playerAnim.SetBool("IsSkillQ", false);
        playerAnim.SetBool("IsSkillW", false);
        fireObjectInpomation = null;
    }
}
