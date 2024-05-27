using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestController : MonoBehaviour
{
    
    public float speed = 30f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
 
        if (Input.GetKey(KeyCode.W))
        {
            transform.position +=speed*Time.deltaTime* new Vector3(0,0,1);
        }
 
        if (Input.GetKey(KeyCode.S))
        {
            transform.position +=speed*Time.deltaTime* new Vector3(0,0,-1);;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position +=speed*Time.deltaTime* Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position +=speed*Time.deltaTime* Vector3.right;
        }
 
        
    }

   
}
