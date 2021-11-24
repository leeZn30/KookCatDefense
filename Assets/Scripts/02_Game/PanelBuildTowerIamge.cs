using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelBuildTowerIamge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<4; i++)
        {
            transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/icon/icon_tower" + GameData.Instance.selectedTowers[i]);
        }
        for (int i=4; i<6; i++)
        {
            transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/icon/icon_skill"+GameData.Instance.selectedSkills[i-4]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
