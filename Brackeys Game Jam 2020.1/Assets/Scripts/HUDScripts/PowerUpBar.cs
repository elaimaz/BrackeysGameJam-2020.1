using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    public void Start(){
        fill.color = gradient.Evaluate(slider.value / slider.normalizedValue);
    }
    
    public void SetMaxPoints(int points){
    
        slider.maxValue = points;
        
        fill.color = gradient.Evaluate(slider.value / slider.normalizedValue);
    }
    
    public void SetPoints(int points){
        slider.value = points;
        
        fill.color = gradient.Evaluate(slider.value / slider.normalizedValue);
    }
    
    public void AddPoints(int points){
        if ((slider.value + points)> slider.maxValue){
            slider.value = slider.maxValue;
        }
        else slider.value += points;
        
        fill.color = gradient.Evaluate(slider.value / slider.normalizedValue);
    }
    
    public void RemovePoints(int points){
        if ((slider.value - points) <= 0){
            slider.value = 0;
        }
        else slider.value -= points;
        
        fill.color = gradient.Evaluate(slider.value / slider.normalizedValue);
    }
    
    public void ResetToZero(){
        slider.value = 0;
        fill.color = gradient.Evaluate(slider.value / slider.normalizedValue);
    }
}
