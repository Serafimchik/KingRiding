using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContorllerr : MonoBehaviour
{
    [SerializeField]
    float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");

        if (verInput > 0)
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
        }

        if (verInput < 0)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }

        if (horInput > 0)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }

        if (horInput < 0)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
    }
}
