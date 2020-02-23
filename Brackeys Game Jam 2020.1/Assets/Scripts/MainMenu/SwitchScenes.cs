using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Click");
    }

    public void GotoLevel()
    {
        SceneManager.LoadScene("Levels");
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Click");
    }
}
