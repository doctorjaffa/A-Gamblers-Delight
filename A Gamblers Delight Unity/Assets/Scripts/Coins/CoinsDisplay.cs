using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsDisplay : MonoBehaviour
{
    // Variables that are references to objects in this scene 
    private Text coinsValue;
    private Coins coins;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Text component attached to this object and store it in the variable
        coinsValue = GetComponent<Text>();

        // Search the scene for the Coins component and store it in the variable
        coins = FindObjectOfType<Coins>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current amount of coins and set the text to match that value
        coinsValue.text = coins.GetCoins().ToString();
    }
}
