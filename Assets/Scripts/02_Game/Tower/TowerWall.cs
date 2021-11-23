using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerWall : MonoBehaviour
{
    public RectTransform rectTransform;
    Vector3 mousePos;
    bool move_flag;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        move_flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        moveObjcet();
        if (Input.GetMouseButtonDown(0))
        {
            fixingObject();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
    }


    void moveObjcet()
    {
        if (move_flag)
        {
            Vector3 pos = Input.mousePosition;
            pos.z += 10.0f;

            mousePos = Camera.main.ScreenToWorldPoint(pos);
            rectTransform.position = mousePos;
        }
    }

    void fixingObject()
    {
        move_flag = false;
    }
}
