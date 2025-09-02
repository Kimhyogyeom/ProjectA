using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;

public class PlayerDieController : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private NavMeshAgent playerAgent;
    [SerializeField] private GameObject playerMesh;
    [SerializeField] private GameObject playerCanvas;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private ScoreBoardController scoreBoardController;

    [Header("Respawn Settings")]
    [SerializeField] private Vector3 originPos;
    [SerializeField] private TextMeshProUGUI cooldownText;
    [SerializeField] private GameObject cooldownObj;
    [SerializeField] private Image profileImage;
    [SerializeField] private GameObject dieBackground;

    private void Start()
    {
        originPos = transform.position;
    }

    public void PlayerDieOn()
    {
        GameManager.Instance.gameState = GameManager.GameState.Pause;
        scoreBoardController.playerDeathCount++;
        gameObject.tag = "Untagged";

        playerMesh.SetActive(false);
        playerCanvas.SetActive(false);

        cooldownObj.SetActive(true);
        profileImage.fillAmount = 0;

        playerAgent.Warp(originPos);

        dieBackground.SetActive(true);

        int level = playerStats.playerLevel;
        float waitTime = level * 5f;

        StartCoroutine(ResurrectionCooldown(waitTime));
        SoundManager.Instance.PlaySfxUI(SoundManager.Instance.soundDatabase.playerDieClip);
    }

    private IEnumerator ResurrectionCooldown(float waitTime)
    {
        float elapsed = 0f;

        while (elapsed < waitTime)
        {
            elapsed += Time.deltaTime;

            profileImage.fillAmount = Mathf.Clamp01(elapsed / waitTime);

            cooldownText.text = Mathf.Ceil(waitTime - elapsed).ToString("F0");

            yield return null;
        }
        PlayerResurrection();
    }

    private void PlayerResurrection()
    {
        GameManager.Instance.gameState = GameManager.GameState.Play;
        gameObject.tag = "Player";

        playerMesh.SetActive(true);
        playerCanvas.SetActive(true);
        cooldownObj.SetActive(false);

        dieBackground.SetActive(false);

        playerStats.PlayerResurrection();
        SoundManager.Instance.PlaySfxUI(SoundManager.Instance.soundDatabase.playerResurrectionClip);
    }
}
