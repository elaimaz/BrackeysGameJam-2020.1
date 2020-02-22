using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossEnemy : EnemyBase
{
    public GameObject ProjectilePrefab;
    public float PullForce;

    [Header("Location for Spawn random Enemy")]
    public Vector2 ProjectileOrigin;
    public Vector2 size;
    public int NoOfSpawns;
    [Space()]
    public float MeleeAttackDamageRadius;
    public float ProjectileSpeed;

    private bool isDead = false;
    private int noSpawned;
    private bool isPulling;
    private Vector2 forceVec;
    private Rigidbody2D rb;

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

    private void FixedUpdate()
    {
        if (isPulling)
        {
            rb.AddForce(forceVec, ForceMode2D.Force);
        }
    }

    public void OnStartRanged()
    {
        isPulling = true;
        forceVec = (transform.GetChild(0).position - PlayerManager.instance.transform.position ).normalized * PullForce;
        rb = PlayerManager.instance.GetComponent<Rigidbody2D>();
    }

    public void OnEndRanged()
    {
        isPulling = false;
    }

    public void StartSpawningEnemies()
    {
        noSpawned = 0;
        Invoke("CreateProjectiles", 0.1f);
    }


    private void CreateProjectiles()
    {
        GameObject go = Instantiate(ProjectilePrefab, GetRandomPoint(), Quaternion.identity);
        go.transform.GetChild(Random.Range(0, go.transform.childCount)).gameObject.SetActive(true);
        go.GetComponent<ProjectileScript>().SetProjectile((int)longDamage, ProjectileSpeed);
        noSpawned++;
        if (noSpawned < NoOfSpawns)
        {
            Invoke("CreateProjectiles", 0.4f);
        }
    }

    private Vector2 GetRandomPoint()
    {
        return new Vector2(transform.GetChild(0).position.x + ProjectileOrigin.x + (Random.Range(0, size.x) - (size.x / 2)), ProjectileOrigin.y + (Random.Range(0, size.y) - (size.y / 2)));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.GetChild(1).position, MeleeAttackDamageRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(ProjectileOrigin + (Vector2)transform.GetChild(0).position, size);
    }
}
