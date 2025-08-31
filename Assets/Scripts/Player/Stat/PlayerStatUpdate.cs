using UnityEngine;

public class PlayerStatUpdate : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    public void PlayerStatToItemUp(float atk, float hp, float mp)
    {
        playerStats.playerDamage += atk;
        print("playerStats.playerDamage " + playerStats.playerDamage);
        playerStats.playerMaxHp += hp;
        playerStats.playerMaxMp += mp;
    }
    public void PlayerStatToItemDown(float atk, float hp, float mp)
    {
        playerStats.playerDamage -= atk;
        print("playerStats.playerDamage " + playerStats.playerDamage);
        playerStats.playerMaxHp -= hp;
        playerStats.playerMaxMp -= mp;
    }
}
