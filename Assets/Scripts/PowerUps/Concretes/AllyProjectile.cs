using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyProjectile : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int power;
    [SerializeField]
    protected LayerMask enemyLayer;

    protected Collider2D enemyCollider;
    public virtual void HitEnemy()
    {

    }
}
