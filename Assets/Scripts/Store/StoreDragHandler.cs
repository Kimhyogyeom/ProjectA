using UnityEngine;
using UnityEngine.EventSystems;

public class StoreDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private RectTransform storePanel;
    private Vector2 offset;

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            storePanel.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out var localMousePos
        );

        offset = storePanel.localPosition - (Vector3)localMousePos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (storePanel == null) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            storePanel.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out var localMousePos
        );

        storePanel.localPosition = localMousePos + offset;
    }
}
