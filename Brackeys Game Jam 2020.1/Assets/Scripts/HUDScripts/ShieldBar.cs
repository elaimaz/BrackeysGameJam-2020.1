using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public GameObject player;

    private float startTime = 0.0f;

    public void Start()
    {
        fill.color = Color.red;
        slider.maxValue = player.GetComponent<PlayerController>().shieldPowerUpResetTime;
        slider.value = slider.maxValue;
    }

    public void Update()
    {
        if (player.GetComponent<PlayerController>().shieldPowerCooldown == false)
        {
            startTime += Time.deltaTime;
            slider.value = Mathf.Clamp(startTime, 0, slider.maxValue);

            if (startTime <= player.GetComponent<PlayerController>().shieldPowerUpTime)
            {
                fill.color = Color.red;
            }
            else if (startTime > player.GetComponent<PlayerController>().shieldPowerUpTime && startTime < slider.maxValue)
            {
                fill.color = Color.white;
            }
            else if (startTime >= slider.maxValue)
            {
                fill.color = Color.red;
                startTime = 0.0f;
            }
        }
    }
}