using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private Dictionary<int, List<EnemyController>> enemies = new Dictionary<int, List<EnemyController>>();

    [SerializeField]
    private List<GameObject> grounds = new List<GameObject>();

    private PlayerController playerController;

    private int currentIndex;
    private int enemyDeathCount;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        for(int i = 0; i < grounds.Count; i++)
        {
            enemies[i] = new List<EnemyController>();
            enemies[i].AddRange(grounds[i].GetComponentsInChildren<EnemyController>());
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
