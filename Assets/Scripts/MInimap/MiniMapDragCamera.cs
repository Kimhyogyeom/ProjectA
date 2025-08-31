using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class MiniMapDragCamera : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform minimapRect;
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform ground;
    private Vector2 lastPointerPos;
    private bool dragging = false;
    private Transform originalFollow;
    private CinemachineTransposer transposer;

    void Start()
    {
        if (vcam == null)
        {
            Debug.LogError("카메라 없음");
            return;
        }

        originalFollow = vcam.Follow;
        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(minimapRect, eventData.position, eventData.pressEventCamera))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(minimapRect, eventData.position, eventData.pressEventCamera, out lastPointerPos);
            dragging = true;

            if (vcam != null)
            {
                vcam.Follow = null;
                vcam.gameObject.SetActive(true);
            }

            MoveCameraToClickPosition(eventData);
        }
    }

    private void MoveCameraToClickPosition(PointerEventData eventData)
    {
        Vector2 localPoint;
        Camera cam = eventData.pressEventCamera;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(minimapRect, eventData.position, cam, out localPoint);

        // 0~1 정규화 좌표
        float clickXNorm = (localPoint.x + minimapRect.rect.width / 2f) / minimapRect.rect.width;
        float clickZNorm = (localPoint.y + minimapRect.rect.height / 2f) / minimapRect.rect.height;

        // 월드 좌표 계산
        float groundWidth = 10f * ground.localScale.x;
        float groundHeight = 10f * ground.localScale.z;

        float worldMinX = ground.position.x - groundWidth / 2f;
        float worldMaxX = ground.position.x + groundWidth / 2f;
        float worldMinZ = ground.position.z - groundHeight / 2f;
        float worldMaxZ = ground.position.z + groundHeight / 2f;

        float targetX = Mathf.Lerp(worldMinX, worldMaxX, clickXNorm);
        float targetZ = Mathf.Lerp(worldMinZ, worldMaxZ, clickZNorm);

        // Transposer가 있으면 FollowOffset, 없으면 Transform 직접 이동
        if (transposer != null)
        {
            transposer.m_FollowOffset = new Vector3(targetX, transposer.m_FollowOffset.y, targetZ);
        }
        else
        {
            vcam.transform.position = new Vector3(targetX, vcam.transform.position.y, targetZ);
        }
    }

    // 드래그 중일 때
    public void OnDrag(PointerEventData eventData)
    {
        if (!dragging || vcam == null) return;

        // 마우스 위치에 따라 카메라 절대 이동
        MoveCameraToClickPosition(eventData);
    }

    // 마우스 뗄 때
    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;

        if (vcam != null && originalFollow != null)
        {
            vcam.Follow = originalFollow; // 원래 Follow 복귀
            vcam.gameObject.SetActive(false);
        }
    }
}
