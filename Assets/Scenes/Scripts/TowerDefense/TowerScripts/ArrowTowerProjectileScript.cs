using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectileArrowScript : MonoBehaviour
{

    Transform target;
    public float hitDistance = 35f;
    public float speed = 100f;
    public float damage = 15f;
    
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
                EnemyParametrs enemyParametrs = target.GetComponent<EnemyParametrs>();
                if (enemyParametrs != null)
                {
                    enemyParametrs.TakeDamage(damage);
                }
                Destroy(gameObject);
            }
            else
            {
                Vector3 dir = target.position - transform.position;
                transform.rotation = Quaternion.LookRotation(dir);
                //float angle = Mathf.Atan2(dir.x,dir.y)*Mathf.Rad2Deg;
                //transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
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
