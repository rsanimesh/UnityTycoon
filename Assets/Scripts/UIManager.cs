using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text CurrentBalanceText;
    public Text CompanyNameText;

    public enum State
    {
        Main, Mangers
    }

    public State CurrentState;
    public GameObject ManagerPanel;
    // Start is called before the first frame update
    void Start()
    {
        CurrentState = State.Main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onShowManagers()
    {
        CurrentState = State.Mangers;
        ManagerPanel.SetActive(true);
    }

    void onShowMain()
    {
        CurrentState = State.Main;
        ManagerPanel.SetActive(false);
    }

    public void OnCLickManager()
    {
        if (CurrentState == State.Main)
            onShowManagers();
        else
            onShowMain();

    }

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

    public void UpdateUI()
    {
        CurrentBalanceText.text = gamemanager.instance.GetCurrentBalance().ToString("C2");
        CompanyNameText.text = gamemanager.instance.CompanyName;
    }
}
