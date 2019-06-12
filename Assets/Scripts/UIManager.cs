using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text CurrentBalanceText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    public void UpdateUI()
    {
        CurrentBalanceText.text = gamemanager.instance.GetCurrentBalance().ToString("C2");
    }
}
