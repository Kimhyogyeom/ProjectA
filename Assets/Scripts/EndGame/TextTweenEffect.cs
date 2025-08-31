using UnityEngine;
using DG.Tweening;

public class ObjectTweenEffect : MonoBehaviour
{
    [SerializeField] private RectTransform vicObject;
    [SerializeField] private RectTransform defObject;

    private Vector3 startPos = Vector3.zero;
    private Vector3 hiddenPos = new Vector3(0, 1200, 0);

    void Start()
    {
        vicObject.anchoredPosition3D = hiddenPos;
        defObject.anchoredPosition3D = hiddenPos;

        vicObject.gameObject.SetActive(false);
        defObject.gameObject.SetActive(false);
    }

    public void GameVictory()
    {
        vicObject.gameObject.SetActive(true);
        vicObject.anchoredPosition3D = hiddenPos;

        vicObject.DOAnchorPos3D(startPos, 3f)
            .SetEase(Ease.OutBounce);
    }

    public void GameDefeat()
    {
        defObject.gameObject.SetActive(true);
        defObject.anchoredPosition3D = hiddenPos;

        defObject.DOAnchorPos3D(startPos, 3f)
            .SetEase(Ease.OutBounce);
    }
}
