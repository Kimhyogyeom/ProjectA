using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Component")]
    [SerializeField] private Animator playerAnim;
    [SerializeField] private NavMeshAgent playerNav;
    [SerializeField] private PlayerSkillController playerSkillController;

    [Header("Setting Value")]
    [SerializeField] private float playerNavRotateSpeed = 0.05f;
    private float animationSmmothTime = 0.1f;
    private float playerNavRotateVel = 0f;

    [Header("Effect Settings")]
    [SerializeField] private GameObject[] clickQuads;
    [SerializeField] private Vector3 quadOffset = new Vector3(0, 0.1f, 0);

    [Header("Enumy Component")]
    public Transform enumyGameobject;
    public float stopDistance = 0f;
    private Outline outline;
    void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Pause || GameManager.Instance.gameState == GameManager.GameState.Stop)
        {
            return;
        }
        AnimationController();
        MoveController();
    }

    private void AnimationController()
    {
        float speed = playerNav.velocity.magnitude / playerNav.speed;
        playerAnim.SetFloat("Speed", speed, animationSmmothTime, Time.deltaTime);
    }

    private void MoveController()
    {
        if (playerSkillController.isSkill03 || playerSkillController.isSkill04)
        {
            playerNav.isStopped = true;
            playerNav.ResetPath();
            playerNav.velocity = Vector3.zero;
            return;
        }
        if (EventSystem.current.IsPointerOverGameObject())
            return; // UI 클릭이면 이동 무시
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    PlayerNavToMove(hit.point);
                    PlayerRotation(hit.point);
                }
                else if (hit.collider.CompareTag("Enumy"))
                {
                    if (outline != null)
                        outline.enabled = false;
                    outline = hit.transform.GetComponent<Outline>();
                    PlayerNavToEnumy(hit.collider.transform);
                }
                else if (hit.collider.CompareTag("Golem"))
                {
                    if (outline != null)
                        outline.enabled = false;
                    outline = hit.transform.GetComponent<Outline>();
                    PlayerNavToEnumy(hit.collider.transform);
                }
                else if (hit.collider.CompareTag("TurretEnemy"))
                {
                    if (outline != null)
                        outline.enabled = false;
                    outline = hit.transform.GetComponent<Outline>();
                    PlayerNavToEnumy(hit.collider.transform);
                }
                else if (hit.collider.CompareTag("EnemyNexus"))
                {
                    if (outline != null)
                        outline.enabled = false;
                    outline = hit.transform.GetComponent<Outline>();
                    PlayerNavToEnumy(hit.collider.transform);
                }
            }
        }
        if (enumyGameobject != null)
        {
            if (Vector3.Distance(transform.position, enumyGameobject.position) > stopDistance)
            {
                playerNav.SetDestination(enumyGameobject.position);
            }
        }
    }
    private void PlayerNavToMove(Vector3 getHitPoint)
    {
        if (outline != null)
            outline.enabled = false;

        playerNav.SetDestination(getHitPoint);
        playerNav.stoppingDistance = 0f;

        if (enumyGameobject != null)
        {
            enumyGameobject = null;
        }
    }
    private void PlayerRotation(Vector3 getHitPoint)
    {
        Quaternion rotationToLookat = Quaternion.LookRotation(getHitPoint - transform.position);
        float rotationY = Mathf.SmoothDampAngle(
            transform.eulerAngles.y,                    // 현재 y축 회전값
            rotationToLookat.eulerAngles.y,             // 목표 Y축 회전값
            ref playerNavRotateVel,                     // 보정 값
            playerNavRotateSpeed * (Time.deltaTime));   // 회전하는데 걸릴 시간

        transform.eulerAngles = new Vector3(0, rotationY, 0);

        ActivateClickQuad(getHitPoint + quadOffset);
    }
    private void ActivateClickQuad(Vector3 position)
    {
        foreach (var quad in clickQuads)
        {
            if (!quad.activeSelf)
            {
                quad.transform.position = position;
                quad.SetActive(true);
                return;
            }
        }
    }
    private void PlayerNavToEnumy(Transform getEnumyTr)
    {
        if (outline != null)
            outline.enabled = true;

        enumyGameobject = getEnumyTr;
        playerNav.SetDestination(getEnumyTr.transform.position);
        playerNav.stoppingDistance = stopDistance;
        PlayerRotation(getEnumyTr.transform.position);
    }
}
