using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Animator playerAnim;

    [Header("Player Setting Value")]
    public bool performMeleeAttack = true;
    private float attackInterval;
    private float nextAttackTime = 0;

    [Header("Enumy Target Setting")]
    public GameObject enumyGameobject;
    public GameObject[] basicAttackObjects;
    private GameObject currentBasicAttackObject;
    [SerializeField] private Vector3 attackSpawnPoint;

    void Update()
    {
        attackInterval = playerStats.playerAttackSpeed / ((500 + playerStats.playerAttackSpeed) * 0.01f);

        if (playerMovement.enumyGameobject != null)
        {
            enumyGameobject = playerMovement.enumyGameobject.gameObject;
        }
        else
        {
            enumyGameobject = null;
        }

        if (enumyGameobject != null && performMeleeAttack && Time.time > nextAttackTime)
        {
            if (Vector3.Distance(transform.position, enumyGameobject.transform.position) <= playerMovement.stopDistance)
            {
                StartCoroutine(PlayerAttackInterval());
            }
        }
    }
    private IEnumerator PlayerAttackInterval()
    {
        performMeleeAttack = false;
        playerAnim.SetBool("IsAttack", true);
        yield return new WaitForSeconds(attackInterval);
        if (!enumyGameobject)
        {
            playerAnim.SetBool("IsAttack", false);
            performMeleeAttack = true;
        }
    }

    public void OnPlayerAttack()
    {
        if (enumyGameobject != null)
        {
            if (playerAnim.GetBool("IsSkillQ") || playerAnim.GetBool("IsSkillW"))
                return;
            GetBasicAttacjObject();

            if (currentBasicAttackObject != null)
            {
                PlayerTargetObject playerTargetObject = currentBasicAttackObject.GetComponent<PlayerTargetObject>();

                if (playerTargetObject != null)
                {
                    playerTargetObject.SetTarget(enumyGameobject.transform);

                }
            }

            nextAttackTime = Time.time + attackInterval;
            performMeleeAttack = true;
            playerAnim.SetBool("IsAttack", false);
        }
    }

    private void GetBasicAttacjObject()
    {
        foreach (var basicAttackObject in basicAttackObjects)
        {
            if (!basicAttackObject.activeSelf)
            {
                basicAttackObject.transform.position = transform.position + attackSpawnPoint;
                currentBasicAttackObject = basicAttackObject;
                basicAttackObject.SetActive(true);
                return;
            }
        }
    }
}
