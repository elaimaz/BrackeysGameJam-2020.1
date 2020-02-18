using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODRangedWeaponPlayer : MonoBehaviour
{
    void PlayRanged(string path)
    {
        FMOD.Studio.EventInstance PlayRanged = FMODUnity.RuntimeManager.CreateInstance(path);
        PlayRanged.start();
        PlayRanged.release();
    }
}
