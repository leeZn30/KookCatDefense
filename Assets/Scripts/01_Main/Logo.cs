using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Logo : MonoBehaviour
{
    public Button startButton;
    public Button creditButton;
    public Button creditExitButton;
    public GameObject creditBack;
    public GameObject credit;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(delegate { SoundManager.Instance.PlaySFX(SFX.ButtonClick); SceneManager.LoadScene("01_Main"); });
        creditButton.onClick.AddListener(OpenCredit);
        creditExitButton.onClick.AddListener(CloseCredit);
        SoundManager.Instance.PlayBGM();
    }


    void OpenCredit()
    {
        SoundManager.Instance.PlaySFX(SFX.ButtonClick);
        credit.SetActive(true);
        creditBack.SetActive(true);
    }
    void CloseCredit()
    {
        SoundManager.Instance.PlaySFX(SFX.ButtonClick);
        credit.SetActive(false);
        creditBack.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
