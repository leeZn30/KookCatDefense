using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Button openButton;
    public Button exitButton;
    public GameObject backPanel;
    public GameObject panel;


    public Slider masterSoundSlider;
    public Slider bgmSlider;
    public Slider gameSFXSlider;
    public Slider sfxSlider;

    // Start is called before the first frame update
    void Start()
    {
        openButton.onClick.AddListener(delegate { OpenPanel(true); }) ;
        exitButton.onClick.AddListener(delegate { OpenPanel(false); });
        masterSoundSlider.value = AudioListener.volume;
        bgmSlider.value = SoundManager.Instance.BGMVolume;
        gameSFXSlider.value = SoundManager.Instance.GameSFXVolume;
        sfxSlider.value = SoundManager.Instance.SFXVolume;
    }
    private void Update()
    {
        SoundManager.Instance.SetMasterVolume(masterSoundSlider.value);
        SoundManager.Instance.BGMVolume= bgmSlider.value;
        SoundManager.Instance.GameSFXVolume=gameSFXSlider.value;
        SoundManager.Instance.SFXVolume=sfxSlider.value; 
    }
    private void OpenPanel(bool isOpen)
    {
        SoundManager.Instance.PlaySFX(SFX.ButtonClick);
        backPanel.SetActive(isOpen);
        panel.SetActive(isOpen);
    }

}
