using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerGauge : MonoBehaviour
{
    //public Image SkillGaugeBar;

    Tower parentTower;

    // Start is called before the first frame update
    void Start()
    {
        parentTower = transform.parent.GetComponent<Tower>();
    }

    // Update is called once per frame
    void Update()
    {
        showGauge();
    }

    void showGauge()
    {
        //SkillGaugeBar.fillAmount = parentTower.skillGague / 100;
        float perValue = parentTower.skillGague / parentTower.maxSkillGauge;

        gameObject.transform.localScale = new Vector3(perValue, 1.0f, 1.0f);
    }
}
