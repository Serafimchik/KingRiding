using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Location : MonoBehaviour
{
    private static Location instancelocation;
    public static Location Instance {  get { return instancelocation; } }

    public TowerBuilding[,] alllocation;
    public TowerBuilding[,] locallocation {  get => alllocation; set => alllocation = value; }

    private void Awake()
    {
        if (instancelocation != this && instancelocation != null) { Destroy(this.gameObject); }
        else instancelocation = this;
    }
}
