using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODPlayer : MonoBehaviour
{
    void PlayFootsteps(string path)
    {
        FMOD.Studio.EventInstance Footsteps = FMODUnity.RuntimeManager.CreateInstance(path);
        Footsteps.start();
        Footsteps.release();
    }

    void PlayJump(string path)
    {
        FMOD.Studio.EventInstance Jump = FMODUnity.RuntimeManager.CreateInstance(path);
        Jump.start();
        Jump.release();
    }
    void PlayDeath(string path)
    {
        FMOD.Studio.EventInstance Death = FMODUnity.RuntimeManager.CreateInstance(path);
        Death.start();
        Death.release();
    }
}
