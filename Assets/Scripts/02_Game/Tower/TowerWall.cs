using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerWall : MonoBehaviour
{
    public RectTransform rectTransform;
    Vector3 mousePos;
    bool move_flag;
    float max_distance;

    // Start is called before the first frame update
    void Start()
    {
        max_distance = 7.0f;
        transform.parent.GetComponent<Tower>().skillGague = 0.0f;
        rectTransform = GetComponent<RectTransform>();
        move_flag = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        moveObjcet();
        fixingObject();
        if (Input.GetMouseButtonDown(1) && move_flag)
        {
            Destroy(gameObject);
            transform.parent.GetComponent<Tower>().skillGague = transform.parent.GetComponent<Tower>().maxSkillGauge;
        }
    }


    void moveObjcet()
    {
        if (move_flag)
        {
            Vector3 pos = Input.mousePosition;
            pos.z += 10.0f;

            mousePos = Camera.main.ScreenToWorldPoint(pos);

            float curr_distance = Vector3.Distance(mousePos, transform.parent.position);

            if (curr_distance <= max_distance)
            {
                rectTransform.position = mousePos;
            }
        }
    }


    void fixingObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Transform hitTransform = null;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                hitTransform = hit.transform;

                if (hit.transform.CompareTag("Road"))
                {
                    move_flag = false;
                    gameObject.tag = "Wall";
                    transform.parent.GetComponentInChildren<TowerWallRange>().DestoryTowerRange();
                }
            }
        }
    }
}
