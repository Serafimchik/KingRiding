using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_mover : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tmp = transform.position;
        tmp.x = player.position.x;
        tmp.y = player.position.y;

        transform.position = tmp;
    }
}
