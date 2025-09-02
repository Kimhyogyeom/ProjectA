using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] private PlayerDieController playerDieController;
    [SerializeField] private PlayerHpBar playerHpBar;
    [Header("Level")]
    public int playerLevel = 1;

    [Header("Hp")]
    public float playerMaxHp = 100f;
    public float playerHp = 100f;

    [Header("Mp")]
    public float playerMaxMp = 100f;
    public float playerMp = 100f;
    public float playerManaRegenRate = 5.0f;
    public float playerMpQConsumption = 35f;
    public float playerMpWConsumption = 20f;
    public float playerMpEConsumption = 20f;
    public float playerMpRConsumption = 50f;

    [Header("Exp")]
    public float playerMaxExp = 100;
    public float playerExp = 0;

    [Header("Point")]
    public int playerStatPoint = 0;

    [Header("Attack")]
    public float playerDamage = 10f;
    public float playerSkillQDamage = 2f;
    public float playerSkillWDamage = 0f;
    public float playerSkillEDamage = 0f;
    public float playerSkillRDamage = 4f;

    [Header("Setting Value")]
    public float playerAttackSpeed = 3f;

    [Header("Gold")]
    public float playerGold = 0f;

    public void TakeDamage(float damage)
    {
        playerHp -= damage;
        playerHpBar.HpVarUpdate(playerHp, playerMaxHp);
        if (playerHp <= 0)
        {
            playerDieController.PlayerDieOn();
        }
    }
    public void PlayerResurrection()
    {
        playerHp = playerMaxHp;
        playerHpBar.HpVarUpdate(playerHp, playerMaxHp);
    }
}
