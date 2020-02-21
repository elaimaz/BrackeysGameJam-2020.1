using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MyBox;

public class PowerUpBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    public GameObject player;
    
    public int NumKeyPressToActivate;
    
    public bool PortalJumpPowerUp;
    [ConditionalField("PortalJumpPowerUp")]
    public float jumpVelocity;
    
    public bool SpeedPowerUp;
    [ConditionalField("SpeedPowerUp")]
    [Range(0, 10)]
    public float SpeedValue = 6f;
    
    public bool ShieldPowerUp;
    [ConditionalField("ShieldPowerUp")]
    [Range(5, 30)]
    public float ShieldValue = 6f;
    
    public bool usePowerUpDelay;
    [ConditionalField("usePowerUpDelay")]
    [Range(0, 50)]
    public float PowerUpDelayTime = 10f;
    
    [Tooltip("Using this feature will let you set the duration of the effect to last. Setting it to False will let it last for a lifetime.")]
    public bool useActiveTime = true;
    [ConditionalField("useActiveTime")]
    [Range(0, 30)]
    public float ActiveDuration = 10;
    
    public bool useCoolDown = true;
    [ConditionalField("useCoolDown")]
    [Range(0, 80)]
    public float coolDownTime = 0.0f;
    
    private bool isCoolingDown = true;
    private float startTime = 0.0f;
    
    private PlayerController playerControllerScript;
    
    [Tooltip("ReducePartially set to false will reset upon powerup activation.")]
    public bool ReducePartially = false;
    [ConditionalField("ReducePartially")]
    [Range(1, 100)]
    public int ReduceByValue = 30;
    
    //Ready=true just means that the player can now apply powerup.
    bool ready;
    
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
        
        if (!ReducePartially)
             if (slider.value == slider.maxValue)
                ready = true;
        else if (slider.value >= ReduceByValue)
            ready = true;
    }
    
    void activatePowerUp(){
        ready = false;
        if (!ReducePartially) slider.value = 0;
        else slider.value -= ReduceByValue;
        
        if(PortalJumpPowerUp){
            playerControllerScript.OnPortalJumpPowerUPActivated();
            StartCoroutine(ResetPortalJumpAtribute());
            StartCoroutine(JumpPortalRoutine());
        }
        if(ShieldPowerUp){
            playerControllerScript.moveSpeed = SpeedValue;
        }
    }
    
    
    public void Start(){
        playerControllerScript = player.GetComponent<PlayerController>();
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
    
    /******************************************************/
    private IEnumerator ResetPortalJumpAtribute()
    {
        yield return new WaitForSeconds(PowerUpDelayTime + 1.5f);
        playerControllerScript.jumpVelocity = playerControllerScript.defaultJumpVelocity;
    }

    private IEnumerator JumpPortalRoutine()
    {
        yield return new WaitForSeconds(ActiveDuration);
        isCoolingDown = true;
    }

    private IEnumerator ResetShieldAtribute()
    {
        yield return new WaitForSeconds(PowerUpDelayTime + 1.5f);
        //Reset Shield.
    }
    
    private IEnumerator ShieldPortalRoutine()
    {
        yield return new WaitForSeconds(ActiveDuration);
        isCoolingDown = true;
    }

    private IEnumerator ResetSpeedAtribute()
    {
        yield return new WaitForSeconds(PowerUpDelayTime + 1.5f);
        playerControllerScript.moveSpeed =  playerControllerScript.defaultMoveSpeed;
    }

    private IEnumerator SpeedPortalRoutine()
    {
        yield return new WaitForSeconds(ActiveDuration);
        isCoolingDown = true;
    }
    /******************************************************/
}
