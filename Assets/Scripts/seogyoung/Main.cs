using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Main : MonoBehaviour
{
    public Select select;
    public GameObject backGround;
    public StageInfoPanel stageInfoPanel;


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
                        Debug.Log("click");
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

        if (GameData.Instance.selectedStage == stageInfo.num) return;//���� �������� ���� ��� return;
        GameData.Instance.selectedStage = stageInfo.num;


        stageInfoPanel.PanelOpen();
    }

}