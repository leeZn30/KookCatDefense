using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public GameObject backGround;
    public StageInfoPanel stageInfoPanel;
    public Button backButton;

    private Ray ray;
    private RaycastHit hit;
    private Camera mainCamera;
    private void Awake()
    {
        stageInfoPanel.SetStageInfos( backGround.GetComponentsInChildren<StageInfo>());
        
    }
    void Start()
    {
        
        mainCamera = Camera.main;
        GetStageInfo();
        SoundManager.Instance.PlayBGM();
        backButton.onClick.AddListener(delegate { SoundManager.Instance.PlaySFX(SFX.CatSoundClick); SceneManager.LoadScene("00_Logo"); });

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.transform.CompareTag("StageButton"))
                    {
                        Debug.Log("stage click");
                        OnClickStageButton(hit.transform.GetComponent<StageInfo>());
                    }
                    else
                    {
                        stageInfoPanel.PanelClose();

                    }
                }
            }
   
        }
    }
    public void GetStageInfo()
    {
        DataParser parser = GetComponent<DataParser>();
        parser.StageInfoParse(stageInfoPanel.GetStageInfos(),"stageInfo");
    }
    void OnClickStageButton(StageInfo stageInfo)
    {
        if (GameData.Instance.stageLocks[stageInfo.num] == -1) return;//잠금 안풀린 스테이지 누른경우
        if (GameData.Instance.selectedStage == stageInfo.num) return;//같은 스테이지 누른 경우 return;
        GameData.Instance.selectedStage = stageInfo.num;


        stageInfoPanel.PanelOpen();
        SoundManager.Instance.PlaySFX(SFX.ButtonClick);
    }

}
