using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Loads the game scene
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    // Loads the legend viewing scene
    public void LegendViewer()
    {
        SceneManager.LoadScene("LegendViewer");
    }

    // Loads multiplayer selection screen and legend selection
    public void CharacterSelect()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    // Returns to main menu
    public void MenuReturn()
    {
        SceneManager.LoadScene("Menu");
    }

    // Quits game, won't work while in preview but will once game is built.
    public void QuitGame()
    {
        Debug.Log("Game Quit.");
        Application.Quit();
    }
}
