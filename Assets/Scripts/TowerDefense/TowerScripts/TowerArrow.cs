using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerArrow : MonoBehaviour
{
    public float range = 2;
    public float CurrCooldown,Cooldown;
    public string tagEn = "Enemy";

    public GameObject Projectile;
    public void Update()
    {
        if (CanShoot())
        {
            SearchTarget();
        }

        if (CurrCooldown > 0) 
        {
            CurrCooldown -= Time.deltaTime;
        }
    }
    bool CanShoot() 
    {
        if (CurrCooldown <= 0)
        {
            return true;
        }
        return false;
    }
    void SearchTarget() {
        Transform nearesEnemy = null;
        float nearesEnemyDistance = Mathf.Infinity;

        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag(tagEn))
        {
            float currDistance = Vector2.Distance(transform.position,enemy.transform.position);

            if (currDistance < nearesEnemyDistance && currDistance <= range)
            {
                nearesEnemy = enemy.transform;
                nearesEnemyDistance = currDistance;
            }
        }
        if (nearesEnemy != null)
        {
            Shoot(nearesEnemy);
        }
    }
    void Shoot(Transform enemy) 
    { 
        CurrCooldown = Cooldown;
        GameObject proj = Instantiate(Projectile);
        proj.transform.position = transform.position;
        proj.GetComponent<TowerProjectileArrowScript>().SetTarget(enemy);
    }
    
}
