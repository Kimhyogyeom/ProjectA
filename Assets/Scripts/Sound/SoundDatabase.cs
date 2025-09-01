using UnityEngine;

[CreateAssetMenu(fileName = "SoundDatabase", menuName = "Sound/SoundDatabase")]
public class SoundDatabase : ScriptableObject
{
    [Header("SFX UI")]
    // 시스템 메시지
    public AudioClip systemMessage0Sec;
    public AudioClip systemMessage30Sec;
    public AudioClip systemMessage60Sec;
    public AudioClip systemMessageVictory;
    // 터렛
    public AudioClip turretBreakClip;
    // 넥서스
    public AudioClip nexusBreakClip;
    // 버튼
    public AudioClip buttonClickClip;
    // 골드
    public AudioClip goldGetClip;
    // 에너미
    public AudioClip enemyDie;
    // 아이템
    public AudioClip itemDrop;
    public AudioClip itemBuy;
    public AudioClip itemSell;
    public AudioClip itemError;
    // 플레이어
    public AudioClip playerLevelUp;
    public AudioClip playerSkillLevelUp;

    [Header("SFX")]
    // 플레이어
    public AudioClip playerWalkClip;
    public AudioClip playerDieClip;
    public AudioClip playerResurrectionClip;
    public AudioClip playerBasicAttackClip;
    public AudioClip playerSkill01Clip;
    public AudioClip playerSkill02Clip;
    public AudioClip playerSkill03Clip;
    public AudioClip playerSkill04Clip;



}
