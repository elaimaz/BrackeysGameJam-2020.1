using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public static Transform PlayerPos;
    public LayerMask PlayerLayer;
    public float speed;
    public float searchRange;
    public float meleeRange;
    public float meleeDamage;
    public float longRange;
    public float longDamage;
    public float health;

    [Header("While Taking Damage")]
    public float tStartDazed;
    private float tDazed;
    private float oriSpeed;
    public bool isPlayerInRange = false;

    protected Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPos == null)
        {
            PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
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

    public virtual void OnDamageTaken(float damage)
    {
        anime.SetTrigger("isTakingDamage");
        tDazed = tStartDazed;
       
    }
}
