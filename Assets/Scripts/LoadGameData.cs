using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class LoadGameData : MonoBehaviour
{

    public TextAsset GameData;
    public GameObject StorePrefab;
    public GameObject StorePanel;

    public GameObject ManagerPrefab;
    public GameObject ManagerPanel;

    public delegate void LoadDataComplete();
    public static event LoadDataComplete OnLoadDataComplete;

    public void Start()
    {
        LoadData();
        if (OnLoadDataComplete != null)
            OnLoadDataComplete();
    }
    public void LoadData()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(GameData.text);

        // Load GameManagerData
        LoadGameManagerData(xmlDoc);

        // Load the Store
        LoadStore(xmlDoc);


    }
    void LoadGameManagerData(XmlDocument _xmlDoc)
    {
        // Load Game Manager Info - Company Name
        XmlNodeList CompanyTextNameNode = _xmlDoc.GetElementsByTagName("CompanyName");
        gamemanager.instance.CompanyName = CompanyTextNameNode[0].InnerText;

        // Load Game Manager Info - Starting Balance
        XmlNodeList StartingBalanceNode = _xmlDoc.GetElementsByTagName("StartingBalance");
        gamemanager.instance.AddToBalance(float.Parse(StartingBalanceNode[0].InnerText));

    }
    void LoadStore(XmlDocument _xmlDoc)
    {
        XmlNodeList StoreList = _xmlDoc.GetElementsByTagName("store");

        foreach (XmlNode StoreInfo in StoreList)
        {
            // Load Store Nodes
            LoadStoreNodes(StoreInfo);
        }
    }

    void LoadStoreNodes(XmlNode StoreInfo)
    {
        GameObject NewStore = (GameObject)Instantiate(StorePrefab);

        store storeobj = NewStore.GetComponent<store>();

        XmlNodeList StoreNodes = StoreInfo.ChildNodes;
        foreach (XmlNode StoreNode in StoreNodes)
        {
            // Set Store variables
            SetStoreObj( StoreNode, NewStore, storeobj);


        }
        storeobj.SetNextStoreCost(storeobj.BaseStoreCost);
        NewStore.transform.SetParent(StorePanel.transform);

    }

    void SetStoreObj(XmlNode StoreNode, GameObject NewStore, store storeobj)
    {
        if (StoreNode.Name == "name")
        {
            Text StoreText = NewStore.transform.Find("StoreNameText").GetComponent<Text>();
            StoreText.text = StoreNode.InnerText;
            storeobj.StoreName = StoreNode.InnerText;
        }

        if (StoreNode.Name == "image")
        {
            Sprite newSprite = Resources.Load<Sprite>(StoreNode.InnerText);
            Image StoreImage = NewStore.transform.Find("ImageButtonClick").GetComponent<Image>();
            StoreImage.sprite = newSprite;
        }

        if (StoreNode.Name == "BaseStoreCost")
            storeobj.BaseStoreCost = float.Parse(StoreNode.InnerText);
        if (StoreNode.Name == "BaseStoreProfit")
            storeobj.BaseStoreProfit = float.Parse(StoreNode.InnerText);
        if (StoreNode.Name == "StoreTimer")
            storeobj.StoreTimer = float.Parse(StoreNode.InnerText);

        if (StoreNode.Name == "StoreMultiplier")
            storeobj.StoreMultiplier = float.Parse(StoreNode.InnerText);
        if (StoreNode.Name == "StoreTimerDivision")
            storeobj.StoreTimerDivision = int.Parse(StoreNode.InnerText);
        if (StoreNode.Name == "StoreCount")
            storeobj.StoreCount = int.Parse(StoreNode.InnerText);
        if (StoreNode.Name == "ManagerCost")
            CreateManager(StoreNode, storeobj);
    }

    void CreateManager(XmlNode StoreNode, store storeobj)
    {
        GameObject NewManager = (GameObject)Instantiate(ManagerPrefab);
        Text ManagerNameText = NewManager.transform.Find("ManagerNameText").GetComponent<Text>();
        ManagerNameText.text = storeobj.StoreName;

        storeobj.ManagerCost = float.Parse(StoreNode.InnerText);
        Button ManagerButton = NewManager.transform.Find("UnlockManagerButton").GetComponent<Button>();
        Text ManagerButtonText = ManagerButton.transform.Find("UnlockManagerButtonText").GetComponent<Text>();
        ManagerButtonText.text = "Unlock " + storeobj.ManagerCost.ToString("C2");
        storeobj.UnlockManagerButton = ManagerButton;
        NewManager.transform.SetParent(ManagerPanel.transform);

        

    }
}
