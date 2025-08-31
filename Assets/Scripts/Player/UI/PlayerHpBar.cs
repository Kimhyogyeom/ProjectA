using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    [SerializeField] private Slider playerHpBar;
    [SerializeField] private Slider playerBottomHpBar;

    public void HpVarUpdate(float hp, float maxHp)
    {
        playerHpBar.value = hp / maxHp;
        playerBottomHpBar.value = hp / maxHp;
    }
}