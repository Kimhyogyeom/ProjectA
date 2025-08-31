using UnityEngine;
using Cinemachine;

public class CameraZoomController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineCam;
    [SerializeField] private Camera mainCam;
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float minFOV = 20f;
    [SerializeField] private float maxFOV = 80f;

    private float currentFOV = 70f;

    private void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Stop) return;
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            currentFOV -= scroll * zoomSpeed;
            currentFOV = Mathf.Clamp(currentFOV, minFOV, maxFOV);

            cinemachineCam.m_Lens.FieldOfView = currentFOV;
            mainCam.fieldOfView = currentFOV;
        }
    }
}
