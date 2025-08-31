using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public List<GameObject> minions = new List<GameObject>();

    void Awake()
    {
        Instance = this;
    }

    public void RegisterMinion(GameObject minion)
    {
        if (!minions.Contains(minion))
            minions.Add(minion);
    }

    public void UnregisterMinion(GameObject minion)
    {
        if (minions.Contains(minion))
            minions.Remove(minion);
    }

    public List<GameObject> GetMinions()
    {
        return minions;
    }
}
