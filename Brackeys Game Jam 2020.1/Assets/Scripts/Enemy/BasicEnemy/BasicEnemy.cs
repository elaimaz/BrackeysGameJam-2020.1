using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyBase
{

    public override void OnDamageTaken(int damage)
    {
        base.OnDamageTaken(damage);
        health -= damage;
        Debug.Log("Taken " + damage + " health : " + health);
    }
}
