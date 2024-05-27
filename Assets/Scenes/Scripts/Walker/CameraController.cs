using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    Vector2 maxVal;

    [SerializeField]
    Vector2 minVal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame 
    void Update()
    {
        Vector3 targetPosition = player.position; //получаем позицию игрока

        Vector3 boundPosition = new Vector3(
            Math.Clamp(targetPosition.x, minVal.x, maxVal.x),
            Math.Clamp(targetPosition.y, minVal.y, maxVal.y), -10);

        Vector3 smoothPosition = Vector3.Lerp(transform.position,boundPosition, Time.deltaTime);
        transform.position = smoothPosition;
    }
}
