using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public int MaxPlayerHealth;
    public int PlayerHealth;

    [System.Serializable]
    public class IntEvent : UnityEvent<int> { }

    public IntEvent OnSetMaxHealth;
    public IntEvent OnHealthChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        PlayerHealth = Mathf.Clamp(PlayerHealth - damage, 0, MaxPlayerHealth);
    }

    public void GainHealth(int health)
    {
        PlayerHealth = Mathf.Clamp(PlayerHealth + health, 0, MaxPlayerHealth);
    }

    
}
