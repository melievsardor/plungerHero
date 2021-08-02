using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private Dictionary<int, List<EnemyController>> enemies = new Dictionary<int, List<EnemyController>>();

    private void Start()
    {
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
        foreach (var enemy in enemies[index])
            enemy.SetPlay();
    }

}
