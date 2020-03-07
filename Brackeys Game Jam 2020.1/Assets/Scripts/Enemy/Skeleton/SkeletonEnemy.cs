using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemy : EnemyBase
{
    public GameObject BonePrefab;
    public float BoneSpeed;
    public override void OnDamageTaken(int damage)
    {
        base.OnDamageTaken(damage);
        health -= damage;
        Debug.Log("Taken " + damage + " health : " + health);
        if (health <= 0)
        {
            disableCollisionAttack = true;
            Instantiate(healthPrefab, gameObject.transform.position, Quaternion.identity);
            anime.SetTrigger("Death");
            Destroy(gameObject, 1f);
        }
        else
        {
            anime.SetTrigger("isTakingDamage");
        }
    }


    public override void OnRangedAttackDone()
    {
        base.OnRangedAttackDone();
        Instantiate(BonePrefab, transform.GetChild(0).position, transform.GetChild(0).rotation).GetComponent<ProjectileScript>().SetProjectile((int)longDamage,BoneSpeed);
    }
}
