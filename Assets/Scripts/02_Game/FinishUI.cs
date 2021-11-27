using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishUI : MonoBehaviour
{
    // Start is called before the first frame update
     GameObject canvas;
    public List<GameObject> canvasChild;
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;
    public Button goButton1;
    public Button goButton2;

    public GameObject Star;
    private GameObject[] stars = new GameObject[3];
    Animator animator;
    void Start()
    {

        animator = GetComponent<Animator>();
        goButton1.onClick.AddListener(delegate { SoundManager.Instance.PlaySFX(SFX.CatSoundClick); SceneManager.LoadScene("01_Main"); });
        goButton2.onClick.AddListener(delegate { SoundManager.Instance.PlaySFX(SFX.CatSoundClick); SceneManager.LoadScene("01_Main"); });

        canvas = transform.parent.gameObject;
        canvasChild = new List<GameObject>();
        for(int i=0; i<canvas.transform.childCount; i++)
        {
            canvasChild.Add(canvas.transform.GetChild(i).gameObject);
        }

        for(int i=0; i<3; i++)
        {
            stars[i] = Star.transform.GetChild(i).gameObject;
            stars[i].SetActive(false);
        }
        //CleanUI();
        GameManager.Instance.GameOverEvent += StartGameOverAct;
        GameManager.Instance.GameClearEvent += StartGameClearAct;
    }
    public void OpenClearPanel()
    {
        animator.SetInteger("Star", GameManager.Instance.GetStar());
        gameClearPanel.SetActive(true);
    }
    public void SetActiveStar(int num)
    {
        SoundManager.Instance.PlaySFX(SFX.Shine);
    }
    void StartGameClearAct()
    {
        animator.SetTrigger("Clear");
        CleanUI();
    }
    void StartGameOverAct()
    {
        StartCoroutine(Wait());
        CleanUI();
    }
    void CleanUI()
    {
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            if (canvasChild[i] != gameObject)
            {
                canvasChild[i].SetActive(false);
            }
                
        }
    }
    IEnumerator Wait()
    {
        Time.timeScale = 3;
        yield return new WaitForSecondsRealtime(7.0f);
        Time.timeScale = 1;
        gameOverPanel.SetActive(true);
        SoundManager.Instance.PlaySFX(SFX.GameOver);
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
