using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] towerPrefab;
    [SerializeField]
    private GameObject followtowerPrefab;
    private bool isOnTowerButton = false;
    private GameObject followTowerClone = null;
    private int towerType;

    public void ReadytoSpawnTower(int type)
    {
        towerType = type;

        //towerPrefab[towerType].;
        Tower tower = towerPrefab[towerType].GetComponent<Tower>();

        Debug.Log(GameManager.Instance.coin);

        if (tower.Price > GameManager.Instance.coin)
        {
            return;
        }

        if (isOnTowerButton == true)
        {
            return;
        }
        
        isOnTowerButton = true;

        followTowerClone = Instantiate(followtowerPrefab);

        StartCoroutine("OnTowerCancelSystem");
    }

    public void SpawnTower(Transform tileTransform)
    {
        if (isOnTowerButton == false)
        {
            return;
        }

        Tile tile = tileTransform.GetComponent<Tile>();

        if(tile.IsBuildTower == true)
        {
            return;
        }

        isOnTowerButton = false;

        tile.IsBuildTower = true;

        Tower tower = towerPrefab[towerType].GetComponent<Tower>();

        GameManager.Instance.coin -= (int)tower.price;

        Vector3 position = tileTransform.position + Vector3.back;
        GameObject clone = Instantiate(towerPrefab[towerType], position, Quaternion.identity);

        Destroy(followTowerClone);

        StopCoroutine("OnTowerCancelSystem");
    }

    private IEnumerator OnTowerCancelSystem()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                isOnTowerButton = false;
                Destroy(followTowerClone);
                break;
            }

            yield return null;
        }
    }
}
