using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public static Transform PlayerPos;
    public LayerMask PlayerLayer;
    public float speed;
    public int health;
    public float searchRange;
    [Header("Melee Attack Info")]
    public float meleeRange;
    public int meleeDamage;
    public float meleeRate;
    [Header("Ranged Attack Info")]
    public float longRange;
    public float longDamage;
    public float longRate;

    [Header("Freez After Taking Damage")]
    public float tStartDazed;
    [Space()]
    private float tDazed;
    private float oriSpeed;
    private bool isPlayerInRange = false;

    [Range(1, 100)]
    public int TouchDamage;

    protected Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPos == null)
        {
            PlayerPos = PlayerManager.instance.transform;
        }
        anime = GetComponent<Animator>();
        oriSpeed = speed;
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position,PlayerPos.position) <= searchRange && isPlayerInRange == false)
        {
            anime.SetBool("isInRange", true);
            isPlayerInRange = true;
        }
        else if (Vector2.Distance(transform.position, PlayerPos.position) > searchRange && isPlayerInRange == true)
        {
            anime.SetBool("isInRange", false);
            isPlayerInRange = false;
        }

        if(tDazed <= 0)
        {
            speed = oriSpeed;
            if(isPlayerInRange)
                anime.SetBool("isInRange", true);
        }
        else
        {
            speed = 0;
            tDazed -= Time.deltaTime;
        }
    }

    public virtual void OnMeleeAttackDone()
    {

    }
    public virtual void OnRangedAttackDone()
    {

    }

    public virtual void OnDamageTaken(int damage)
    {
        tDazed = tStartDazed;
        anime.SetBool("isInRange", false);
    }

    public virtual void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
