using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Handles button presses in the main menu.
public class MainMenu : MonoBehaviour
{
    // If the player presses the play button, load the dungeon level.
    public void PlayGame()
    {
        SceneManager.LoadScene("Small Dungeon Level");
    }
}
