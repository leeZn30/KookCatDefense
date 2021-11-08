using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseUI;
    public GameObject NormalUI;

    private bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        PauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause")){
            paused = !paused;
        }
        if(paused){
            PauseUI.SetActive(true);
            NormalUI.SetActive(false);
            Time.timeScale = 0;
        }
        if(!paused){
            PauseUI.SetActive(false);
            NormalUI.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    public void Pause(){
        paused = !paused;

        if(paused){
            PauseUI.SetActive(true);
            NormalUI.SetActive(false);
            Time.timeScale = 0;
        }
        if(!paused){
            PauseUI.SetActive(false);
            NormalUI.SetActive(true);
            Time.timeScale = 1f;
        }
    }
    
    public void Resume(){
        paused = !paused;
    }

    public void Quit(){
        Application.Quit();
    }
}
