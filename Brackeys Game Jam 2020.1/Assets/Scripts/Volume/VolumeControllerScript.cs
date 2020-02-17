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
    }

    public void updateMusicVolume()
    {
        musicVolume = (int) musicSlider.value;
        musicValueText.text = "" + musicVolume;
    }
    
    public void updateEffectsVolume()
    {
        effectsVolume = (int) effectsSlider.value;
        effectsValueText.text = "" + effectsVolume;        
    }
}
