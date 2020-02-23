using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(PlayerManager.instance.gameObject);
        Destroy(GameManager.instance.gameObject);
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Click");
    }

    public void GotoLevel()
    {
        SceneManager.LoadScene("Levels");
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Click");
    }
}
