using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerTile : MonoBehaviour
{
 
    bool isOver = false; 
    void Update()
    {
        if (isOver && Input.GetMouseButtonDown(0))
        { //좌클릭 이벤트 
            Debug.Log("click");
        } 
 
    } 
    public void BulidTower()
    {

    }
    void OnMouseOver() {
        if (isOver == false)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                isOver = true;
                Debug.Log("over");
            }


        }
            
    }
    void OnMouseExit()
    {
        if (isOver == true)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                isOver = false;
            }


        }

    }

}
