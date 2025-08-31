using TMPro;
using UnityEngine;

public class PlayerStatPoint : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private TextMeshProUGUI levelUpText;

    [Header("Skill Q")]
    [SerializeField] private GameObject skillLevelUpQ;
    [SerializeField] private TextMeshProUGUI damageValueQ;
    [SerializeField] private TextMeshProUGUI mpValueQ;
    [SerializeField] private GameObject skillDiscriptionQ;
    private int maxPointQ = 0;

    [Header("Skill W")]
    [SerializeField] private GameObject skillLevelUpW;
    [SerializeField] private TextMeshProUGUI damageValueW;
    [SerializeField] private TextMeshProUGUI mpValueW;
    [SerializeField] private GameObject skillDiscriptionW;
    private int maxPointW = 0;

    [Header("Skill E")]
    [SerializeField] private GameObject skillLevelUpE;
    [SerializeField] private TextMeshProUGUI damageValueE;
    [SerializeField] private TextMeshProUGUI mpValueE;
    [SerializeField] private GameObject skillDiscriptionE;
    private int maxPointE = 0;

    [Header("Skill R")]
    [SerializeField] private GameObject skillLevelUpR;
    [SerializeField] private TextMeshProUGUI damageValueR;
    [SerializeField] private TextMeshProUGUI mpValueR;
    [SerializeField] private GameObject skillDiscriptionR;
    private int maxPointR = 0;

    public void SKillLevelUpBtnPopup()
    {
        levelUpText.gameObject.SetActive(true);
        if (!(maxPointQ >= 5))
        {
            skillLevelUpQ.SetActive(true);
        }
        if (!(maxPointW >= 5))
        {
            skillLevelUpW.SetActive(true);
        }
        if (!(maxPointE >= 5))
        {
            skillLevelUpE.SetActive(true);
        }
        if (!(maxPointR >= 3))
        {
            skillLevelUpR.SetActive(true);
        }
        levelUpText.text = $"Level up! +{playerStats.playerStatPoint}";
    }

    public void LevelUpQ()
    {
        if (playerStats.playerStatPoint > 0)
        {
            playerStats.playerStatPoint -= 1;
            playerStats.playerSkillQDamage += 0.1f;
            playerStats.playerMpQConsumption -= 1;
            levelUpText.text = $"Level up! +{playerStats.playerStatPoint}";
            maxPointQ += 1;
            if (playerStats.playerStatPoint <= 0)
            {
                levelUpText.gameObject.SetActive(false);
                HideAllLevelUpBtn();
            }
        }
    }
    public void ShowSkillQ()
    {
        damageValueQ.text = $"{playerStats.playerDamage * playerStats.playerSkillQDamage:0.0} > {playerStats.playerDamage * (playerStats.playerSkillQDamage + 0.1f):0.0}";
        mpValueQ.text = $"{playerStats.playerMpQConsumption} > {playerStats.playerMpQConsumption - 1}";
        skillDiscriptionQ.SetActive(true);
    }
    // --------
    public void LevelUpW()
    {

        if (playerStats.playerStatPoint > 0)
        {
            playerStats.playerStatPoint -= 1;
            playerStats.playerSkillWDamage += 0.1f;
            playerStats.playerMpWConsumption -= 1;
            levelUpText.text = $"Level up! +{playerStats.playerStatPoint}";
            maxPointW += 1;
            if (playerStats.playerStatPoint <= 0)
            {
                levelUpText.gameObject.SetActive(false);
                HideAllLevelUpBtn();
            }
        }
    }
    public void ShowSkillW()
    {
        damageValueW.text = $"{playerStats.playerDamage * playerStats.playerSkillWDamage:0/0} > {playerStats.playerDamage * (playerStats.playerSkillWDamage + 0.1f):0.0}";
        mpValueW.text = $"{playerStats.playerMpWConsumption} > {playerStats.playerMpWConsumption - 1}";
        skillDiscriptionW.SetActive(true);
    }
    // --------
    public void LevelUpE()
    {

        if (playerStats.playerStatPoint > 0)
        {
            playerStats.playerStatPoint -= 1;
            playerStats.playerSkillEDamage += 0f;
            playerStats.playerMpEConsumption -= 1;
            levelUpText.text = $"Level up! +{playerStats.playerStatPoint}";
            maxPointE += 1;
            if (playerStats.playerStatPoint <= 0)
            {
                levelUpText.gameObject.SetActive(false);
                HideAllLevelUpBtn();
            }
        }
    }
    public void ShowSkillE()
    {
        damageValueE.text = $"{playerStats.playerSkillQDamage} > {playerStats.playerSkillQDamage}";
        mpValueE.text = $"{playerStats.playerMpEConsumption} > {playerStats.playerMpEConsumption - 1}";
        skillDiscriptionE.SetActive(true);
    }
    // --------
    public void LevelUpR()
    {

        if (playerStats.playerStatPoint > 0)
        {
            playerStats.playerStatPoint -= 1;
            playerStats.playerSkillRDamage += 0.1f;
            playerStats.playerMpRConsumption -= 1;
            levelUpText.text = $"Level up! +{playerStats.playerStatPoint}";
            maxPointR += 1;
            if (playerStats.playerStatPoint <= 0)
            {
                levelUpText.gameObject.SetActive(false);
                HideAllLevelUpBtn();
            }
        }
    }
    public void ShowSkillR()
    {
        damageValueR.text = $"{playerStats.playerDamage * playerStats.playerSkillRDamage:0.0} > {playerStats.playerDamage * (playerStats.playerSkillRDamage + 0.1f):0.0}";
        mpValueR.text = $"{playerStats.playerMpRConsumption} > {playerStats.playerMpRConsumption - 1}";
        skillDiscriptionR.SetActive(true);
    }
    // --------

    public void HideAll()
    {
        skillDiscriptionQ.SetActive(false);
        skillDiscriptionW.SetActive(false);
        skillDiscriptionE.SetActive(false);
        skillDiscriptionR.SetActive(false);
    }
    private void HideAllLevelUpBtn()
    {
        skillLevelUpQ.SetActive(false);
        skillLevelUpW.SetActive(false);
        skillLevelUpE.SetActive(false);
        skillLevelUpR.SetActive(false);
        HideAll();
    }
}
