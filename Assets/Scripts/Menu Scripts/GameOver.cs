using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public  Text state;
    public  static GameState current;
    public  Button returnButton;
    public  GameObject victoryBackground;
    public  GameObject defeatBackground;
    public  static string mainMenu = "Menu";
    public  static string gameoverScene = "GameOver";

    private void Awake()
    {       
        state = GameObject.Find("Canvas/GameState").GetComponent<Text>();
        returnButton = GameObject.Find("Canvas/Return").GetComponent<Button>();
        victoryBackground = GameObject.Find("Canvas/Victory BG");
        defeatBackground = GameObject.Find("Canvas/Defeat BG");

        victoryBackground.SetActive(false);
        defeatBackground.SetActive(false);      
    }

    private void Start()
    {
        returnButton.onClick.AddListener(ReturnMain);
        if (current == GameState.Win)
        {
            victoryBackground.SetActive(true);
            state.color = new Color(180f / 255f, 137f / 255f, 38f / 255f, 255f / 255f);
            state.fontSize = 100;
            state.text = "VICTORY";
        }
        else
        {
            defeatBackground.SetActive(true);
            state.color = new Color(159f / 255f, 0f / 255f, 41f / 255f, 255f / 255f);
            state.fontSize = 100;
            state.text = "DEFEAT";
        }
    }

    public static void Init(GameState gameState)
    {
        SceneManager.LoadScene(gameoverScene);
        current = gameState;
    }

    private static void ReturnMain()
    {
        SceneManager.LoadScene(mainMenu);
    }
}

public enum GameState
{
    Win, Fail
}