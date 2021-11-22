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
            transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Tower"+GameData.Instance.selectedTowers[i]);
        }
        for (int i=4; i<6; i++)
        {
            transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Skill"+GameData.Instance.selectedSkills[i-4]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
