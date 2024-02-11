using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilding : MonoBehaviour
{
    [SerializeField] private Vector2 towerSize;
    [SerializeField] private new Renderer renderer;

    public Vector2 TowerSize { get => towerSize;set {; } }   
    
    public void SetColor(bool isAllowedtoBuild)
    {
        //print("Color moment");
        if (isAllowedtoBuild)
            renderer.material.color = Color.green;
        else renderer.material.color = Color.red;
    }

    public void ResetColor()
    {
        renderer.material.color = Color.white;
    }
}
