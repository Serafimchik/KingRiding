using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CardManager : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Card towerCard;

    public Card TowerCard
    {
        get => towerCard;
        set => towerCard = value;
    }

    private GameObject draggingTower;
    private TowerBuilding tower;
    private int towercost;
    private EconomyManager economy;


    private Vector2Int gridSize = new Vector2Int(53, 53);
    private bool isAllowedtoBuild;


    private Location LocationController;

    private void Awake()
    {


        LocationController = Location.Instance;
        LocationController.locallocation = new TowerBuilding[gridSize.x, gridSize.y];
        //print("Awake");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //print("OnDrag");
        if (draggingTower != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 mapPosition = ray.GetPoint(position);
                int x = Mathf.RoundToInt(mapPosition.x);
                int z = Mathf.RoundToInt(mapPosition.z);


                if (x < 1 || x > gridSize.x - 2 - tower.TowerSize.x)
                {
                    isAllowedtoBuild = false;
                    //print($"Error: 1 " +
                    //    $"x = {x} " +
                    //    $"gridSize.x = {gridSize.x} " +
                    //    $"TowerSize.x = {tower.TowerSize.x} " +
                    //    $"gridSize.x - TowerSize.x = {gridSize.x - tower.TowerSize.x}");

                }

                else if (z < 1 || z > gridSize.y - 2 - tower.TowerSize.y)
                {
                    //print($"Error: 2 " +
                    //    $"z = {z} " +
                    //    $"gridSize.y = {gridSize.y} " +
                    //    $"TowerSize.y = {tower.TowerSize.y} " +
                    //    $"gridSize.y - TowerSize.y = {gridSize.y - tower.TowerSize.y} ");

                    isAllowedtoBuild = false;
                }

                else if (IsTerritoryOccupied(x, z))
                {
                    //print("3");
                    isAllowedtoBuild = false;
                }
                else
                {
                    //print("4");
                    if (!GameObject.Find("EconomyManager").GetComponentInChildren<EconomyManager>().DeleteCoin(towercost))
                    {
                        //print("4.5");
                        isAllowedtoBuild = false;
                    }
                    else
                    {
                        //print("5");
                        isAllowedtoBuild = true;
                    }
                }
                draggingTower.transform.position = new Vector3(x, 0, z);
                tower.SetColor(isAllowedtoBuild);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //print("OnPointerDown");
        draggingTower = Instantiate(towerCard.prefab, Vector3.zero, Quaternion.identity);

        tower = draggingTower.GetComponent<TowerBuilding>();

        towercost = towerCard.cost;

        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float position))
        {
            Vector3 mapPosition = ray.GetPoint(position);
            int x = Mathf.RoundToInt(mapPosition.x);
            int z = Mathf.RoundToInt(mapPosition.z);

            draggingTower.transform.position = new Vector3(x, 0, z);

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //print("OnPointerUp");
        if (!isAllowedtoBuild)
            Destroy(draggingTower);
        else
        {
            LocationInstaller();

            tower.ResetColor();
            GameObject.Find("EconomyManager").GetComponentInChildren<EconomyManager>().SpendCoin(towercost);
            //print($"dragging x = {(int)draggingTower.transform.position.x}, dragging z = {(int)draggingTower.transform.position.z}");
            //location[(int) draggingTower.transform.position.x, (int) draggingTower.transform.position.z] = tower;
        }
    }

    private bool IsTerritoryOccupied(int x, int y)
    {
        if (LocationController.locallocation[x, y])
            return true;
        return false;
    }
    private void LocationInstaller()
    {
        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                if ((int)draggingTower.transform.position.x + i >= 0 && (int)draggingTower.transform.position.z + j >= 0)
                    LocationController.locallocation[(int)draggingTower.transform.position.x + i, (int)draggingTower.transform.position.z + j] = tower;
            }
        }
    }
}
