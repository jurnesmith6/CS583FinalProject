using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject hud;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    void Update()
    {
        switch (GameManager.Instance.currentState)
        {
            case GameState.MainMenu:
                Show(mainMenu);
                break;
            case GameState.Playing:
                Show(hud);
                break;
            case GameState.Paused:
                Show(pauseMenu);
                break;
            case GameState.GameOver:
                Show(gameOverMenu);
                break;
        }
    }

    void Show(GameObject panel)
    {
        mainMenu.SetActive(panel == mainMenu);
        hud.SetActive(panel == hud);
        pauseMenu.SetActive(panel == pauseMenu);
        gameOverMenu.SetActive(panel == gameOverMenu);
    }
}