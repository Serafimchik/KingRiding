using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomyManager : MonoBehaviour
{
    [SerializeField] private GameObject Resources;


    public int coinCounter;

    private void Awake()
    {
        coinCounter = 100;
        Resources.GetComponentInChildren<TMP_Text>().text = coinCounter.ToString();
    }
    public bool DeleteCoin(int price)
    {
        if (coinCounter >= price)
        {
            //coinCounter -= price;
            //Resources.GetComponentInChildren<TMP_Text>().text = coinCounter.ToString();
            //print(coinCounter);
            return true;
        }
        return false;
    }
    public void GetCoin(int price) {
        coinCounter += price;
        Resources.GetComponent<TMP_Text>().text = coinCounter.ToString();
    }

    public void SpendCoin(int price)
    {
        coinCounter -= price;
        Resources.GetComponent<TMP_Text>().text = coinCounter.ToString();
    }
}
