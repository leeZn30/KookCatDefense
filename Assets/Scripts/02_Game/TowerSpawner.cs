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

    private void Start()
    {
        for(int i=0; i<towerPrefab.Length; i++)
        {
            towerPrefab[i] = Resources.Load<GameObject>("Prefabs/Tower/Tower"+GameData.Instance.selectedTowers[i]); 
        }
    }
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
        
        TowerTile towerTile = tileTransform.GetComponent<TowerTile>();
        //Tile tile = tileTransform.GetComponent<Tile>();
        /*
        if (tile.IsBuildTower == true)
        {
            return;
        }

        

        tile.IsBuildTower = true;
        */
        Tower tower = towerPrefab[towerType].GetComponent<Tower>();

        if (towerTile.BulidTower(towerPrefab[towerType]))
        {
            GameManager.Instance.coin -= (int)tower.Price;

        }
        else Debug.Log("타워 건설 실패");
        isOnTowerButton = false;

        //Vector3 position = tileTransform.position + Vector3.back;
        //GameObject clone = Instantiate(towerPrefab[towerType], position, Quaternion.identity);

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
