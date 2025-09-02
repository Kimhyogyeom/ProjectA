using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Ready, Play, Pause, Stop
    }
    public GameState gameState = GameState.Ready;

    public static GameManager Instance = null;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

}
