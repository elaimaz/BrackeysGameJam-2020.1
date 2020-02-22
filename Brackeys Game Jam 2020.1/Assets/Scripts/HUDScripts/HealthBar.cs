using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    public GameObject player;
    
    public void Start(){
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    
    public void Update(){
        slider.maxValue = player.GetComponent<PlayerManager>().MaxPlayerHealth;
        slider.value = Mathf.Clamp(player.GetComponent<PlayerManager>().PlayerHealth, 0, slider.maxValue);
        
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
