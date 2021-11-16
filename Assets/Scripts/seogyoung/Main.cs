using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Main : MonoBehaviour
{
    public Select select;
    public GameObject backGround;
    public GameObject stageInfoPanel;
    public TextMeshProUGUI text_stageName;
    public TextMeshProUGUI text_stageContent;
    public Button btn_select;

    private Button[] stage_btns;

    // Start is called before the first frame update
    void Start()
    {
        stage_btns = backGround.GetComponentsInChildren<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
