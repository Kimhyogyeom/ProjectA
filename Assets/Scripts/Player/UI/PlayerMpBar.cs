using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMpBar : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider playerMpBar;
    [SerializeField] private Slider playerWorldBottomMpBar;

    [Header("Skill")]
    [SerializeField] private Image skillQImage;
    [SerializeField] private Image skillWImage;
    [SerializeField] private Image skillEImage;
    [SerializeField] private Image skillRImage;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI worldMaxMpText;
    [SerializeField] private TextMeshProUGUI worldMpText;
    [SerializeField] private TextMeshProUGUI skillQMpText;
    [SerializeField] private TextMeshProUGUI skillWMpText;
    [SerializeField] private TextMeshProUGUI skillEMpText;
    [SerializeField] private TextMeshProUGUI skillRMpText;


    void Update()
    {
        MpRegenRate();
        MpTextUpdate();
    }
    private void MpTextUpdate()
    {
        worldMaxMpText.text = playerStats.playerMaxMp.ToString("F1");
        worldMpText.text = "/" + playerStats.playerMp.ToString("F1");
        skillQMpText.text = playerStats.playerMpQConsumption.ToString();
        skillWMpText.text = playerStats.playerMpWConsumption.ToString();
        skillEMpText.text = playerStats.playerMpEConsumption.ToString();
        skillRMpText.text = playerStats.playerMpRConsumption.ToString();
    }
    private void MpRegenRate()
    {
        playerStats.playerMp += playerStats.playerManaRegenRate * Time.deltaTime;
        playerStats.playerMp = Mathf.Clamp(playerStats.playerMp, 0f, playerStats.playerMaxMp);


        MpBarUpdate(playerStats.playerMp, playerStats.playerMaxMp);
    }

    public void MpBarUpdate(float mp, float maxMp)
    {
        playerMpBar.value = mp / maxMp;
        playerWorldBottomMpBar.value = mp / maxMp;
    }
    public void MpBarLimit(byte skillNumber)
    {
        if (skillNumber == 0)
        {
            skillQImage.color = Color.blue;
        }
        if (skillNumber == 1)
        {
            skillWImage.color = Color.blue;
        }
        if (skillNumber == 2)
        {
            skillEImage.color = Color.blue;
        }
        if (skillNumber == 3)
        {
            skillRImage.color = Color.blue;
        }
    }
    public void MpBarLimitClear(byte skillNumber)
    {
        if (skillNumber == 0)
        {
            skillQImage.color = Color.white;
        }
        if (skillNumber == 1)
        {
            skillWImage.color = Color.white;
        }
        if (skillNumber == 2)
        {
            skillEImage.color = Color.white;
        }
        if (skillNumber == 3)
        {
            skillRImage.color = Color.white;
        }
    }
}
