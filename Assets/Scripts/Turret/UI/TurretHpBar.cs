
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretHpBar : MonoBehaviour
{
    [SerializeField] private Slider turretHpBar;

    public void HpVarUpdate(float hp, float maxHp)
    {
        turretHpBar.value = hp / maxHp;
    }
}