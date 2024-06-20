using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyParametrs : MonoBehaviour
{
    public Animator animator;
    public float maxHealth = 100f;  
    private float currentHealth;

    public float maxSpeed = 100f;
    public float currentSpeed;
    public float rotationSpeed = 10f;
    
    public GameObject healthBarUi;
    public Slider slider;

    private Coroutine runningCoroutine = null;
    
    public Transform[] waypoints;
    private int currentWaypointIndex;
    void Start()
    {
        animator = GetComponent<Animator>();
        // animator.SetTrigger("Walk");
        currentHealth = maxHealth;
        currentSpeed = maxSpeed;
        UpdateHealthText();
        currentWaypointIndex = 0;
        MoveToWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            // Если да, то двигаемся к следующей точке пути
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                // Если достигли последней точки пути, уничтожаем врага
                Destroy(gameObject);
                return;
            }
            
            
        }
        else
        {
            MoveToWaypoint();
        }
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;  
        UpdateHealthText();
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    public void Heal(float amount)
    {
        
        if (currentHealth+amount > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount; 
        }
        UpdateHealthText();
    }
    private void UpdateHealthText()
    {
        healthBarUi.SetActive(true);
        slider.value = currentHealth / maxHealth;

    }
    
    public void TakeDamageWithFire(float damage)
    {
        currentHealth -= damage;  
        UpdateHealthText();
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        
        runningCoroutine = StartCoroutine(BurnEnemy());

    }
    
    public void TakeDamageWithFrozen(float damage)
    {
        currentHealth -= damage;  
        UpdateHealthText();
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        
        runningCoroutine = StartCoroutine(FrozenEnemy());

    }
    
    IEnumerator BurnEnemy()
    {

        currentSpeed = maxSpeed;
        for (int i = 0; i < 5; i++)
        {
            
            TakeDamage(GetMaxHealth() * 0.03f);

            yield return new WaitForSeconds(1f); 
        }

        runningCoroutine = null;
    }

    IEnumerator FrozenEnemy()
    {
        for (int i = 0; i < 5; i++)
        {

            currentSpeed = maxSpeed / 2;

            yield return new WaitForSeconds(1f); 
        }

        currentSpeed = maxSpeed;
        runningCoroutine = null;    
    }
    
    void MoveToWaypoint()
    {
       
        Vector3 nextWaypointPosition = waypoints[currentWaypointIndex].position;

       
        Vector3 targetPosition = new Vector3(nextWaypointPosition.x, transform.position.y, nextWaypointPosition.z);

        Vector3 direction = (targetPosition - transform.position).normalized;
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);
        
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
    
    public void SetWaypoints(Transform[] newWaypoints)
    {
        waypoints = newWaypoints;
    }
}
