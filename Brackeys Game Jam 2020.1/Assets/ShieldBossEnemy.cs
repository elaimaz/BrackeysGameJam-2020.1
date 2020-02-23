using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBossEnemy : EnemyBase
{
    public GameObject ProjectilePrefab;
    public float ProjectileSpeed;

    private bool isDead = false;
    private bool isMeleeAttacking = false;

    protected override void Start()
    {
        base.Start();
        powerUpGate = GameObject.Find("ClosedGateShieldBossPowerUp");
        roomGate = GameObject.Find("GateCloseInstantlyShield");
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
                anime.SetTrigger("Death");
            }
        }
        else
        {
            anime.SetTrigger("isTakingDamage");
        }
    }

    public void OnStartMeleeAttack()
    {
        isMeleeAttacking = true;
        transform.GetChild(1).gameObject.tag = "Untagged";
    }

    public void OnEndMeleeAttack()
    {
        isMeleeAttacking = false;
        transform.GetChild(1).gameObject.tag = "Enemy";
    }

    public override void OnRangedAttackDone()
    {
        Instantiate(ProjectilePrefab, transform.GetChild(0).position, transform.GetChild(0).rotation).GetComponent<ProjectileScript>().SetProjectile((int)longDamage, ProjectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isMeleeAttacking)
        {
            if(collision.tag == "Player")
            {
                collision.GetComponent<PlayerManager>().TakeDamage(meleeDamage);
            }
        }
    }
}
