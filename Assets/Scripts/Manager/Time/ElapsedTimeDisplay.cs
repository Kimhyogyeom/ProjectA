using TMPro;
using UnityEngine;

public class ElapsedTimeDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI elapsedText;
    private float elapsedTime = 0f;
    private float nextPopupTime = 0f;
    [SerializeField] private SystemMessage systemMessage;
    private int systemMessagCount = 0;
    public void ElapsedResult()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        elapsedText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (nextPopupTime < 90)
        {
            if (elapsedTime >= nextPopupTime)
            {
                systemMessage.SystemMessagePopup();
                nextPopupTime += 30f;
                if (systemMessagCount == 0)
                {
                    SoundManager.Instance.PlaySfxUI(SoundManager.Instance.soundDatabase.systemMessage0Sec, 0.7f);
                }
                else if (systemMessagCount == 1)
                {
                    SoundManager.Instance.PlaySfxUI(SoundManager.Instance.soundDatabase.systemMessage30Sec, 0.7f);
                }
                else if (systemMessagCount == 2)
                {
                    SoundManager.Instance.PlaySfxUI(SoundManager.Instance.soundDatabase.systemMessage60Sec, 0.7f);
                }
                systemMessagCount++;
            }
        }
    }
}
