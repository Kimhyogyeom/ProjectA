using UnityEngine;

public class GolemStats : MonoBehaviour
{
    public float golemMaxHp = 300f;
    public float golemHp = 0f;

    public float golemDamage = 10f;

    public Collider golemAttackCollider;
    void Start()
    {
        golemHp = golemMaxHp;
    }

    public void AttackColliderOn()
    {
        golemAttackCollider.enabled = true;
    }
    public void AttackColliderOff()
    {
        golemAttackCollider.enabled = false;
    }
}
