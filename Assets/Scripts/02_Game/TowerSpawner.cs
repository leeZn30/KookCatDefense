using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{   
    [SerializeField]
    private TowerDataViewer towerDataViewer;
    [SerializeField]
    private GameObject[] towerPrefab;
    [SerializeField]
    private GameObject[] followtowerPrefab;
    private SkillSpawner skillSpawner;
    private bool isOnTowerButton = false;
    public GameObject followTowerClone = null;
    private int towerType;

    private void Start()
    {
        skillSpawner = GameObject.Find("SkillSpawner").GetComponent<SkillSpawner>();

        for(int i=0; i<towerPrefab.Length; i++)
        {
            towerPrefab[i] = Resources.Load<GameObject>("Prefabs/Tower/Tower"+GameData.Instance.selectedTowers[i]);
            followtowerPrefab[i] = Resources.Load<GameObject>("Prefabs/UI/FollowTower"+GameData.Instance.selectedTowers[i]); 
        }
    }
    public void ReadytoSpawnTower(int type)
    {
        isOnTowerButton = false;

        Destroy(followTowerClone);
        Destroy(skillSpawner.followSkillClone);

        towerType = type;

        towerDataViewer.OnPanel1(towerPrefab[towerType]);

        //towerPrefab[towerType].;
        Tower tower = towerPrefab[towerType].GetComponent<Tower>();

        //Debug.Log(GameManager.Instance.coin);

        if (tower.Price > GameManager.Instance.coin)
        {
            Debug.Log("돈 부족");
            return;
        }

        if (isOnTowerButton == true)
        {
            return;
        }
        
        isOnTowerButton = true;

        followTowerClone = Instantiate(followtowerPrefab[towerType]);

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
        else Debug.Log("Ÿ�� �Ǽ� ����");

        isOnTowerButton = false;

        //Vector3 position = tileTransform.position + Vector3.back;
        //GameObject clone = Instantiate(towerPrefab[towerType], position, Quaternion.identity);

        Destroy(followTowerClone);

        StopCoroutine("OnTowerCancelSystem");

        towerDataViewer.OffPanel();
    }

    private IEnumerator OnTowerCancelSystem()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                isOnTowerButton = false;
                Destroy(followTowerClone);
                towerDataViewer.OffPanel();
                break;
            }

            yield return null;
        }
    }
}
