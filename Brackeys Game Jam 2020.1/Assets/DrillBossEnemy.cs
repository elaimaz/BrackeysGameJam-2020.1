using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillBossEnemy : EnemyBase
{
    public GameObject ProjectilePrefab;
    [Header("Attack From Sky")]
    public Vector2 ProjectileOrigin;
    public Vector2 size;
    public int NoOfProjectiles;
    [Space()]
    public float MeleeAttackDamageRadius;
    public float ProjectileSpeed;

    private bool isDead = false;
    private int noSpawned;

    protected override void Start()
    {
        base.Start();
        powerUpGate = GameObject.Find("ClosedGateDrillBossPowerUp");
        roomGate = GameObject.Find("GateCloseInstantly");
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
        noSpawned = 0;
        Invoke("CreateProjectiles", 0.1f);
    }


    private void CreateProjectiles()
    {
        GameObject go = Instantiate(ProjectilePrefab, GetRandomPoint(), Quaternion.AngleAxis(-90f, Vector3.forward));
        go.transform.GetChild(Random.Range(0, go.transform.childCount)).gameObject.SetActive(true);
        go.GetComponent<ProjectileScript>().SetProjectile((int)longDamage, ProjectileSpeed);
        noSpawned ++;
        if(noSpawned < NoOfProjectiles)
        {
            Invoke("CreateProjectiles", 0.4f);
        }
    }

    private Vector2 GetRandomPoint()
    {
        return new Vector2(transform.GetChild(0).position.x + ProjectileOrigin.x + (Random.Range(0,size.x) - (size.x / 2 ) ), ProjectileOrigin.y + (Random.Range(0, size.y) - (size.y / 2)));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.GetChild(1).position, MeleeAttackDamageRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(ProjectileOrigin + (Vector2)transform.GetChild(0).position, size);
    }
}
