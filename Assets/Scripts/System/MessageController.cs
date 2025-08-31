using System.Collections;
using TMPro;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    [SerializeField] private float disableTime = 2.0f;
    [SerializeField] private TextMeshProUGUI systemMessage;
    private Coroutine messageCoroutine;

    public void MessageSetting(string message)
    {
        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);

        systemMessage.text = message;
        messageCoroutine = StartCoroutine(DisableTime());
    }

    private IEnumerator DisableTime()
    {
        yield return new WaitForSeconds(disableTime);
        systemMessage.text = "";
        messageCoroutine = null;
    }
}
