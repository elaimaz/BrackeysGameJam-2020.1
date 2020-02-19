using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public int MaxPlayerHealth;
    public float PlayerHealth;

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

    private void Start()
    {
        if (OnSetMaxHealth != null)
            OnSetMaxHealth.Invoke(MaxPlayerHealth);
            
    }

    public void TakeDamage(float damage)
    {
        PlayerHealth = Mathf.Clamp(PlayerHealth - damage, 0, MaxPlayerHealth);
        if(OnHealthChanged != null) 
            OnHealthChanged.Invoke((int)PlayerHealth);
    }
}
