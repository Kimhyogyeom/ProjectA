using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform dragCanvas;

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;

    public Transform originalParent;

    void Start()
    {
        dragCanvas = transform.root;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;

        transform.SetParent(dragCanvas);
        transform.SetAsLastSibling();

        canvasGroup.blocksRaycasts = false;

        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == dragCanvas)
        {
            transform.SetParent(originalParent);
            rectTransform.position = originalParent.GetComponent<RectTransform>().position;
        }

        canvasGroup.blocksRaycasts = true;

        transform.localScale = Vector3.one;
    }
}
