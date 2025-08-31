using TMPro;
using UnityEngine;

public class MsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI msText;
    private float msTime = 0f;

    public void MsResult()
    {
        msTime += (Time.deltaTime - msTime) * 0.1f;
        float ms = msTime * 1000;
        msText.text = ms.ToString("F1") + "ms";
    }
}
