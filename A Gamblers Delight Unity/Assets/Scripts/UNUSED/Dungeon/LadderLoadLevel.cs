using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LadderLoadLevel : MonoBehaviour
{
    private string currentScene;
    private string targetScene;

    // Upon creating the ladder, figure out what the current open scene is and set the target scene accordingly.
    private void Awake()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Small Dungeon Level"))
        {
            targetScene = "Boss Room Level";
        }
        else
        {
            targetScene = "Small Dungeon Level";
        }
    }

    // When a collision occurs, check if it is the player.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If it is the player, load the target scene.
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}
