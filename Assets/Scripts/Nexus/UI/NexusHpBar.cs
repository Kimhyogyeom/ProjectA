using UnityEngine;
using UnityEngine.UI;

public class NexusHpBar : MonoBehaviour
{
    [SerializeField] private Slider nexusHpBar;

    public void HpVarUpdate(float hp, float maxHp)
    {
        nexusHpBar.value = hp / maxHp;
    }
}