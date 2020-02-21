using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillBossEnemy : EnemyBase
{
    public GameObject ProjectilePrefab;
    public float MeleeAttackDamageRadius;
    public float ProjectileSpeed;

    private bool isDead = false;

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
        GameObject go = Instantiate(ProjectilePrefab, transform.GetChild(0).position, transform.GetChild(0).rotation);
        go.GetComponent<ProjectileScript>().SetProjectile((int)longDamage, ProjectileSpeed);
        go.transform.GetChild(Random.Range(0, go.transform.childCount)).gameObject.SetActive(true);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.GetChild(1).position, MeleeAttackDamageRadius);
    }
}
