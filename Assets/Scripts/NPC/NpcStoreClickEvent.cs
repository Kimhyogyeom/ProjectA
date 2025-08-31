using UnityEngine;

public class NpcStoreClickEvent : MonoBehaviour
{
    [SerializeField] private GameObject storePanel;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Animator storeNpcAnimator;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (storePanel.activeSelf == true)
            {
                return;
            }
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, LayerMask.GetMask("NpcStore")))
            {
                storeNpcAnimator.SetTrigger("IsClick");
                storePanel.SetActive(true);
            }
        }
    }
}
