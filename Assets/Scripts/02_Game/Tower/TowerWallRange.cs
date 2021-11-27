using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerWallRange : MonoBehaviour
{
    public GameObject towerWallRange;
    GameObject rangeObj;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void makeTowerRange()
    {
        rangeObj = Instantiate(towerWallRange, transform.position, Quaternion.identity, transform);
    }

    public void DestoryTowerRange()
    {
        Destroy(rangeObj);
    }
}
