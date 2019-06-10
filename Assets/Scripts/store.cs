using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class store : MonoBehaviour
{
    // public variables - Define Gameplay
    public float BaseStoreCost; // Starting cost for the store
    public float BaseStoreProfit; // Starting profit that store will give after one run 
    public float StoreTimer = 4f; // Time store will take to run for creating profit
    public int StoreCount=0; // No of stores bought
    public bool ManagerUnlock=true; // If true: Automatically restart the store, flase: Run store whenever clicked
    public float StoreMultiplier; // Fraction by which store price will increase every time
    public bool StoreUnlocked=false; // If true: Store is visible to player else Not

    public Text StoreCountText; // To display store count
    public Slider ProgressSlider; 
    public gamemanager GameManager;
    public Text BuyButtonText;
    public Button BuyButton;

    private float NextStoreCost; // Updated value of store cost, calculated using BaseStoreCost, StoreMultiplier, StoreCount
    float CurrentTimer = 0;
    bool StartTimer; // To start running the store


    // Start is called before the first frame update
    void Start()
    {
        NextStoreCost = BaseStoreCost;
        StoreCountText.text = StoreCount.ToString();
        StartTimer = false;
        UpdateBuyButton();
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
                GameManager.AddToBalance(BaseStoreProfit * StoreCount);
            }
        }
        ProgressSlider.value = CurrentTimer / StoreTimer;
        UnlockStore();
        CheckBuyButton();
        
    }

    void UnlockStore()
    {
        CanvasGroup cg = this.transform.GetComponent<CanvasGroup>();
        if (!StoreUnlocked && !GameManager.CanBuy(NextStoreCost))
        {
            cg.interactable = false;
            cg.alpha = 0;
        }
        else
        {
            StoreUnlocked = true;
            cg.interactable = true;
            cg.alpha = 1;
        }
    }

    void CheckBuyButton()
    {
        if (GameManager.CanBuy(NextStoreCost))
            BuyButton.interactable = true;
        else
            BuyButton.interactable = false;
    }

    void UpdateBuyButton()
    {
        BuyButtonText.text = "Buy " + NextStoreCost.ToString("C2");
    }


    public void BuyStoreOnClick()
    {
        if (!GameManager.CanBuy(NextStoreCost))
            return;

        StoreCount = StoreCount + 1;
        Debug.Log(StoreCount);
        StoreCountText.text = StoreCount.ToString();
        GameManager.AddToBalance(-NextStoreCost);
        NextStoreCost = (BaseStoreCost * Mathf.Pow(StoreMultiplier, StoreCount));
        Debug.Log(NextStoreCost);
        UpdateBuyButton();
    }

    public void StoreOnClick()
    {
        Debug.Log("Clicked the store");
        if (!StartTimer)
            StartTimer = true;

    }
}
