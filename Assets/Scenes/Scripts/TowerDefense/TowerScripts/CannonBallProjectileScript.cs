using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallProjectileScript : MonoBehaviour
{

    Transform target;
    public float hitDistance = 35f;
    public float speed = 400f;
    public float damage = 40f;
    public float massiveDamage = 20f;
    public float attackRadius = 100f;
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
                    enemyParametrs.TakeDamage(damage);
                }
                //атака по области
                
                foreach(GameObject enemy in GameObject.FindGameObjectsWithTag(tagEn))
                {
                    if (enemy.transform != target){ 
                        float currDistance = Vector3.Distance(new Vector3(target.transform.position.x, 0, target.transform.position.z),new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z));

                    if (currDistance <= attackRadius)
                    {
                        EnemyParametrs enemyParametrsRad = enemy.GetComponent<EnemyParametrs>();
                        if (enemyParametrsRad != null)
                        {
                            enemyParametrsRad.TakeDamage(massiveDamage);
                        }
                    }
                    
                    }
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
