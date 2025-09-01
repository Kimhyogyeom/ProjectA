using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExpBar : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerStatPoint playerStatPoint;

    [SerializeField] private Slider playerExpBar;

    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private TextMeshProUGUI playerWorldBottomExpBar;

    void Start()
    {
        playerLevelText.text = playerStats.playerLevel.ToString();
        playerWorldBottomExpBar.text = playerStats.playerLevel.ToString();
    }
    public void ExpBarUpdate(float exp)
    {
        playerStats.playerExp += exp;

        while (playerStats.playerExp >= playerStats.playerMaxExp)
        {
            playerStats.playerExp -= playerStats.playerMaxExp;
            playerStats.playerMaxExp *= 2;

            playerStats.playerLevel += 1;
            playerLevelText.text = playerStats.playerLevel.ToString();
            playerWorldBottomExpBar.text = playerStats.playerLevel.ToString();

            playerStats.playerStatPoint += 1;
            playerStats.playerDamage += 1;
            playerStatPoint.SKillLevelUpBtnPopup();
            SoundManager.Instance.PlaySfxUI(SoundManager.Instance.soundDatabase.playerLevelUp);
        }

        playerExpBar.value = playerStats.playerExp / playerStats.playerMaxExp;
    }
}
