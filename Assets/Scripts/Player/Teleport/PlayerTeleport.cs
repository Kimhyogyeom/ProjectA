using UnityEngine;
using UnityEngine.AI;

public class PlayerTeleport : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 returnStartPosition;

    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject returnParticle;
    private bool isReturning = false;

    void Start()
    {
        initialPosition = transform.position;
    }
    void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Pause || GameManager.Instance.gameState == GameManager.GameState.Stop)
        {
            return;
        }
        if (isReturning)
        {
            float moved = Vector3.Distance(returnStartPosition, transform.position);
            if (moved > 0.1f)
            {
                CancelReturn();
            }
        }
        if (Input.GetKeyDown(KeyCode.B) && !isReturning)
        {
            returnStartPosition = transform.position;
            returnParticle.SetActive(true);
            isReturning = true;
            animator.SetTrigger("IsReturn");
        }
    }
    public void OnReturnAnimationEnd()
    {
        if (!isReturning) return;

        returnParticle.SetActive(false);
        agent.Warp(initialPosition);
        isReturning = false;
    }
    private void CancelReturn()
    {
        returnParticle.SetActive(false);
        isReturning = false;
        animator.Play("IdleToRun", 0, 0f);
    }
}
