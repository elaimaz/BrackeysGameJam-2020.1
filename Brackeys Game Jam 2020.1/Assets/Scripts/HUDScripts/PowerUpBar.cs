﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MyAttributes;

[ExecuteInEditMode]
public class PowerUpBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    public GameObject player;
    
    public int NumKeyPressToActivate;
    
    [Space(15)]
    public bool PortalJumpPowerUp;
    [ConditionalField("PortalJumpPowerUp")]
    [Range(0, 15)]
    public float jumpVelocity = 10f;
    
    public bool SpeedPowerUp;
    [ConditionalField("SpeedPowerUp")]
    [Range(0, 10)]
    public float SpeedValue = 6f;
    
    public bool ShieldPowerUp;
    [ConditionalField("ShieldPowerUp")]
    [Range(5, 30)]
    public float ShieldValue = 6f;
    
    [Space(15)]
    public bool usePowerUpDelay;
    [ConditionalField("usePowerUpDelay")]
    [Range(0, 50)]
    public float PowerUpDelayTime = 10f;
    
    [Tooltip("Using this feature will let you set the duration of the effect to last. Setting it to False will let it last for a lifetime.")]
    public bool useActiveTime;
//    [Tooltip("Use this to control how long the PowerUp is to last.")]
    [ConditionalField("useActiveTime")]
    [Range(0, 30)]
    public float ActiveDuration = 10;
    
    [Tooltip("Using this feature will let the powerBar increase upon set time. Disable to not increase based on time.")]
    public bool useCoolDown;
//    [Tooltip("Use this to control how long the PowerUp takes going from 0 to maxValue.")]
    [ConditionalField("useCoolDown")]
    [Range(1, 80)]
    public float coolDownTime = 10.0f;
    
//    private bool isCoolingDown = true;
    private float startTime = 0.0f;
    
    private PlayerController playerControllerScript;
    private PlayerManager playerManagerScript;
    
    [Tooltip("ReducePartially set to false will reset upon powerup activation.")]
    public bool ReducePartially = false;
    [ConditionalField("ReducePartially")]
    [Range(1, 100)]
    public int ReduceByValue = 30;
    
    //Ready=true just means that the player can now apply powerup.
    bool ready;
    
    public Text text;
    private string ReadyText = "Press {} Ready!";
    
    public void Update(){
        if (ready && playerControllerScript.isGrounded)
        if ((Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0)) && (NumKeyPressToActivate == 0)) {
            activatePowerUp();
        } else if ((Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) && (NumKeyPressToActivate == 1)) {
            activatePowerUp();
        } else if ((Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) && (NumKeyPressToActivate == 2)) {
            activatePowerUp();
        } else if ((Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)) && (NumKeyPressToActivate == 3)) {
            activatePowerUp();
        } else if ((Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4)) && (NumKeyPressToActivate == 4)) {
            activatePowerUp();
        } else if ((Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5)) && (NumKeyPressToActivate == 5)) {
            activatePowerUp();
        } else if ((Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6)) && (NumKeyPressToActivate == 6)) {
            activatePowerUp();
        } else if ((Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7)) && (NumKeyPressToActivate == 7)) {
            activatePowerUp();
        } else if ((Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8)) && (NumKeyPressToActivate == 8)) {
            activatePowerUp();
        } else if ((Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9)) && (NumKeyPressToActivate == 9)) {
            activatePowerUp();
        }
    }
    
    void FixedUpdate(){
        
        if (useCoolDown)
            //Increase powerUp based on cooldown value.
            
            slider.value += (1f/coolDownTime)*slider.maxValue*Time.fixedDeltaTime;
    
        //Code to set to ready.
        if (!ReducePartially)
            //Check if bar is full.
            if (slider.value == slider.maxValue)
                ready = true;
            else ready = false;
        else 
            //Check if bar is full enough to use powerup.
            if (slider.value >= ReduceByValue)
                ready = true;
            else ready = false;
            
        ReadyText = "Press " + NumKeyPressToActivate + " Ready!";
        if (ready == true){
            text.gameObject.SetActive(true);
            text.text = ReadyText;
        }
        else text.gameObject.SetActive(false);
        
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    
    void activatePowerUp(){
        //Assume Player not ready to use next powerup.
        ready = false;
        
        //Resets to 0, or reduces by value, depending on ReducePartially.
        if (!ReducePartially) slider.value = 0;
        else slider.value -= ReduceByValue;
        
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/PortalSwitch");
        
        /*This is where we activate powerups.
        Note: we can use 2 powerups in one bar as well, 
        but with same delay and activation time.*/
        if(PortalJumpPowerUp && playerManagerScript.haveJumpPowerUp == true){
            playerControllerScript.jumpVelocity = jumpVelocity;
            playerControllerScript.OnPortalJumpPowerUPActivated();
            StartCoroutine(ResetPortalJumpAtribute());
//            StartCoroutine(JumpPortalRoutine());
        }
        if(SpeedPowerUp && playerManagerScript.haveSpeedPowerUp == true){
            playerControllerScript.moveSpeed = SpeedValue;
            playerControllerScript.OnSpeedPowerUPActivated();
            StartCoroutine(ResetSpeedAtribute());
//            StartCoroutine(SpeedPortalRoutine());
        }
        if(ShieldPowerUp && playerManagerScript.haveShieldPowerUp == true){
            playerManagerScript.shieldActive = true;
            playerControllerScript.OnShieldPowerUPActivated();
            StartCoroutine(ResetShieldAtribute());
//            StartCoroutine(ShieldPortalRoutine());
        }
    }
    
    public void Start(){
        playerControllerScript = player.GetComponent<PlayerController>();
        playerManagerScript = player.GetComponent<PlayerManager>();
        
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    
    public void SetMaxPoints(int points){
    
        slider.maxValue = points;
        
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    
    public void SetPoints(int points){
        slider.value = points;
        
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    
    public void AddPoints(int points){
        if ((slider.value + points)> slider.maxValue){
            slider.value = slider.maxValue;
        }
        else slider.value += points;
        
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    
    public void RemovePoints(int points){
        if ((slider.value - points) <= 0){
            slider.value = 0;
        }
        else slider.value -= points;
        
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    
    public void ResetToZero(){
        slider.value = 0;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    
    /***********************Coroutines**********************/
    private IEnumerator ResetPortalJumpAtribute()
    {
        yield return new WaitForSeconds(PowerUpDelayTime + 1.5f);
        playerControllerScript.jumpVelocity = playerControllerScript.defaultJumpVelocity;
    }

    private IEnumerator ResetShieldAtribute()
    {
        yield return new WaitForSeconds(PowerUpDelayTime + 1.5f);
        playerManagerScript.shieldActive = false;
    }

    private IEnumerator ResetSpeedAtribute()
    {
        yield return new WaitForSeconds(PowerUpDelayTime + 1.5f);
        playerControllerScript.moveSpeed =  playerControllerScript.defaultMoveSpeed;
    }
    /******************************************************/
}
