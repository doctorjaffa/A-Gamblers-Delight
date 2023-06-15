using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGamble : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // -------------------------------------------------------------------------------------------- VARIABLES INITIALISATION -------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    
    // Private variables
    // Coins object
    private Coins coins;

    // Lists for the unique numbers
    private List<int> surviveNumbers = new List<int>();
    private List<int> killNumbers = new List<int>();

    // The next number to be randomly determined in the loop
    private int nextNumber = 0;
    // Amonut of unique numbers in each list
    private int uniqueNumbersCount = 100;

    //Percantage of coins lost
    private int coinsLostPercent;
    // Total coins lost
    private int coinsLost;

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ---------------------------------------------------------------------------------------------------  FUNCTIONS --------------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // Awake is called on object creation
    private void Awake()
    {
        // Find the coins object
        coins = GetComponentInParent<Coins>();
    }

    // Main function to lose X amount of coins
    public void GambleCoins()
    {
        // Collect the unique survive and kill numbers
        CreateUniqueNumbers();

        // Get a random number between 1 - 1000
        int gambleNumber = Random.Range(1, 1000);

        // If this number does not match any unique number, run this code
        if (!CheckGambleNumber(gambleNumber))
        {
            // Figure out how many coins have been lost
            coinsLost = DetermineCoinsLost();

            // Update the current amount of coins to match this loss
            coins.ChangeCoins(-coinsLost);
        }
    }

    // Creates the unique survive and kill numbers
    private void CreateUniqueNumbers()
    {
        // Run this X amount of times (X = amount of unique numbers per list)
        for (int i = 0; i < uniqueNumbersCount; i++)
        {
            // Set the boolean to false - this is the check to ensure no duplicates
            bool surviveDone = false;

            // Runs this code first time then continues until a number is added
           while (!surviveDone)
            {
                // Get a random number between 1 - 1000
                nextNumber = Random.Range(0, 1000);

                // If the survive numbers list does not contain this nextNumber, run the code 
                if (!surviveNumbers.Contains(nextNumber))
                {
                    // Add the new number into the survive numbers list
                    surviveNumbers.Add(nextNumber);
                    // Set the boolean to true to end this iteration
                    surviveDone = true;
                }
            }
        }

        // Run this X amount of times (X = amount of unique numbers per list)
        for (int i = 0; i < uniqueNumbersCount; i++)
        {
            // Set the boolean to false - this is the check to ensure no duplicates
            bool killDone = false;

            // Runs this code first time then continues until a number is added
            while (!killDone)
            {
                // Get a random number between 1 - 1000
                nextNumber = Random.Range(0, 1000);

                // If the kill numbers list does not contain this nextNumber, run the code 
                if (!killNumbers.Contains(nextNumber) && !surviveNumbers.Contains(nextNumber))
                {
                    // Add the new number into the kill numbers list
                    killNumbers.Add(nextNumber);
                    // Set the boolean to true to end this iteration
                    killDone = true;
                }
            } 
        }
    }

    // Check the player's gambled number against the unqiue numbers
    private bool CheckGambleNumber(int gambleNumber)
    {
        // If the kill numbers list has the player's gambled number, run this code
        if (killNumbers.Contains(gambleNumber))
        {
            // Kill the player 
            Debug.Log("Player will be killed.");
            //Destroy(gameObject);
            SceneManager.LoadScene("Title Screen");
        }
        // Else If the survive numbers list has the player's gambled number, run this code
        else if (surviveNumbers.Contains(gambleNumber))
        {
            // The player lives, so return true
            Debug.Log("Player has survived.");
            return true;
        }

        // Else, return false
        return false;
    }

    // Determine how many coins the player will lose 
    private int DetermineCoinsLost()
    {
        // Get a random number between 1 - 10
        int randomNumber = Random.Range(1, 10);

        // Work out the percentage to lose
        coinsLostPercent = randomNumber * 10;

        // Divide the total coins owned by this percentage lost
        coinsLost /= coins.GetCoins();

        // Return the number of coins to lose 
        return coinsLost;
    }

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------ END OF FUNCTIONS ------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
}
