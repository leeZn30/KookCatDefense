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
    public Button goButton;
    void Start()
    {
        goButton.onClick.AddListener(delegate { SceneManager.LoadScene("01_Main"); });

        canvas = transform.parent.gameObject;
        canvasChild = new List<GameObject>();
        for(int i=0; i<canvas.transform.childCount; i++)
        {
            canvasChild.Add(canvas.transform.GetChild(i).gameObject);
        }
        //CleanUI();
        GameManager.Instance.GameOverEvent += CleanUI;
    }
    void CleanUI()
    {
        StartCoroutine(Wait());
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
