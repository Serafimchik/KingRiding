using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;

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
    private int towerhigh;
    private List<Color> towercolors;
    private Terrain terrain;
    private Vector2Int gridSize = new Vector2Int(2000, 2000);
    private bool isAllowedtoBuild;
    private int roadTextureIndex = 2;

    private Location LocationController;

    private void Awake()
    {
        terrain = GameObject.FindGameObjectWithTag("platform").GetComponent<Terrain>();
        //CalculatePlatformEdges(platform);
        LocationController = Location.Instance;
        LocationController.locallocation = new TowerBuilding[gridSize.x, gridSize.y];
        print(LocationController.locallocation);
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

                if (!IsInsideTerrainBounds(mapPosition))
                {
                    towerhigh = tower.towerHigh;
                    isAllowedtoBuild = false;
                }

                else if (IsTerritoryOccupied(x, z))
                {
                    //print("3");
                    float terrainHeight = terrain.SampleHeight(mapPosition);
                    towerhigh = (int)terrainHeight + tower.towerHigh;
                    isAllowedtoBuild = false;
                }
                else if (IsRoad(mapPosition))
                {
                    float terrainHeight = terrain.SampleHeight(mapPosition);
                    towerhigh = (int)terrainHeight + tower.towerHigh;
                    isAllowedtoBuild = false;
                }
                else
                {
                    //print("4");
                    if (!GameObject.Find("EconomyManager").GetComponentInChildren<EconomyManager>().DeleteCoin(towercost))
                    {
                        //print("4.5");
                        float terrainHeight = terrain.SampleHeight(mapPosition);
                        towerhigh = (int)terrainHeight + tower.towerHigh;
                        isAllowedtoBuild = false;
                    }
                    else
                    {
                        //print("5");
                        float terrainHeight = terrain.SampleHeight(mapPosition);
                        towerhigh = (int)terrainHeight + tower.towerHigh;
                        isAllowedtoBuild = true;
                    }
                }
                draggingTower.transform.position = new Vector3(x, towerhigh, z);
                tower.ApplySetColor(isAllowedtoBuild);
                //CheckObjectsInside();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //print("OnPointerDown");
        draggingTower = Instantiate(towerCard.prefab, Vector3.zero, Quaternion.identity);

        tower = draggingTower.GetComponent<TowerBuilding>();

        towercost = towerCard.cost;
        towerhigh = tower.towerHigh;
        towercolors = TakeTowerColors();

        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float position))
        {
            Vector3 mapPosition = ray.GetPoint(position);
            int x = Mathf.RoundToInt(mapPosition.x);
            int z = Mathf.RoundToInt(mapPosition.z);

            print(tower.towerHigh);
            draggingTower.transform.position = new Vector3(x, towerhigh, z);

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

            tower.ApplyResetColor(towercolors);
            GameObject.Find("EconomyManager").GetComponentInChildren<EconomyManager>().SpendCoin(towercost);
            //print($"dragging x = {(int)draggingTower.transform.position.x}, dragging z = {(int)draggingTower.transform.position.z}");
            //location[(int) draggingTower.transform.position.x, (int) draggingTower.transform.position.z] = tower;
        }
    }

    private bool IsTerritoryOccupied(int x, int y)
    {
        if (LocationController.locallocation[x, y])
            return true;
        if (LocationController.locallocation[x-(int)tower.TowerSize.x, y-(int)tower.TowerSize.y])
            return true;
        if (LocationController.locallocation[x-(int)tower.TowerSize.x, y+(int)tower.TowerSize.y])
            return true;
        if (LocationController.locallocation[x+(int)tower.TowerSize.x, y-(int)tower.TowerSize.y])
            return true;
        if (LocationController.locallocation[x + (int)tower.TowerSize.x, y + (int)tower.TowerSize.y])
            return true;
        return false;
    }
    private void LocationInstaller()
    {   
        print($"tower.TowerSize.x = {tower.TowerSize.x}, tower.TowerSize.y = {tower.TowerSize.y}");
        for (int i = - (int)tower.TowerSize.x; i <= (int)tower.TowerSize.x; i++) 
        {
            for (int j = -(int)tower.TowerSize.y; j <= (int)tower.TowerSize.y; j++)
            {
                if ((int)draggingTower.transform.position.x + i >= 0 && (int)draggingTower.transform.position.z + j >= 0)
                    LocationController.locallocation[(int)draggingTower.transform.position.x + i, (int)draggingTower.transform.position.z + j] = tower;
            }
        }
    }

    List<Color> TakeTowerColors()
    {
        Renderer[] renderers = draggingTower.GetComponentsInChildren<Renderer>();
        List<Color> colors = new List<Color>();
        foreach (Renderer renderer in renderers)
        {
            colors.Add(renderer.material.color);
        }
        return colors;
    }

    private bool IsInsideTerrainBounds(Vector3 position)
    {
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainPosition = terrain.transform.position;

        float terrainWidth = terrainData.size.x;
        float terrainLength = terrainData.size.z;

        if (position.x - tower.TowerSize.x < terrainPosition.x || position.x + tower.TowerSize.x > terrainPosition.x + terrainWidth ||
            position.z - tower.TowerSize.y < terrainPosition.z || position.z - tower.TowerSize.y > terrainPosition.z + terrainLength)
        {
            return false;
        }
        return true;
    }

    bool IsRoad(Vector3 position)
    {
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainPosition = position - terrain.transform.position;
        Vector2 normalizedPos = new Vector2(
            Mathf.InverseLerp(0, terrainData.size.x, terrainPosition.x),
            Mathf.InverseLerp(0, terrainData.size.z, terrainPosition.z)
        );

        float[,,] alphas = terrainData.GetAlphamaps(
            (int)(normalizedPos.x * terrainData.alphamapWidth),
            (int)(normalizedPos.y * terrainData.alphamapHeight),
            1,
            1
        );

        float roadTextureAmount = alphas[0, 0, roadTextureIndex];
        return roadTextureAmount > 0.5f; // Пороговое значение для определения, что это дорожка
    }

}
