using Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private CinemachineVirtualCamera cinemachineCam;
    [SerializeField] private Camera mainCam;
    [Header("Stting Value")]
    private bool isSpaceKey = false;
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float screenPaddingSize = 10.0f;

    void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Stop) return;
        IsKeySpaceActive();
        MoveController();
    }
    private void IsKeySpaceActive()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSpaceKey = true;
            cinemachineCam.gameObject.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isSpaceKey = false;
            cinemachineCam.gameObject.SetActive(false);
        }
    }
    private void MoveController()
    {
        Vector3 forward = transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = transform.right;
        right.y = 0;
        right.Normalize();

        if (!isSpaceKey)
        {
            float mousePosX = Input.mousePosition.x;
            float mousePosY = Input.mousePosition.y;

            if (mousePosX < screenPaddingSize)
                transform.position -= right * Time.deltaTime * moveSpeed;
            if (mousePosX > Screen.width - screenPaddingSize)
                transform.position += right * Time.deltaTime * moveSpeed;

            if (mousePosY < screenPaddingSize)
                transform.position -= forward * Time.deltaTime * moveSpeed;
            if (mousePosY > Screen.height - screenPaddingSize)
                transform.position += forward * Time.deltaTime * moveSpeed;
        }
    }
}
