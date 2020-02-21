using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public int Damage;
    public void SetBullete(int _damage, float BulleteSpeed)
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * BulleteSpeed;
        Damage = _damage;
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            if(collision.GetComponent<EnemyBase>() != null)
                collision.GetComponent<EnemyBase>().OnDamageTaken(Damage);
        }
        Destroy(gameObject);
    }
}
