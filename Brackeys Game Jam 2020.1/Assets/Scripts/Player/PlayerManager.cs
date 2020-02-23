using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerController playerControler;
    public float pushBackForce;
    public int MaxPlayerHealth;
    public int PlayerHealth;
    public bool shieldActive = false;
    public bool playerAlive = true;

    [System.Serializable]
    public class IntEvent : UnityEvent<int> { }

    public IntEvent OnSetMaxHealth;
    public IntEvent OnHealthChanged;
    private Vector2 forceVec;
    private bool isInEnemyRange;
    private Rigidbody2D rb;

    [Range(0.1f, 1)]
    public float touchDamageTimeRate;
    public bool canTouchDamage = true;
    
    //We dont need these bools: just enable or disable PowerUp bar slider when boss is killed.
//    //Bools to check if the player has really gotten the power after kill boss in order to use power up
    public bool haveJumpPowerUp = false;
    public bool haveShieldPowerUp = false;
    public bool haveSpeedPowerUp = false;

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
        if (shieldActive == false)
        {
            PlayerHealth = Mathf.Clamp(PlayerHealth - damage, 0, MaxPlayerHealth);
            if (PlayerHealth <= 0 && playerAlive == true)
            {
                playerAlive = false;
                playerControler.PlayerDeath();
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Damage");
            }
        }
    }

    public void GainHealth(int health)
    {
        PlayerHealth = Mathf.Clamp(PlayerHealth + health, 0, MaxPlayerHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            forceVec = (collision.transform.position - transform.position).normalized * pushBackForce * -1f;
            isInEnemyRange = true;
            if (collision.GetComponent<EnemyBase>() != null)
            {
                if (canTouchDamage == true)
                {
                    TakeDamage(collision.gameObject.GetComponent<EnemyBase>().TouchDamage);
                    StartCoroutine(TouchDamageReset());
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            isInEnemyRange = false;
        }
    }

    protected IEnumerator TouchDamageReset()
    {
        canTouchDamage = false;
        yield return new WaitForSeconds(touchDamageTimeRate);
        canTouchDamage = true;
    }
}
