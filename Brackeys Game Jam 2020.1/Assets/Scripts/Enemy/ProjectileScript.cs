using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public int Damage;
    public void SetProjectile(int _damage, float Speed)
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * Speed;
        Damage = _damage;
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerManager>().TakeDamage(Damage);
        }
        if (collision.tag == "Enemy")
            return;
        Destroy(gameObject);
    }
}
