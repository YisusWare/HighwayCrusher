using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : Obstacle
{
    [SerializeField] GameObject[] drops;

    public override void DestroyEnemy()
    {

        int dropIndex = Random.Range(0, drops.Length);

        Instantiate(drops[dropIndex], transform.position, Quaternion.identity);
        base.DestroyEnemy();

    }
}
