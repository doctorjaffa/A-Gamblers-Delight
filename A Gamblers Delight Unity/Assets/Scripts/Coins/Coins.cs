using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Coins : MonoBehaviour
{
    // Project prefab
    [SerializeField]
    private GameObject Projectile;

    // Private variables
    // Starting and current value of coins
    private int startingAmount = 100;
    private int currentAmount;

    // Invincibility frames time
    private float hitInvincibilityMaxTime = 1.3f;

    // How long it has been since the player was hit
    private float lastHitTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Set the current amount of coins to the starting
        currentAmount = startingAmount;
    }

    // Fires coin from player, then checks how many the player has
    public void ShootCoin()
    {
        // Create a copy of the weapon when it is fired
        Instantiate(Projectile, transform.position, Quaternion.identity);

        // Decrease the amount of coins owned by 1
        --currentAmount;

        // Check if the player has ran out of coins
        CheckCoinsAmount();
    }

    // Updates current amount of coins owned
    public void ChangeCoins(int changeAmount)
    {
        // If enough time has passed since the player was last hit
        if (Time.time >= lastHitTime + hitInvincibilityMaxTime)
        {
            // Update the amount of coins owned
            currentAmount += changeAmount;

            // Check if the player has ran out of coins
            CheckCoinsAmount();

            // Update the time since the player was last hit
            lastHitTime = Time.time;
        }

        // Check if the player has ran out of coins
        CheckCoinsAmount();
    }

    // Getter function to give information on current amount of coins held
    public int GetCoins()
    {
        return currentAmount;
    }

    // Kill the player and restart
    private void KillPlayer()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Checks if the current amount of coins owned is 0 or less and, if so, the player loses
    private void CheckCoinsAmount()
    {
        // If the player runs out of coins, they lose
        if (currentAmount <= 0)
        {
            KillPlayer();
        }
    }
}
