using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSystem : MonoBehaviour
{

    private int MaxEnemy = 10;
    private int LoseEnemyCounter = 0;
    private int EnemyCounter = 0;
    private void Awake()
    {
        EnemyCounter = GameObject.Find("Spawner").GetComponentInChildren<EnemySpawnerTD>().spawnSettings.Length;
    }

    public void EnemyAdd()
    {
        LoseEnemyCounter++;
        EnemyCounter--;
        CheckFight();
    }

    public void EnemyReset() {
        EnemyCounter--;
        CheckFight();
    }

    private void CheckFight()
    {
        print(EnemyCounter);
        if (LoseEnemyCounter > MaxEnemy)
        {
            LoseLocation();
        }
        else if (EnemyCounter == 0) 
        {
            WinLocation();
        }
    }

    private void LoseLocation() 
    {
        SceneManager.LoadScene("2d_map_scen");
    }
    
    private void WinLocation()
    {
        SceneManager.LoadScene("2d_map_scen");
    }
}
