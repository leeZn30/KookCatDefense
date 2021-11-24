using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelBuildTowerIamge : MonoBehaviour
{
    [SerializeField]
    private GameObject[] towerPrefab;
    [SerializeField]
    private GameObject[] skillPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<4; i++)
        {
            transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/icon/icon_tower" + GameData.Instance.selectedTowers[i]);

            towerPrefab[i] = Resources.Load<GameObject>("Prefabs/Tower/Tower"+GameData.Instance.selectedTowers[i]);
            Tower tower = towerPrefab[i].GetComponent<Tower>();

            transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = "$" + tower.Price;
        }
        
        for (int i=4; i<8; i++)
        {
            transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/icon/icon_skill"+GameData.Instance.selectedSkills[i-4]);

            skillPrefab[i-4] = Resources.Load<GameObject>("Prefabs/Skill/Skill" + GameData.Instance.selectedSkills[i-4]);
            Skill skill = skillPrefab[i-4].GetComponent<Skill>();

            transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = "$" + skill.Price;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
