using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 3;
    [SerializeField] float FightChance = 1;
    private int walk = 0;
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        float vertInput = Input.GetAxis("Vertical");
        float horInput = Input.GetAxis("Horizontal");

        if (vertInput > 0)
        { transform.Translate(Vector2.up * Time.deltaTime * speed);
          //walk += 1;
        }
        if (vertInput < 0)
        { transform.Translate(-Vector2.up * Time.deltaTime * speed);
          //walk += 1;
        }

        if (horInput > 0)
        { transform.Translate(Vector2.right * Time.deltaTime * speed);
          //walk += 1;
        }
        if (horInput < 0)
        { transform.Translate(-Vector2.right * Time.deltaTime * speed);
          //walk += 1;
        }
         //if (walk + Random.Range(-100f, 90f) >= 100) { SceneManager.LoadScene("TowerDefense"); }
    }
}
