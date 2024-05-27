using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : MonoBehaviour
{
    public float range = 300;
    public float CurrCooldown;
    public float Cooldown = 1;
    public string tagEn = "Enemy";
    public GameObject Projectile;
    public float yOffset = 0.5f;
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
            float currDistance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z));

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
        proj.transform.position = transform.position + Vector3.up * yOffset;;
        
        proj.GetComponent<FireBallProjectile>().SetTarget(enemy);
    }
}
