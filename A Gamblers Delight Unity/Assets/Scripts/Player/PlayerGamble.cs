using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGamble : MonoBehaviour
{
    private Coins coins;

    private List<int> surviveNumbers = new List<int>();
    private List<int> killNumbers = new List<int>();

    private int nextNumber = 0;
    private int uniqueNumbersCount = 100;
    private int coinsLostPercent;
    private int coinsLost;

    private void Awake()
    {
        coins = GetComponentInParent<Coins>();
    }

    public void GambleCoins()
    {
        CreateUniqueNumbers();

        int gambleNumber = Random.Range(1, 1000);

        if (!CheckGambleNumber(gambleNumber))
        {
            coinsLost = DetermineCoinsLost();

            coins.ChangeCoins(-coinsLost);
        }
    }

    private void CreateUniqueNumbers()
    {
        for (int i = 0; i < uniqueNumbersCount; i++)
        {
            bool surviveDone = false;

            do
            {
                nextNumber = Random.Range(0, 1000);

                if (!surviveNumbers.Contains(nextNumber))
                {
                    surviveNumbers.Add(nextNumber);
                    surviveDone = true;
                }
            } while (!surviveDone);
        }

        for (int i = 0; i < uniqueNumbersCount; i++)
        {
            bool killDone = false;

            do
            {
                nextNumber = Random.Range(0, 1000);

                if (!killNumbers.Contains(nextNumber) && !surviveNumbers.Contains(nextNumber))
                {
                    killNumbers.Add(nextNumber);
                    killDone = true;
                }
            } while (!killDone);
        }
    }

    private bool CheckGambleNumber(int gambleNumber)
    {
        if (killNumbers.Contains(gambleNumber))
        {
            Debug.Log("Player will be killed.");
            Destroy(gameObject);
            //SceneManager.LoadScene("Title Screen");
        }
        else if (surviveNumbers.Contains(gambleNumber))
        {
            Debug.Log("Player has survived.");
            return true;
        }

        return false;
    }

    private int DetermineCoinsLost()
    {
        int randomNumber = Random.Range(1, 10);

        coinsLostPercent = randomNumber * 10;

        coinsLost /= coins.GetCoins();

        return coinsLost;
    }
}
