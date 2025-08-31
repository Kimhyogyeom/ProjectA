using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private GetGoldText getGoldText;
    private PlayerExpBar playerExpBar;
    [SerializeField] private EnumyHpBar enumyHpBar;

    [Header("Stats")]
    public float enumyMaxHp = 100f;
    public float enumyHp = 100f;
    public float enemyDamage = 10f;
    public float enumyAttackSpeed = 3f;

    [Header("Exp")]
    public float enumyExpResult = 50f;

    [Header("Gold")]
    private float enumyGoldResult = 0;

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerExpBar = playerObj.GetComponent<PlayerExpBar>();
        }

        GameObject goldPool = GameObject.FindWithTag("GoldPool");
        if (goldPool != null)
        {
            getGoldText = goldPool.GetComponent<GetGoldText>();
        }
    }
    public void TakeDamage(float damage)
    {
        enumyGoldResult = Random.Range(10, 20);
        enumyHp -= damage;
        enumyHpBar.HpVarUpdate(enumyHp, enumyMaxHp);
        if (enumyHp <= 0)
        {
            playerExpBar.ExpBarUpdate(enumyExpResult);
            getGoldText.GetGold(enumyGoldResult.ToString());
            EnemyManager.Instance.UnregisterMinion(this.gameObject);
            Destroy(this.gameObject);
        }
    }
    public void TakeDamageTurret(float damage)
    {
        // enumyGoldResult = Random.Range(10, 20);
        enumyHp -= damage;
        enumyHpBar.HpVarUpdate(enumyHp, enumyMaxHp);
        if (enumyHp <= 0)
        {
            // playerExpBar.ExpBarUpdate(enumyExpResult);
            // getGoldText.GetGold(enumyGoldResult.ToString());
            EnemyManager.Instance.UnregisterMinion(this.gameObject);
            Destroy(this.gameObject);
        }
    }
    public void TakeDamageEnemy(float damage)
    {
        // enumyGoldResult = Random.Range(10, 20);
        enumyHp -= damage;
        enumyHpBar.HpVarUpdate(enumyHp, enumyMaxHp);
        if (enumyHp <= 0)
        {
            // playerExpBar.ExpBarUpdate(enumyExpResult);
            // getGoldText.GetGold(enumyGoldResult.ToString());
            EnemyManager.Instance.UnregisterMinion(this.gameObject);
            Destroy(this.gameObject);
        }
    }

}
