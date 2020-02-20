using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public float pushBackForce;
    public int MaxPlayerHealth;
    public int PlayerHealth;

    [System.Serializable]
    public class IntEvent : UnityEvent<int> { }

    public IntEvent OnSetMaxHealth;
    public IntEvent OnHealthChanged;
    private Vector2 forceVec;
    private bool isInEnemyRange;
    private Rigidbody2D rb;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            rb = GetComponent<Rigidbody2D>();
        }
        else
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if(isInEnemyRange)
        {
            rb.AddForce(forceVec, ForceMode2D.Force);
        }
    }

    public void TakeDamage(int damage)
    {
        PlayerHealth = Mathf.Clamp(PlayerHealth - damage, 0, MaxPlayerHealth);
    }

    public void GainHealth(int health)
    {
        PlayerHealth = Mathf.Clamp(PlayerHealth + health, 0, MaxPlayerHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            forceVec = (collision.transform.position - transform.position).normalized * pushBackForce * -1f;
            isInEnemyRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            isInEnemyRange = false;
        }
    }
}
