using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class speedBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public GameObject player;

    private float startTime = 0.0f;

    public void Start()
    {
        fill.color = Color.green;
        slider.maxValue = player.GetComponent<PlayerController>().speedPowerUpResetTime;
        slider.value = slider.maxValue;
    }

    public void Update()
    {
        if (player.GetComponent<PlayerController>().speedPowerCooldown == false)
        {
            startTime += Time.deltaTime;
            slider.value = Mathf.Clamp(startTime, 0, slider.maxValue);

            if (startTime <= player.GetComponent<PlayerController>().speedPowerUpTime)
            {
                fill.color = Color.green;
            }
            else if (startTime > player.GetComponent<PlayerController>().speedPowerUpTime && startTime < slider.maxValue)
            {
                fill.color = Color.white;
            }
            else if (startTime >= slider.maxValue)
            {
                fill.color = Color.green;
                startTime = 0.0f;
            }
        }
    }
}
