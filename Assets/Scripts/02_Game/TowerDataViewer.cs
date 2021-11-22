using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerDataViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textName;
    [SerializeField]
    private TextMeshProUGUI textContent;
    //[SerializeField]
    //private TextMeshProUGUI textDamage;
    [SerializeField]
    private TextMeshProUGUI textPrice;

    private Tower currentTower;
    private Skill currentSkill;
    // Start is called before the first frame update
    void Start()
    {
        OffPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OffPanel();
        }
    }

    public void OnPanel(Transform tower)
    {
        currentTower = tower.GetComponent<Tower>();
        Debug.Log(currentTower.Price);
        gameObject.SetActive(true);
        UpdateTowerData();
    }

    public void OnPanel1(GameObject tower)
    {
        currentTower = tower.GetComponent<Tower>();
        Debug.Log(currentTower.Price);
        gameObject.SetActive(true);
        UpdateTowerData();
    }

    public void OnPanelSkill(GameObject skill)
    {
        currentSkill = skill.GetComponent<Skill>();
        Debug.Log(currentSkill.Price);
        gameObject.SetActive(true);
        UpdateSkillData();
    }

    public void OffPanel()
    {
        gameObject.SetActive(false);
    }

    private void UpdateTowerData()
    {
        textName.text = currentTower.Name;
        textContent.text = currentTower.Content;
        //textDamage.text = "Damage : " + currentTower.Price;
        textPrice.text = "Price : " + currentTower.Price;
    }

    private void UpdateSkillData()
    {
        textName.text = currentSkill.Name;
        textContent.text = currentSkill.Content;
        textPrice.text = "Price : " + currentSkill.Price;
    }

    public void OnClickEventTowerSell()
    {
        currentTower.Sell();
        OffPanel();
    }
}
