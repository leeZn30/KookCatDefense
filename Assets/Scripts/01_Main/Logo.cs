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
    public Button quitButton;
    public GameObject creditBack;
    public GameObject credit;

    public GameObject cat1;
    private Animator catAnim;

    // Start is called before the first frame update
    void Start()
    {
        catAnim = cat1.GetComponent<Animator>();

        startButton.onClick.AddListener(delegate { StopAllCoroutines(); SoundManager.Instance.PlaySFX(SFX.ButtonClick); SceneManager.LoadScene("01_Main"); });
        creditButton.onClick.AddListener(OpenCredit);
        creditExitButton.onClick.AddListener(CloseCredit);
        SoundManager.Instance.PlayBGM();
        StartCoroutine(CatMoveX());
        quitButton.onClick.AddListener(delegate { Application.Quit(); });
    }

    IEnumerator CatMoveX()
    {
        int dir = -1;
        int cnt = 0;
        catAnim.SetFloat("MoveX", dir);
        while (true)
        {
            if (++cnt > 50)
            {
                cnt = 0;
                dir = -dir;
                catAnim.SetFloat("MoveX", dir);
            }
            cat1.transform.position += new Vector3(dir*0.05f, 0, 0);
            
            yield return new WaitForSeconds(0.05f);

        }
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
