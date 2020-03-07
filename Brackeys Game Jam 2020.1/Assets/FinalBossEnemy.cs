using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossEnemy : EnemyBase
{
    public float PullForce;

    [Header("Location for Spawn random Enemy")]
    public int DoAtHealth;
    public Vector2 ProjectileOrigin;
    public Vector2 size;
    public int NoOfSpawns;
    public GameObject[] Enemies;
    public float checkingRadius;
    public LayerMask LayersToCheck;
    [Space()]
    public float MeleeAttackDamageRadius;

    private bool isDead = false;
    private int noSpawned;
    private bool isPulling;
    private Vector2 forceVec;
    private Rigidbody2D rb;
    private bool EnemiesSpawned;

    public override void OnDamageTaken(int damage)
    {
        base.OnDamageTaken(damage);
        health -= damage;
        isPulling = false;
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

            //if(health <= DoAtHealth && EnemiesSpawned == false)
            //{
            //    StartSpawningEnemies();
            //    EnemiesSpawned = true;
            //}
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
        GameObject go = Instantiate(Enemies[Random.Range(0, Enemies.Length)], GetRandomPoint(), Quaternion.identity);
        noSpawned++;
        if (noSpawned < NoOfSpawns)
        {
            Invoke("CreateProjectiles", 0.4f);
        }
    }

    private Vector2 GetRandomPoint()
    {
        Vector2 pos = new Vector2();
        while(true)
        {
            pos.Set(transform.GetChild(2).position.x + ProjectileOrigin.x + (Random.Range(0, size.x) - (size.x / 2)), transform.GetChild(2).position.y + ProjectileOrigin.y + (Random.Range(0, size.y) - (size.y / 2)));
            if (Physics2D.OverlapCircle(pos, checkingRadius, LayersToCheck) == null)
                return pos;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.GetChild(1).position, MeleeAttackDamageRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(ProjectileOrigin + (Vector2)transform.GetChild(2).position, size);
    }

}
