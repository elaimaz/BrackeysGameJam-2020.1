using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    public void Start(){
        fill.color = gradient.Evaluate(slider.value / slider.normalizedValue);
    }
    
    public void SetMaxHealth(int health){
    
        slider.maxValue = health;
        
        fill.color = gradient.Evaluate(slider.value / slider.normalizedValue);
    }
    
    public void SetHealth(int health){
        slider.value = health;
        
        fill.color = gradient.Evaluate(slider.value / slider.normalizedValue);
    }
    
    public void AddHealth(int health){
        if ((slider.value + health)> slider.maxValue){
            slider.value = slider.maxValue;
        }
        else slider.value += health;
        
        fill.color = gradient.Evaluate(slider.value / slider.normalizedValue);
    }
    
    public void RemoveHealth(int health){
        if ((slider.value - health) <= 0){
            slider.value = 0;
        }
        else slider.value -= health;
        
        fill.color = gradient.Evaluate(slider.value / slider.normalizedValue);
    }
}
