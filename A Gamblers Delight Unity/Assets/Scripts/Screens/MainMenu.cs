using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Handles button presses in the main menu
public class MainMenu : MonoBehaviour
{
    // If the player presses the play button, load the dungeon level
    public void PlayGame()
    {
        SceneManager.LoadScene("Spawn Level");
    }

    // If the player presses the quit button, close the game
    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha4))
        {
            PlayGame();
        }
        else if (Input.GetKey(KeyCode.Alpha7))
        {
            QuitGame();
        }
    }
}
