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
            towerPrefab[i] = Resources.Load<GameObject>("Prefabs/Tower/Tower"+GameData.Instance.selectedTowers[i]);
            Tower tower = towerPrefab[i].GetComponent<Tower>();

            transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Tower"+GameData.Instance.selectedTowers[i]);
            transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = "$" + tower.Price;
        }
        for (int i=4; i<8; i++)
        {
            skillPrefab[i-4] = Resources.Load<GameObject>("Prefabs/Skill/Skill" + GameData.Instance.selectedSkills[i-4]);
            Skill skill = skillPrefab[i-4].GetComponent<Skill>();

            transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Skill"+GameData.Instance.selectedSkills[i-4]);
            transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = "$" + skill.Price;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
