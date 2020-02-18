using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public int currWeapon = 0;
    public GameObject MeleeWeapon;
    public GameObject RangedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && currWeapon != 0)
        {
            currWeapon = 0;
            ChangeWeapon();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && currWeapon != 1)
        {
            currWeapon = 1;
            ChangeWeapon();
        }
    }

    private void ChangeWeapon()
    {
        //TODO: ADD Gun Changing sound here
        if(currWeapon == 0)
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
}
