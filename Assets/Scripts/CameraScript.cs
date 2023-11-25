using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothing = 3f;
    void Start()
    {
        offset.z = -10;
    }

    // Update is called once per frame
    void Update()
    {
        
        var nextPosition = Vector3.Lerp(transform.position, targetTransform.position + offset, Time.deltaTime * smoothing);
        transform.position = nextPosition; 
    }
}
