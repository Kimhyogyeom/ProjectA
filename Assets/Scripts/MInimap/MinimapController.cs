using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinimapController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent playerNav;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private RectTransform minimapRawImage;
    [SerializeField] private RectTransform playerIcon;
    [SerializeField] private RectTransform pointIcon;
    [SerializeField] private GameObject ground;
    [SerializeField] private RectTransform cameraIcon;
    [SerializeField] private Camera mainCamera;
    private float worldMinX = 0f;
    private float worldMaxX = 0f;
    private float worldMinZ = 0f;
    private float worldMaxZ = 0f;

    [SerializeField] private GameObject minionIcon;
    [SerializeField] private GameObject enemyMinionIcon;


    private Dictionary<GameObject, RectTransform> minionIconDict = new Dictionary<GameObject, RectTransform>();


    void Start()
    {
        float groundSizeX = 10f * ground.transform.localScale.x;
        float groundSizeZ = 10f * ground.transform.localScale.z;

        worldMinX = -groundSizeX / 2f;
        worldMaxX = groundSizeX / 2f;
        worldMinZ = -groundSizeZ / 2f;
        worldMaxZ = groundSizeZ / 2f;
    }

    void Update()
    {

        float xNorm = Mathf.InverseLerp(worldMinX, worldMaxX, playerTransform.position.x);
        float zNorm = Mathf.InverseLerp(worldMinZ, worldMaxZ, playerTransform.position.z);

        float uiX = (xNorm - 0.5f) * minimapRawImage.rect.width;
        float uiZ = (zNorm - 0.5f) * minimapRawImage.rect.height;

        playerIcon.anchoredPosition = new Vector2(uiX, uiZ);

        if (pointIcon.gameObject.activeSelf)
        {
            if (!playerNav.pathPending && playerNav.remainingDistance <= playerNav.stoppingDistance)
            {
                pointIcon.gameObject.SetActive(false);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(minimapRawImage, Input.mousePosition, null, out localPoint))
            {
                if (localPoint.x >= -minimapRawImage.rect.width / 2f && localPoint.x <= minimapRawImage.rect.width / 2f &&
                    localPoint.y >= -minimapRawImage.rect.height / 2f && localPoint.y <= minimapRawImage.rect.height / 2f)
                {
                    float clickXNorm = (localPoint.x / minimapRawImage.rect.width) + 0.5f;
                    float clickZNorm = (localPoint.y / minimapRawImage.rect.height) + 0.5f;

                    float targetX = Mathf.Lerp(worldMinX, worldMaxX, clickXNorm);
                    float targetZ = Mathf.Lerp(worldMinZ, worldMaxZ, clickZNorm);

                    playerNav.isStopped = true;
                    playerNav.ResetPath();
                    playerNav.SetDestination(new Vector3(targetX, playerTransform.position.y, targetZ));

                    pointIcon.gameObject.SetActive(true);
                    pointIcon.anchoredPosition = new Vector2(localPoint.x, localPoint.y);
                }
            }
        }
        UpdateMinions();
    }

    void UpdateMinions()
    {
        UpdateCamImage();
        List<GameObject> minions = EnemyManager.Instance.minions;

        foreach (GameObject minion in minions)
        {
            if (minion == null) continue;

            if (!minionIconDict.ContainsKey(minion))
            {
                GameObject prefab = null;
                if (minion.CompareTag("EnemyMinion")) prefab = minionIcon;
                else if (minion.CompareTag("Enumy")) prefab = enemyMinionIcon;

                if (prefab != null)
                {
                    GameObject icon = Instantiate(prefab, minimapRawImage);
                    RectTransform rt = icon.GetComponent<RectTransform>();
                    minionIconDict[minion] = rt;
                }
            }

            if (minionIconDict.TryGetValue(minion, out RectTransform iconRect))
            {
                float xNorm = Mathf.InverseLerp(worldMinX, worldMaxX, minion.transform.position.x);
                float zNorm = Mathf.InverseLerp(worldMinZ, worldMaxZ, minion.transform.position.z);

                float uiX = (xNorm - 0.5f) * minimapRawImage.rect.width;
                float uiZ = (zNorm - 0.5f) * minimapRawImage.rect.height;

                iconRect.anchoredPosition = new Vector2(uiX, uiZ);
            }
        }

        List<GameObject> toRemove = new List<GameObject>();
        foreach (var kvp in minionIconDict)
        {
            if (!minions.Contains(kvp.Key) || kvp.Key == null)
            {
                Destroy(kvp.Value.gameObject);
                toRemove.Add(kvp.Key);
            }
        }
        foreach (var m in toRemove) minionIconDict.Remove(m);
    }
    private void UpdateCamImage()
    {
        if (cameraIcon != null && mainCamera != null)
        {
            float camXNorm = Mathf.InverseLerp(worldMinX, worldMaxX, mainCamera.transform.position.x);
            float camZNorm = Mathf.InverseLerp(worldMinZ, worldMaxZ, mainCamera.transform.position.z);

            float camUIX = (camXNorm - 0.5f) * minimapRawImage.rect.width;
            float camUIZ = (camZNorm - 0.5f) * minimapRawImage.rect.height + 15f;

            cameraIcon.anchoredPosition = new Vector2(camUIX, camUIZ);
        }
    }
}
