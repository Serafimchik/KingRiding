using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyParametrs : MonoBehaviour
{
    public float maxHealth = 100f;  
    private float currentHealth;

    public float maxSpeed = 100f;
    private float currentSpeed;
    
    
    public GameObject healthBarUi;
    public Slider slider;

    private Coroutine runningCoroutine = null;
    
    void Start()
    {
        currentHealth = maxHealth;
        currentSpeed = maxSpeed;
        UpdateHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
