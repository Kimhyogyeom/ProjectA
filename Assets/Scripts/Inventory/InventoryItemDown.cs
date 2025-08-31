using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventoryItemDown : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    [SerializeField] private Image slotImage;
    [SerializeField] private RectTransform rectTransform;


    public void OnPointerEnter(PointerEventData eventData)
    {

        slotImage.color = Color.blue;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slotImage.color = Color.white;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        Transform draggedItem = eventData.pointerDrag.transform;
        Transform originalSlot = draggedItem.GetComponent<InventoryItemDrag>().originalParent;

        // 슬롯에 기존 아이템이 있으면 교체
        if (transform.childCount > 0)
        {
            Transform existingItem = transform.GetChild(0);
            existingItem.SetParent(originalSlot);
            existingItem.GetComponent<RectTransform>().position = originalSlot.GetComponent<RectTransform>().position;
        }

        // 드래그 아이템을 슬롯에 배치
        draggedItem.SetParent(transform);
        draggedItem.GetComponent<RectTransform>().position = rectTransform.position;
    }

}
