using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusStats : MonoBehaviour
{
    [SerializeField] private NexusHpBar nexusHpBar;

    public float nexusMaxHp = 300.0f;
    public float nexustHp = 0f;

    [SerializeField] private string classification = "";

    [SerializeField] private ObjectTweenEffect objectTweenEffect;

    [SerializeField] private GameObject[] nexusBoomEffect;
    [SerializeField] private Collider nexusCollider;
    [SerializeField] private GameObject nexusMesh;
    [SerializeField] private GameObject nexusCanvas;
    void Start()
    {
        nexustHp = nexusMaxHp;
    }

    public void TakeDamage(float damage)
    {
        nexustHp -= damage;
        nexusHpBar.HpVarUpdate(nexustHp, nexusMaxHp);

        if (nexustHp <= 0)
        {
            if (classification == "EnemyNexus")
            {
                // print("승리");
                nexusBoomEffect[0].SetActive(true);
                objectTweenEffect.GameVictory();
            }
            else if (classification == "Nexus")
            {
                // print("패배");
                nexusBoomEffect[1].SetActive(true);
                objectTweenEffect.GameDefeat();
            }
            GameManager.Instance.gameState = GameManager.GameState.Stop;
            SoundManager.Instance.PlaySfxUI(SoundManager.Instance.soundDatabase.nexusBreakClip);
            Destroy(this.gameObject);
            // StartCoroutine(DestoryNexus());
        }
    }
}
