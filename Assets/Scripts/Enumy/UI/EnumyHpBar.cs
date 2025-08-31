using UnityEngine;
using UnityEngine.UI;

public class EnumyHpBar : MonoBehaviour
{
    [SerializeField] private Slider sliderHpBar;

    public void HpVarUpdate(float hp, float maxHp)
    {
        // print("22");
        sliderHpBar.value = hp / maxHp;
    }
}
