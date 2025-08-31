using UnityEngine;
using UnityEngine.UI;

public class GolemHpBar : MonoBehaviour
{
    [SerializeField] private Slider sliderHpBar;

    public void HpVarUpdate(float hp, float maxHp)
    {
        sliderHpBar.value = hp / maxHp;
    }
}
