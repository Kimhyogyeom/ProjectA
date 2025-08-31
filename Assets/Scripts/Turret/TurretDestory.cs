
using UnityEngine;
using UnityEngine.UI;

public class TurretDestory : MonoBehaviour
{
    [SerializeField] private Image enemyTurret01;
    [SerializeField] private Image enemyTurret02;
    [SerializeField] private Image enemyTurret03;
    [SerializeField] private Image Turret01;
    [SerializeField] private Image Turret02;
    [SerializeField] private Image Turret03;

    [SerializeField] private Sprite destoryTurretIcon;

    public void EnemyTurretDestoryIcon(int number)
    {
        if (number == 1)
        {
            enemyTurret01.sprite = destoryTurretIcon;
        }
        else if (number == 2)
        {
            enemyTurret02.sprite = destoryTurretIcon;
        }
        else if (number == 3)
        {
            enemyTurret03.sprite = destoryTurretIcon;
        }
    }
    public void TurretDestoryIcon(int number)
    {
        if (number == 1)
        {
            Turret01.sprite = destoryTurretIcon;
        }
        else if (number == 2)
        {
            Turret02.sprite = destoryTurretIcon;
        }
        else if (number == 3)
        {
            Turret03.sprite = destoryTurretIcon;
        }
    }
}
