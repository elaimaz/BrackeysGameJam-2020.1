using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public static Transform PlayerPos;
    public float speed;
    public float searchRange;
    public float meleeRange;
    public float meleeDamage;
    public float longRange;
    public float longDamage;
    public float health;
    public bool isPlayerInRange = false;

    private Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPos == null)
        {
            PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        }
        anime = GetComponent<Animator>();
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
    }

    public virtual void OnDamageTaken(int damage)
    {
        anime.SetTrigger("isTakingDamage");
    }
}
