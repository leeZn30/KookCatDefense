using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerDataViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPrice;

    private Tower currentTower;
    // Start is called before the first frame update
    void Start()
    {
        OffPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
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

    public void OffPanel()
    {
        gameObject.SetActive(false);
    }

    private void UpdateTowerData()
    {
        textPrice.text = "Price : " + currentTower.Price;
    }
}
