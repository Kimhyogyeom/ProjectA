using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SoundManager soundManager;
    public enum GameState
    {
        Ready, Play, Pause, Stop
    }
    public GameState gameState = GameState.Ready;

    public static GameManager Instance = null;

    void Awake()
    {
        Instance = this;
    }

}
