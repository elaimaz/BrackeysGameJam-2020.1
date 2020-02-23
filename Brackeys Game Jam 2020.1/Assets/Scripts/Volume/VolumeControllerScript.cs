using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControllerScript : MonoBehaviour
{
    public Slider musicSlider;
    public Slider effectsSlider;
    
    public Text musicValueText;
    public Text effectsValueText;

    public VCA fxVCA;
    public VCA musicVCA;

    [Range(1, 100)]
    public int musicVolume = 56;
    
    [Range(1, 100)]
    public int effectsVolume = 60;
    
    void Start()
    {
        musicSlider.value = musicVolume;
        effectsSlider.value = effectsVolume;
        
        musicValueText.text = "" + musicVolume;
        effectsValueText.text = "" + effectsVolume;

        string fxVCAPath = "vca:/FX";
        fxVCA = FMODUnity.RuntimeManager.GetVCA(fxVCAPath);
        string musicVCAPath = "vca:/Music";
        musicVCA = FMODUnity.RuntimeManager.GetVCA(musicVCAPath);
    }

    public void updateMusicVolume()
    {
        musicVolume = (int) musicSlider.value;
        musicValueText.text = "" + musicVolume;
        musicVCA.setVolume(musicSlider.value / 100);
    }
    
    public void updateEffectsVolume()
    {
        effectsVolume = (int) effectsSlider.value;
        effectsValueText.text = "" + effectsVolume;
        fxVCA.setVolume(effectsSlider.value / 100);
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/SoundTest");
    }
}
