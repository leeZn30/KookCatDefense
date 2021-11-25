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

    public Dropdown ResolutionDropdown;

    // Start is called before the first frame update
    void Start()
    {
        openButton.onClick.AddListener(delegate { OpenPanel(true); }) ;
        exitButton.onClick.AddListener(delegate { OpenPanel(false); });
        masterSoundSlider.value = AudioListener.volume;
        bgmSlider.value = SoundManager.Instance.BGMVolume;
        gameSFXSlider.value = SoundManager.Instance.GameSFXVolume;
        sfxSlider.value = SoundManager.Instance.SFXVolume;
        ResolutionDropdown.value = 1;
        ResolutionDropdown.onValueChanged.AddListener(delegate {
            ResolutionDropDownAct(ResolutionDropdown);
        });
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

    void ResolutionDropDownAct(Dropdown select)
    {
        switch (select.value)
        {
            case 0:
                Camera.main.GetComponent<ResolutionFixed>().SetResolution(1920, 1080);
                break;

            case 1:
                Camera.main.GetComponent<ResolutionFixed>().SetResolution(1270, 720);
                break;

            case 2:
                Camera.main.GetComponent<ResolutionFixed>().SetResolution(1270, 960);
                break;

            default:
                Debug.Log("Wrong");
                break;
        }
    }

}
