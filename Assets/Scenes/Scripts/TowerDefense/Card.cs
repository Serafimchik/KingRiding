using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Cards", fileName = "New card", order = 51)]
public class Card : ScriptableObject
{
    public Sprite icon;
    public GameObject prefab;
    public int cost;
   
    public Sprite Icon {  get { return icon; } }
    public int Cost { get { return cost; } }

}
