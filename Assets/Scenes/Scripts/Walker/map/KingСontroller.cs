using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class King—ontroller : MonoBehaviour
{
    [SerializeField]
    float speed = 5;
    // Start is called before the first frame update

    [SerializeField]
    Animator animator;

    bool lastSide = true;

    void Flip()
    {
        lastSide = !lastSide;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");

        float HorizontalMove = Input.GetAxis("Horizontal");
        animator.SetFloat("HorizontalMove", Mathf.Abs(HorizontalMove));

        if (HorizontalMove < 0 && lastSide)
        {
            Flip();
        }
        else if (HorizontalMove > 0 && !lastSide)
        {
            Flip();
        }


        if (verInput > 0)
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
            animator.SetFloat("HorizontalMove", 1);
        }

        if (verInput < 0)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
            animator.SetFloat("HorizontalMove", 1);
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
