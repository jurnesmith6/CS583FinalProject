using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver,
    Audio
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

	public PlayerController player; 

    public GameState currentState;

	public Crystal crystal;

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
        
        switch (newState)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                break;
            default:
                Time.timeScale = 0f;
                break;
        }
        
        //Time.timeScale = (newState == GameState.Paused || newState == GameState.MainMenu || newState == GameState.Audio) ? 0 : 1;
    }

    public void playGame()
    {
        SetState(GameState.Playing);
    }

    public void resumeGame()
    {
        SetState(GameState.Playing);
    }

    public void reloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void audioSettings()
    {
        SetState(GameState.Audio);
    }

    public void gameOver()
    {
        //when player health or crystal health < 0;


    }

    public void changePlayerHealth()
    {
        //when player receives damage
        
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            SetState(GameState.Paused);

        if (Input.GetKeyDown(KeyCode.O))
            SetState(GameState.Playing);

        if (Input.GetKeyDown(KeyCode.G))
            SetState(GameState.GameOver);

		if(player.hp <= 0 || crystal.hitpoints <=0 )
			SetState(GameState.GameOver);



    }


}