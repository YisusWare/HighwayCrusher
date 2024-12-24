using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueSceneScript", menuName = "DialogScript")]
public class EnemyNode : ScriptableObject
{
    GameObject enemyPrefab;
    Vector2 spawnPosition;
    float waitTime;
}
