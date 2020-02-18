using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODWeaponPlayer : MonoBehaviour
{
    void PlayMelee(string path)
    {
        FMOD.Studio.EventInstance Melee = FMODUnity.RuntimeManager.CreateInstance(path);
        Melee.start();
        Melee.release();
    }
}
