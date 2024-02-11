using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardHolderManager : MonoBehaviour
{
    [Header("Holder Parameters")] 
    [SerializeField] private Transform CardHolderPosition;
    [SerializeField] private GameObject card;
    [SerializeField] private Card[] Cards;

    [Header("Card Parameters")]
    [SerializeField] private GameObject[] Decks;
    private int cost;
    private Sprite icon;

    void Start()
    {
        Decks = new GameObject[Cards.Length];

        for (int i = 0; i < Cards.Length; i++)
        {
            CreateCard(i);
        }

    }

    private void CreateCard(int index)
    {
        var newcard = Instantiate(card, CardHolderPosition);
        Decks[index] = newcard;
        CardManager cardManager = newcard.GetComponent<CardManager>();
        cardManager.TowerCard = Cards[index];
        print(cardManager.TowerCard);
        icon = Cards[index].icon;
        cost = Cards[index].cost;
        //print(icon);
        //print(cost);
        newcard.GetComponentInChildren<SpriteRenderer>().sprite = icon;
        newcard.GetComponentInChildren<TMP_Text>().text = cost.ToString();
    }
}
