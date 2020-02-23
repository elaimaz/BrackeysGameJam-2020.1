using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close : MonoBehaviour
{
    public void onExitCall()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Click");
        Application.Quit();
    }
}
