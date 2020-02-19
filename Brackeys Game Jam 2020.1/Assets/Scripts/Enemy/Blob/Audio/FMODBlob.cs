using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODBlob : MonoBehaviour
{
    void PlayFootsteps(string path)
    {
        FMOD.Studio.EventInstance Footsteps = FMODUnity.RuntimeManager.CreateInstance(path);
        Footsteps.start();
        Footsteps.release();
    }
    void PlayDeath(string path)
    {
        Debug.Log(path);
        FMOD.Studio.EventInstance Death = FMODUnity.RuntimeManager.CreateInstance(path);
        Death.start();
        Death.release();
    }
}
