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
    }

    void OnDisable()
    {
        gamemanager.OnUpdateBalance -= UpdateUI;
    }

    void Awake()
    {
        Store = transform.GetComponent<store>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StoreCountText.text = Store.StoreCount.ToString();
        BuyButtonText.text = "Buy " + Store.NextStoreCost.ToString("C2");
    }

    // Update is called once per frame
    void Update()
    {
        ProgressSlider.value = Store.CurrentTimer / Store.StoreTimer;
        //UpdateUI();
    }

    void UpdateUI()
    {
        // Hide panel until you can afford the store
        CanvasGroup cg = this.transform.GetComponent<CanvasGroup>();
        if (!Store.StoreUnlocked && !gamemanager.instance.CanBuy(Store.NextStoreCost))
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
        if (gamemanager.instance.CanBuy(Store.NextStoreCost))
            BuyButton.interactable = true;
        else
            BuyButton.interactable = false;

        
    }

    public void BuyStoreOnClick()
    {
        if (!gamemanager.instance.CanBuy(Store.NextStoreCost))
            return;
        Store.BuyStore();
        BuyButtonText.text = "Buy " + Store.NextStoreCost.ToString("C2");
        StoreCountText.text = Store.StoreCount.ToString();
    }
    public void OnTimerClick()
    {
        Store.OnStartTimer();
    }

}
