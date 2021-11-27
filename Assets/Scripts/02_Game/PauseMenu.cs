using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public List<GameObject> canvasChild;
    GameObject canvas;
    public GameObject pauseUI;
    public GameObject towerInfoUI;

    private bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        canvas = pauseUI.transform.parent.gameObject;
        canvasChild = new List<GameObject>();
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            canvasChild.Add(canvas.transform.GetChild(i).gameObject);
        }
        pauseUI.SetActive(false);
        towerInfoUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause")){
            Pause();
        }

    }
    public void Pause(){
        SoundManager.Instance.PlaySFX(SFX.ButtonClick);
        paused = !paused;

        if(paused){
            for (int i = 0; i < canvas.transform.childCount; i++)
            {
                if (canvasChild[i] != pauseUI)
                {
                    canvasChild[i].SetActive(false);
                }
                else
                {
                    canvasChild[i].SetActive(true);
                }

            }

            Time.timeScale = 0;
        }
        if(!paused){
            for (int i = 0; i < canvas.transform.childCount; i++)
            {
                if (canvasChild[i] == pauseUI || canvasChild[i] == towerInfoUI)
                {
                    canvasChild[i].SetActive(false);
                }
                else 
                {
                    canvasChild[i].SetActive(true);
                }


            }
            Time.timeScale = 1f;
        }
    }
    
    public void Resume(){
       
        Pause();
    }

    public void Quit(){
        SoundManager.Instance.PlaySFX(SFX.CatSoundClick);
        Application.Quit();
    }
    public void GoMain()
    {
        
        Pause();
        GameData.Instance.ClearSelectedThings();
        SceneManager.LoadScene("01_Main");
    }
    public void OpenSetting()
    {

    }
}
