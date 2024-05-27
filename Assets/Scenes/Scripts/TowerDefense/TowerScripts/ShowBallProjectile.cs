using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBallProjectile : MonoBehaviour
{
    Transform target;
    public float hitDistance = 35f;
    public float speed = 150f;
    public float damage = 50f;
    public string tagEn = "Enemy";
    
    
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
                    enemyParametrs.TakeDamageWithFrozen(damage);
                }
                
                Destroy(gameObject);
            }
            else
            {
                Vector3 dir = target.position - transform.position;
                transform.rotation = Quaternion.LookRotation(dir);
               
                transform.position = Vector3.MoveTowards(transform.position, target.position,Time.deltaTime*speed);

            }
        }
        else 
        { 
            Destroy(gameObject);
        }
    }
}
