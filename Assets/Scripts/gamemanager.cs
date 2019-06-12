using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    // variables to raise a event 
    public delegate void UpdateBalance();
    public static event UpdateBalance OnUpdateBalance;

    public static gamemanager instance;
    float CurrentBalance; 
    

    // Start is called before the first frame update
    void Start()
    {
        CurrentBalance = 1000; // Initial balance credited to player
        // check if anyone has suscribed to our event
        if (OnUpdateBalance != null)
            OnUpdateBalance(); // if yes raise event  that current balance is changed

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Add the given amount to current balance and update the CurrentBalanceText
    public void AddToBalance(float amt)
    {
        CurrentBalance += amt;
        // check if anyone has suscribed to our event
        if (OnUpdateBalance != null)
            OnUpdateBalance(); // if yes raise event  that current balance is changed
    }

    // Check if player has the input amount in the balance
    public bool CanBuy(float AmtToSpend)
    {
        if (AmtToSpend > CurrentBalance)
            return false;
        else
            return true;
    }

    public float GetCurrentBalance()
    {
        return CurrentBalance;
    }
}
