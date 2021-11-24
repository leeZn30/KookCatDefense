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

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        goButton1.onClick.AddListener(delegate { SceneManager.LoadScene("01_Main"); });
        goButton2.onClick.AddListener(delegate { SceneManager.LoadScene("01_Main"); });

        canvas = transform.parent.gameObject;
        canvasChild = new List<GameObject>();
        for(int i=0; i<canvas.transform.childCount; i++)
        {
            canvasChild.Add(canvas.transform.GetChild(i).gameObject);
        }
        //CleanUI();
        GameManager.Instance.GameOverEvent += StartGameOverAct;
        GameManager.Instance.GameClearEvent += StartGameClearAct;
    }
    public void OpenClearPanel()
    {
        gameClearPanel.SetActive(true);
    }
    void StartGameClearAct()
    {
        animator.SetTrigger("Clear");
        CleanUI();
        Debug.Log(Time.timeScale);
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
        yield return new WaitForSecondsRealtime(5.0f);
        Time.timeScale = 1;
        gameOverPanel.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
