using UnityEngine;

public class ClickEventDisable : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
