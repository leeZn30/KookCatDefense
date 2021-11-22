using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerTile : MonoBehaviour
{
    public GameObject tower;
    void Update()
    {
 
    } 
    public bool BulidTower(GameObject p_towerObj)
    {
        bool isSuccess=false;
        if (tower == null)
        {
            Tower p_tower = p_towerObj.GetComponent<Tower>();
            if (p_tower != null)
            {

                tower = Instantiate(p_towerObj);
                tower.transform.parent = gameObject.transform;//타일의 자식으로 설정
                tower.transform.localPosition = new Vector3(0, 0.5f, 0);
                isSuccess = true;

            }
        }
        return isSuccess;
        
    }

    /*
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

    }*/

}
