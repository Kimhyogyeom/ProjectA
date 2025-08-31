using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private FpsDisplay fpsDisplay;
    [SerializeField] private MsDisplay msDisplay;
    [SerializeField] private ElapsedTimeDisplay elapsedTimeDisplay;

    private float timer = 0f;
    private float timeLimit = 0.5f;

    void Update()
    {
        if (fpsDisplay != null && msDisplay != null && elapsedTimeDisplay != null)
        {
            // Elapsed Time
            elapsedTimeDisplay.ElapsedResult();
            
            timer += Time.deltaTime;
            if (timer >= timeLimit)
            {
                // FPS
                fpsDisplay.FpsResult();
                // MS
                msDisplay.MsResult();

                timer = 0f;
            }
        }
    }
}
