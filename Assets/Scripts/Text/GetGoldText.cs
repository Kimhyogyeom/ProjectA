using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GetGoldText : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameObject[] goldTextObjects;
    [SerializeField] private TextMeshProUGUI[] goldTextObjectTexts;
    [SerializeField] private TextMeshProUGUI currentGold;
    [SerializeField] private float disableTime = 1f;

    public void GetGold(string goldText)
    {
        for (int i = 0; i < goldTextObjects.Length; i++)
        {
            if (goldTextObjects[i].activeSelf == false)
            {
                goldTextObjects[i].SetActive(true);
                goldTextObjectTexts[i].text = $"+G {goldText}";
                currentGold.text = (int.Parse(currentGold.text) + int.Parse(goldText)).ToString();
                playerStats.playerGold = int.Parse(currentGold.text);
                SoundManager.Instance.PlaySfxUI(SoundManager.Instance.soundDatabase.goldGetClip);
                StartCoroutine(getTextDisable(goldTextObjects[i]));
                break;
            }
        }
    }
    private IEnumerator getTextDisable(GameObject goldText)
    {
        yield return new WaitForSeconds(disableTime);
        goldText.SetActive(false);
    }
    public void BuyItemGetGold(int place)
    {
        playerStats.playerGold -= place;
        currentGold.text = playerStats.playerGold.ToString();
    }
    public void SellItemGetGold(int amount)
    {
        playerStats.playerGold += amount;
        currentGold.text = playerStats.playerGold.ToString();
    }
}
