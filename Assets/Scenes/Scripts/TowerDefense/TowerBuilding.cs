using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilding : MonoBehaviour
{
    [SerializeField] private Vector2 towerSize;
    [SerializeField] private new Renderer renderer;
    [SerializeField] public int towerHigh;

    public Vector2 TowerSize { get => towerSize;set {; } }   
    
    private void SetColor(bool isAllowedtoBuild)
    {
        //print("Color moment");
        if (isAllowedtoBuild)
            renderer.material.color = Color.green;
        else renderer.material.color = Color.red;
    }

    private void ResetColor(Color color)
    {
        renderer.material.color = color; // Color.white;
    }

    public void ApplySetColor(bool isAllowedtoBuild)
    {
        Renderer[] towerBuildings = GetComponentsInChildren<Renderer>();
        foreach (Renderer towerBuilding in towerBuildings) 
        { 
            renderer = towerBuilding;
            SetColor(isAllowedtoBuild); 
        }
    }

    public void ApplyResetColor(List<Color> towercolors)
    {
        Renderer[] towerBuildings = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < towerBuildings.Length; i++)
        {
            renderer = (Renderer)towerBuildings[i];
            ResetColor(towercolors[i]);
        }
    }

}
