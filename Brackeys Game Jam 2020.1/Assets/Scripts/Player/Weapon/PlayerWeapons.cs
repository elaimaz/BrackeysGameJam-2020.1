using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeapons : MonoBehaviour
{
    public int currWeapon = 0;
    public GameObject MeleeWeapon;
    public GameObject RangedWeapon;
    public GameObject ArrowRef;
    public float tStartChargeUp;
    public float MinDamage;
    public float MaxDamage;

    [System.Serializable]
    public class FloatEvent : UnityEvent<float> { }

    public FloatEvent shootEvent;
    private float tChargeUp;
    private bool isHolding;

    [FMODUnity.EventRef]
    public string ChargeStateEvent = "";
    FMOD.Studio.EventInstance chargeState;

    // Start is called before the first frame update
    void Start()
    {
        chargeState = FMODUnity.RuntimeManager.CreateInstance(ChargeStateEvent);
    }

    void OnDestroy()
    {
        chargeState.release();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(1))
        {
            currWeapon = 1;
            ChangeWeapon();
        }
        else if(Input.GetMouseButtonUp(1))
        {
            currWeapon = 0;
            ChangeWeapon();
            isHolding = false;
        }
        
        if(Input.GetMouseButtonDown(0))
        {
            tChargeUp = tStartChargeUp;
            isHolding = true;
            ArrowRef.SetActive(true);
            if (currWeapon == 1)
            {
                chargeState.start();
            }
        }
        else if(Input.GetMouseButtonDown(0))
        {

        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(isHolding)
            {
                chargeState.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                shootEvent.Invoke(Remap(tStartChargeUp - tChargeUp, 0, tStartChargeUp, MinDamage, MaxDamage));
            }
            isHolding = false;
            ArrowRef.SetActive(false);
        }

        if (tChargeUp <= 0 || tChargeUp > tStartChargeUp)
        {
            //Do Shoot With full power
            if (isHolding)
            {
                chargeState.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                shootEvent.Invoke(Remap(tStartChargeUp - tChargeUp, 0, tStartChargeUp, MinDamage, MaxDamage));
            }
            isHolding = false;
            ArrowRef.SetActive(false);
        }
        else
        {
            if (isHolding == true)
                tChargeUp -= Time.deltaTime;
            else
                tChargeUp += Time.deltaTime;
        }
    }

    private void ChangeWeapon()
    {
        //TODO: ADD Gun Changing sound here
        if (currWeapon == 0)
        {
            RangedWeapon.SetActive(false);
            MeleeWeapon.SetActive(true);
        }
        else if(currWeapon == 1)
        {
            RangedWeapon.SetActive(true);
            MeleeWeapon.SetActive(false);
        }
    }

    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
