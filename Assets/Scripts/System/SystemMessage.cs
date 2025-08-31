using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SystemMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private GameObject systemMessageObj;

    // 0초
    private string messageStep1 = "소환사의 협곡에 오신것을 환영합니다";
    // 30초
    private string messageStep2 = "미니언 생성까지 30초 남았습니다";
    // 60초
    private string messageStep3 = "미니언이 생성되었습니다";

    private byte currentStep = 0;
    [SerializeField] private float messageOffTime = 5.0f;

    public void SystemMessagePopup()
    {
        if (currentStep == 0)
        {
            textMeshProUGUI.text = messageStep1;
            systemMessageObj.SetActive(true);
            currentStep++;
            StartCoroutine(MessageOff());
        }
        else if (currentStep == 1)
        {
            textMeshProUGUI.text = messageStep2;
            systemMessageObj.SetActive(true);
            currentStep++;
            StartCoroutine(MessageOff());
        }
        else if (currentStep == 2)
        {
            GameManager.Instance.gameState = GameManager.GameState.Play;
            textMeshProUGUI.text = messageStep3;
            systemMessageObj.SetActive(true);
            currentStep++;
            StartCoroutine(MessageOff());
        }
    }

    IEnumerator MessageOff()
    {
        yield return new WaitForSeconds(messageOffTime);
        systemMessageObj.SetActive(false);
    }
}
