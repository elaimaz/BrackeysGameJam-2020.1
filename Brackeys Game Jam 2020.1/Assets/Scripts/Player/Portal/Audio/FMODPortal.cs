using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODPortalPlayer : MonoBehaviour
{
    void PlayPortal(string path)
    {
        FMOD.Studio.EventInstance Portal = FMODUnity.RuntimeManager.CreateInstance(path);
        Portal.start();
        Portal.release();
    }
}
