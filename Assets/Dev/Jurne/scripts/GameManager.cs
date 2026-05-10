using UnityEngine;

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetState(GameState.MainMenu);
    }

    public void SetState(GameState newState)
    {
        currentState = newState;
        Debug.Log("State changed to: " + newState);
        Time.timeScale = (newState == GameState.Paused) ? 0 : 1;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            SetState(GameState.Paused);

        if (Input.GetKeyDown(KeyCode.O))
            SetState(GameState.Playing);

        if (Input.GetKeyDown(KeyCode.G))
            SetState(GameState.GameOver);
    }
}