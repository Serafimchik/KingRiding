using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectileArrowScript : MonoBehaviour
{

    Transform target;
    public float hitDistance = 1f;
    public float speed = 0.3f;
    void Start()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void SetTarget(Transform enemy) 
    {
        target = enemy;
    }
    void Move()
    {
       
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) < hitDistance)
            {
                Destroy(gameObject);
            }
            else
            {
                Vector2 dir = target.position - transform.position;
                float angle = Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
                // transform.Translate(dir.normalized * Time.deltaTime * speed);
                transform.position = Vector3.MoveTowards(transform.position, target.position,Time.deltaTime*speed);

            }
        }
        else 
        { 
            Destroy(gameObject);
        }
    }

}
