using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoardController : MonoBehaviour
{
    public int playerLevel = 1;
    public int playerDeathCount = 0;
    public int turretBreakCount = 0;
    public int creepScore = 0;

    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private TextMeshProUGUI playerDeathCountText;
    [SerializeField] private TextMeshProUGUI turretBreakCountText;
    [SerializeField] private TextMeshProUGUI creepScoreText;


    [SerializeField] private GameObject scoreBoardUI;
    private KeyCode keyCode = KeyCode.Tab;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            playerLevelText.text = playerLevel.ToString();
            playerDeathCountText.text = playerDeathCount.ToString();
            turretBreakCountText.text = turretBreakCount.ToString();
            creepScoreText.text = creepScore.ToString();
            scoreBoardUI.SetActive(true);
        }
        if (Input.GetKeyUp(keyCode))
        {
            scoreBoardUI.SetActive(false);
        }
    }
}
