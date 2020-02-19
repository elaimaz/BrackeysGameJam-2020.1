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
        if (health <= 0)
        {
            anime.SetTrigger("Death");
            Destroy(gameObject, 0.50f);
        }
        else
        {
            anime.SetTrigger("isTakingDamage");
        }
    }

    public override void OnMeleeAttackDone()
    {
        base.OnMeleeAttackDone();
        Collider2D coll = Physics2D.OverlapCircle(transform.position, meleeRange, PlayerLayer);
        if(coll != null)
        {
            coll.GetComponent<PlayerManager>().TakeDamage(meleeDamage);
        }
    }
}
