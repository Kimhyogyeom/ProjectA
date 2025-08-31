using TMPro;
using UnityEngine;

public class FpsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;
    private float fpsTime = 0f;

    public void FpsResult()
    {
        fpsTime += (Time.deltaTime - fpsTime) * 0.1f;
        float fps = 1.0f / fpsTime;
        fpsText.text = Mathf.Ceil(fps).ToString() + " FPS";
    }
}
