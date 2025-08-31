using TMPro;
using UnityEngine;

public class SkillDiscription : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private PlayerStats playerStats;

    [Header("Skill Q")]
    [SerializeField] private GameObject skillDiscriptionQ;
    [SerializeField] private TextMeshProUGUI damageValueQ;
    [SerializeField] private TextMeshProUGUI mpValueQ;

    [Header("Skill W")]
    [SerializeField] private GameObject skillDiscriptionW;
    [SerializeField] private TextMeshProUGUI damageValueW;
    [SerializeField] private TextMeshProUGUI mpValueW;

    [Header("Skill E")]
    [SerializeField] private GameObject skillDiscriptionE;
    [SerializeField] private TextMeshProUGUI damageValueE;
    [SerializeField] private TextMeshProUGUI mpValueE;

    [Header("Skill R")]
    [SerializeField] private GameObject skillDiscriptionR;
    [SerializeField] private TextMeshProUGUI damageValueR;
    [SerializeField] private TextMeshProUGUI mpValueR;

    private void Start()
    {
        skillDiscriptionQ.SetActive(false);
        skillDiscriptionW.SetActive(false);
        skillDiscriptionE.SetActive(false);
        skillDiscriptionR.SetActive(false);
    }

    public void ShowSkillQ()
    {
        damageValueQ.text = (playerStats.playerDamage * playerStats.playerSkillQDamage).ToString();
        mpValueQ.text = playerStats.playerMpQConsumption.ToString();
        skillDiscriptionQ.SetActive(true);
    }

    public void ShowSkillW()
    {
        damageValueW.text = (playerStats.playerDamage * playerStats.playerSkillWDamage).ToString();
        mpValueW.text = playerStats.playerMpWConsumption.ToString();
        skillDiscriptionW.SetActive(true);
    }

    public void ShowSkillE()
    {
        damageValueE.text = (playerStats.playerDamage * playerStats.playerSkillEDamage).ToString();
        mpValueE.text = playerStats.playerMpEConsumption.ToString();
        skillDiscriptionE.SetActive(true);
    }

    public void ShowSkillR()
    {
        damageValueR.text = (playerStats.playerDamage * playerStats.playerSkillRDamage).ToString();
        mpValueR.text = playerStats.playerMpRConsumption.ToString();
        skillDiscriptionR.SetActive(true);
    }

    public void HideAll()
    {
        skillDiscriptionQ.SetActive(false);
        skillDiscriptionW.SetActive(false);
        skillDiscriptionE.SetActive(false);
        skillDiscriptionR.SetActive(false);
    }
}
