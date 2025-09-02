using UnityEngine;
using UnityEngine.AI;

public class PetAI : MonoBehaviour
{
    [Header("Component Settings")]
    [SerializeField] private NavMeshAgent petAgent;
    [SerializeField] private Animator petAnim;
    [SerializeField] private Transform playerTr;

    [Header("Follow Settings")]
    [SerializeField] private float followDistance = 5f;
    [SerializeField] private float stopDistance = 2f;

    void Start()
    {
        petAgent.stoppingDistance = stopDistance;
    }

    void Update()
    {
        FollowPlayer();
        HandleInput();
        AnimationController();
    }

    private void FollowPlayer()
    {
        float distance = Vector3.Distance(transform.position, playerTr.position);

        if (distance >= followDistance)
        {
            StopAllActions();
            petAgent.SetDestination(playerTr.position);
        }
        else if (distance <= stopDistance)
        {
            petAgent.ResetPath();
        }
    }

    private void HandleInput()
    {
        if (petAgent.velocity.magnitude < 0.05f)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) PlayAction("Action1");
            if (Input.GetKeyDown(KeyCode.Alpha2)) PlayAction("Action2");
            if (Input.GetKeyDown(KeyCode.Alpha3)) PlayAction("Action3");
            if (Input.GetKeyDown(KeyCode.Alpha4)) PlayAction("Action4");
        }
    }

    private void PlayAction(string actionName)
    {
        StopAllActions();

        petAnim.SetBool(actionName, false);
        // Animator 갱신
        petAnim.Update(0f);
        petAnim.SetBool(actionName, true);
    }

    private void AnimationController()
    {
        float speed = petAgent.velocity.magnitude / petAgent.speed;
        petAnim.SetFloat("Speed", speed, 0.1f, Time.deltaTime);
    }

    private void StopAllActions()
    {
        petAnim.SetBool("Action1", false);
        petAnim.SetBool("Action2", false);
        petAnim.SetBool("Action3", false);
        petAnim.SetBool("Action4", false);
    }
}
