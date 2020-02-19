using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODRangedWeaponPlayer : MonoBehaviour
{
    void PlayRanged(string path)
    {
        Debug.Log("play charged");
        FMOD.Studio.EventInstance PlayRanged = FMODUnity.RuntimeManager.CreateInstance(path);
        PlayRanged.start();
        PlayRanged.release();
    }
}
