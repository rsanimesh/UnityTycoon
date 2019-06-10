using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    float CurrentBalance; 
    public Text CurrentBalanceText;

    // Start is called before the first frame update
    void Start()
    {
        CurrentBalance = 1000; // Initial balance credited to player
        CurrentBalanceText.text = CurrentBalance.ToString("C2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Add the given amount to current balance and update the CurrentBalanceText
    public void AddToBalance(float amt)
    {
        CurrentBalance += amt;
        CurrentBalanceText.text = CurrentBalance.ToString("C2");
    }

    // Check if player has the input amount in the balance
    public bool CanBuy(float AmtToSpend)
    {
        if (AmtToSpend > CurrentBalance)
            return false;
        else
            return true;
    }
}
