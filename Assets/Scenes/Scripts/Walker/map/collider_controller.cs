using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider_controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Inventory.inventory.ContainsKey("Пива")) {
            if (Inventory.inventory["Пива"] >= 100) { GetComponent<BoxCollider2D>().enabled = false; }
        }
    }
}
