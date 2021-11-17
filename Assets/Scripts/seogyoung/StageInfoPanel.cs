using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StageInfoPanel : MonoBehaviour
{
    public TextMeshProUGUI text_stageName;
    public TextMeshProUGUI text_stageContent;
    public Button btn_select;


    private bool isInfoPanelOpen = false;

    [SerializeField]
    private StageInfo[] stageInfos;
    private Animator stageInfo_animator;

    // Start is called before the first frame update
    void Start()
    {
        stageInfo_animator = gameObject.GetComponent<Animator>();
    }

    public void SetStageInfos(StageInfo[] s)
    {
        stageInfos = s;
    }
    public StageInfo[] GetStageInfos()
    {
        return stageInfos;
    }
    public void PanelOpen()
    {
        if (isInfoPanelOpen == false)
        {
            isInfoPanelOpen = true;
            //올라오는 애니메이션
            stageInfo_animator.SetBool("IsOpen", isInfoPanelOpen);
        }
        else
        {//이미 열려있다면
            //내려갔다가 올라오는 애니메이션
            stageInfo_animator.SetTrigger("ReOpen");
        }
    }
    public void PanelClose()
    {
        if (isInfoPanelOpen == true)
        {
            isInfoPanelOpen = false;
            GameData.Instance.selectedStage = -1;
            //닫기 애니메이션
            stageInfo_animator.SetBool("IsOpen", isInfoPanelOpen);
        }
    }
    public void UpdateStageInfoText()
    {
        if (GameData.Instance.selectedStage == -1)
        {
            text_stageName.text = "";
            text_stageContent.text = "";
           
        }
        else
        {
            text_stageName.text = stageInfos[GameData.Instance.selectedStage].stageName;
            text_stageContent.text = stageInfos[GameData.Instance.selectedStage].content;
        }
        
    }


}
