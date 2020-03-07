using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBossEnemy : EnemyBase
{
    public GameObject ProjectilePrefab;
    public float MeleeAttackDamageRadius;
    public float ProjectileSpeed;

    private bool isDead = false;

    protected override void Start()
    {
        base.Start();
        powerUpGate = GameObject.Find("ClosedGateEyeBossPowerUp");
    }

    public override void OnDamageTaken(int damage)
    {
        base.OnDamageTaken(damage);
        health -= damage;
        Debug.Log("Taken " + damage + " health : " + health);
        if (health <= 0)
        {
            if (isDead == false)
            {
                isDead = true;
                Instantiate(healthPrefab, gameObject.transform.position, Quaternion.identity);
                anime.SetTrigger("Death");
                //Destroy(gameObject, 0.50f);
            }
        }
        else
        {
            anime.SetTrigger("isTakingDamage");
        }
    }

    public override void OnMeleeAttackDone()
    {
        base.OnMeleeAttackDone();
        Collider2D coll = Physics2D.OverlapCircle(transform.GetChild(1).position, MeleeAttackDamageRadius, PlayerLayer);
        if (coll != null)
        {
            coll.GetComponent<PlayerManager>().TakeDamage(meleeDamage);
        }
    }

    public override void OnRangedAttackDone()
    {
        base.OnRangedAttackDone();
        Instantiate(ProjectilePrefab, transform.GetChild(0).position, transform.GetChild(0).rotation).GetComponent<ProjectileScript>().SetProjectile((int)longDamage, ProjectileSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.GetChild(1).position, MeleeAttackDamageRadius);
    }

}
