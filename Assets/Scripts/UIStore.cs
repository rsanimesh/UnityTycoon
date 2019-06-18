using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UIStore : MonoBehaviour
{
    public Text StoreCountText; // To display store count
    public Slider ProgressSlider;
    public Text BuyButtonText;
    public Button BuyButton;

    public store Store;

    // On Enable it suscribe to event form gamemanager
    void OnEnable()
    {
        // whenever event occurs you get notification
        // and you attach that event to a function to take an action
        // parameters and return type of event and function need to be same
        gamemanager.OnUpdateBalance += UpdateUI;
        LoadGameData.OnLoadDataComplete += UpdateUI;
    }

    void OnDisable()
    {
        gamemanager.OnUpdateBalance -= UpdateUI;
        LoadGameData.OnLoadDataComplete -= UpdateUI;
    }

    void Awake()
    {
        Store = transform.GetComponent<store>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StoreCountText.text = Store.StoreCount.ToString();
        BuyButtonText.text = "Buy " + Store.GetNextStoreCost().ToString("C2");

    }

    // Update is called once per frame
    void Update()
    {
        ProgressSlider.value = Store.GetCurrentTimer() / Store.GetStoreTimer();
        //UpdateUI();
    }

    void UpdateUI()
    {
        // Hide panel until you can afford the store
        CanvasGroup cg = this.transform.GetComponent<CanvasGroup>();
        if (!Store.StoreUnlocked && !gamemanager.instance.CanBuy(Store.GetNextStoreCost()))
        {
            cg.interactable = false;
            cg.alpha = 0;
        }
        else
        {
            Store.StoreUnlocked = true;
            cg.interactable = true;
            cg.alpha = 1;
        }

        // Update button if you can afford the store
        if (gamemanager.instance.CanBuy(Store.GetNextStoreCost()))
            BuyButton.interactable = true;
        else
            BuyButton.interactable = false;
        BuyButtonText.text = "Buy " + Store.GetNextStoreCost().ToString("C2");

        // Update manager button if you can afford the store
        if (gamemanager.instance.CanBuy(Store.ManagerCost))
            Store.UnlockManagerButton.interactable = true;
        else
            Store.UnlockManagerButton.interactable = false;

    }

    public void BuyStoreOnClick()
    {
        if (!gamemanager.instance.CanBuy(Store.GetNextStoreCost()))
            return;
        Store.BuyStore();
        StoreCountText.text = Store.StoreCount.ToString();
        UpdateUI();
    }
    public void OnTimerClick()
    {
        Store.OnStartTimer();
    }

}
