using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerTile : MonoBehaviour
{
    private GameObject tower;

    bool isOver = false; 
    void Update()
    {
        if (isOver && Input.GetMouseButtonDown(0))
        { //좌클릭 이벤트 
            if (tower != null)
            {
                Debug.Log("타워 설치되어있음");
                //타워가 설치상태면 클릭 안되게
            }
            else
            {
                BulidTower(GameManager.Instance.currentTowerObj);
                Debug.Log("타워 설치");
            }
            
        } 
 
    } 
    public void BulidTower(GameObject p_towerObj)
    {
        Tower p_tower = p_towerObj.GetComponent<Tower>(); 
        if (p_tower != null)
        {
            if (GameManager.Instance.coin >=p_tower.price)
            {
                //GameManager.Instance.coin += p_tower.price;
                //추후추가

                tower = Instantiate(p_towerObj);
                tower.transform.parent = gameObject.transform;//타일의 자식으로 설정
                tower.transform.localPosition = new Vector3(0, 0.5f, 0);


            }
            else
            {
                Debug.Log("돈 없음");
            }
        }
        else
        {
            Debug.Log("설치 선택된 타워 없음");
        }
        
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
