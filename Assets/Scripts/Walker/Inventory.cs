using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryInfoWindow;
    public Text TextInfo;
    public static Dictionary<string, int> inventory = new Dictionary<string, int>();
    string output;
    bool isShowInventory;
    // Start is called before the first frame update
    void Start()
    {
        InventoryInfoWindow.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isShowInventory == false)
            {
                InventoryInfoWindow.SetActive(true);
                isShowInventory = true;
                foreach (var item in inventory)
                {
                    output += ($"{item.Key}: {item.Value} ");
                }
                if (output == "")
                {
                    TextInfo.text = "inventory is empty";
                }
                else
                {
                    TextInfo.text = output;
                }


            }
            else
            {
                InventoryInfoWindow.SetActive(false);
                isShowInventory = false;
                output = "";
            }
        }
    }
}
