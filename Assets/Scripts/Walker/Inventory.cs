using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject InfoWindow;
    public Text TextInfo;
    public static Dictionary<string, int> inventory = new Dictionary<string, int>();
    string output;
    bool isShowInventory;
    // Start is called before the first frame update
    void Start()
    {
        InfoWindow.SetActive(false);
    }
    void Update()
    {
        //конкретная кнопка
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isShowInventory == false)
            {
                InfoWindow.SetActive(true);
                isShowInventory = true;
                foreach (var item in inventory)
                {
                    output += ($"{item.Key}: {item.Value} ");
                }
                if (output == "")
                {
                    TextInfo.text = "Инвентарь пуст";
                }
                else
                {
                    TextInfo.text = output;
                }
                

            }
            else
            {
                InfoWindow.SetActive(false);
                isShowInventory = false;
                output = "";
            }
        }
    }
}
