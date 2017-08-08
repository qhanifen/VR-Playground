using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestFire : MonoBehaviour {

    public List<Enemy> enemies;

    float shootTimer = 10f;
    float timer = 0f;

    void Update()
    {
        if(timer >= shootTimer)
        {
            Fire();
        }
    }

    void Fire()
    {
        int fireNumber = Random.Range(1, enemies.Count);
        for(int i = 0; i < fireNumber; i++)
        {
            bool enemyFound = false;
            int j = Random.Range(0, enemies.Count);
            while (!enemyFound)
            {
                {
                    if (enemies[j].firing)
                    {
                        j = (j % enemies.Count) + 1; 
                    }
                    else
                    {
                        enemyFound = true;
                    }
                }
            }
            enemies[j].PreShoot();
            
        }
    }
}
