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

    public void Start()
    {
        LoadData();
    }
    public void LoadData()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(GameData.text);
        XmlNodeList StoreList = xmlDoc.GetElementsByTagName("store");

        foreach(XmlNode StoreInfo in StoreList)
        {

            GameObject NewStore = (GameObject)Instantiate(StorePrefab);

            store storeobj = NewStore.GetComponent<store>();

            XmlNodeList StoreNodes = StoreInfo.ChildNodes;
            foreach(XmlNode StoreNode in StoreNodes)
            {
                if (StoreNode.Name == "name")
                { 
                    Text StoreText = NewStore.transform.Find("StoreNameText").GetComponent<Text>();
                    StoreText.text = StoreNode.InnerText;
                }

                if (StoreNode.Name == "BaseStoreCost")
                    storeobj.BaseStoreCost = float.Parse(StoreNode.InnerText);
                if (StoreNode.Name == "BaseStoreProfit")
                    storeobj.BaseStoreProfit = float.Parse(StoreNode.InnerText);
                if (StoreNode.Name == "StoreTimer")
                    storeobj.StoreTimer = float.Parse(StoreNode.InnerText);
            }
            NewStore.transform.SetParent(StorePanel.transform);
            
        }
    }
}
