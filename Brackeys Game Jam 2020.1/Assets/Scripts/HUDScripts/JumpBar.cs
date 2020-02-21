using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public GameObject player;

    private float startTime = 0.0f;

    public void Start()
    {
        fill.color = Color.blue;
        slider.maxValue = player.GetComponent<PlayerController>().jumpPowerUpResetTime;
        slider.value = slider.maxValue;
    }

    public void Update()
    {
        if (player.GetComponent<PlayerController>().jumpPowerCooldown == false)
        {
            startTime += Time.deltaTime;
            slider.value = Mathf.Clamp(startTime, 0, slider.maxValue);
            
            if (startTime <= player.GetComponent<PlayerController>().jumpPowerUpTime)
            {
                fill.color = new Color(0, 0, 0.5f);
            }else if (startTime > player.GetComponent<PlayerController>().jumpPowerUpTime && startTime < slider.maxValue)
            {
                fill.color = Color.white;
            }else if (startTime >= slider.maxValue)
            {
                fill.color = Color.blue;
                startTime = 0.0f;
            }
        }
    }
}