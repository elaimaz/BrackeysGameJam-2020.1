using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public float rps;
    public int damage;
    public Vector2 DetectionZone;
    public Transform AttackOrigin;
    public LayerMask ememyLayer;

    public Animator playerAnime;
    public Animator anime;

    private float tLastFire;
    private float tNextFire;


    // Update is called once per frame
    void Update()
    {
        tLastFire += Time.deltaTime;

        if(Input.GetButtonDown("Fire1"))
        {
            if (tLastFire >= tNextFire)
            {
                tNextFire = 1f / rps;
                tLastFire = 0.0F;
                Shoot();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(AttackOrigin.position, DetectionZone);
    }

    private void Shoot()
    {
        anime.SetTrigger("isAttacking");
        playerAnime.SetTrigger("isAttacking");
    }

    public void CheckForHit()
    {
        //GetComponent<BoxCollider2D>().enabled = true;
        Collider2D coll = Physics2D.OverlapBox(AttackOrigin.position, DetectionZone, 0 , ememyLayer);
        if ( coll != null)
        {
            coll.GetComponent<EnemyBase>().OnDamageTaken(damage);
        }
    }
}
