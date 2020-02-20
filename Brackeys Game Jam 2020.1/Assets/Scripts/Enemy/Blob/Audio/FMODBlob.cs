using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODBlob : MonoBehaviour
{
    void PlayFootsteps(string path)
    {
        FMOD.Studio.EventInstance Footsteps = FMODUnity.RuntimeManager.CreateInstance(path);
        Footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        Footsteps.start();
        Footsteps.release();
    }
    void PlayDeath(string path)
    {
        FMOD.Studio.EventInstance Death = FMODUnity.RuntimeManager.CreateInstance(path);
        Death.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        Death.start();
        Death.release();
    }
}
