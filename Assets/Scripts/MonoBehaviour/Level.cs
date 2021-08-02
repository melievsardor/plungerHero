using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private Dictionary<int, List<EnemyController>> enemies = new Dictionary<int, List<EnemyController>>();

    private PlayerController playerController;

    private int currentIndex;
    private int enemyDeathCount;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        var grounds = GameObject.FindGameObjectsWithTag("ground");

        int i = 0;
        foreach(var ground in grounds)
        {
            enemies[i] = new List<EnemyController>();
            enemies[i].AddRange(ground.GetComponentsInChildren<EnemyController>());
            i++;
        }
    }


    public void SetActiveGround(int index)
    {
        currentIndex = index;

        foreach (var enemy in enemies[index])
            enemy.SetPlay();
    }

    public void NextGround()
    {
        if(currentIndex < enemies.Count)
        {
            enemyDeathCount++;

            if (enemyDeathCount == enemies[currentIndex].Count)
            {
                playerController.NextGround();
                enemyDeathCount = 0;
            }
        }

    }

}
