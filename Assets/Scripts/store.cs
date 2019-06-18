using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class store : MonoBehaviour
{
    // public variables - Define Gameplay
    public string StoreName;
    public float BaseStoreCost; // Starting cost for the store
    public float BaseStoreProfit; // Starting profit that store will give after one run 
    public float StoreTimer = 4f; // Time store will take to run for creating profit
    public int StoreCount=0; // No of stores bought
    public bool ManagerUnlock=true; // If true: Automatically restart the store, flase: Run store whenever clicked
    public float StoreMultiplier; // Fraction by which store price will increase every time
    public bool StoreUnlocked=false; // If true: Store is visible to player else not
    public int StoreTimerDivision = 20; // Reduce store timer by half when store count is multiple of 
    float NextStoreCost; // Updated value of store cost, calculated using BaseStoreCost, StoreMultiplier, StoreCount
    float CurrentTimer = 0;
    public bool StartTimer; // To start running the store
    public float ManagerCost;


    // Start is called before the first frame update
    void Start()
    {
        //NextStoreCost = BaseStoreCost;
        StartTimer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartTimer)
        {
            CurrentTimer += Time.deltaTime;
            if(CurrentTimer > StoreTimer)
            {
                if (!ManagerUnlock)
                    StartTimer = false;
                CurrentTimer = 0f;
                gamemanager.instance.AddToBalance(BaseStoreProfit * StoreCount);
            }
        }
    }


    public void BuyStore()
    { 
        StoreCount = StoreCount + 1;
        Debug.Log(StoreCount);
        float Amt = -NextStoreCost;
        NextStoreCost = (BaseStoreCost * Mathf.Pow(StoreMultiplier, StoreCount));
        gamemanager.instance.AddToBalance(Amt);
        
        Debug.Log(NextStoreCost);

        if  (StoreCount % StoreTimerDivision == 0)
        {
            StoreTimer = StoreTimer / 2;
        }
    }

    public void OnStartTimer()
    {
        if (!StartTimer && StoreCount > 0)
            StartTimer = true;
    }

    public void SetNextStoreCost(float amt)
    {
        NextStoreCost = amt;
    }

    public float GetCurrentTimer()
    {
        return CurrentTimer;
    }

    public float GetStoreTimer()
    {
        return StoreTimer;
    }
    public float GetNextStoreCost()
    {
        return NextStoreCost;
    }
}
 