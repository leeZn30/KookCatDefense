using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageManager : MonoBehaviour
{//enemyspowner나 다름없음
    private Transform[] wayPoints;

    public Tilemap tileMap;
    public GameObject pointTiles;
    public int startCoin;

    public GameObject[] enemy;

    public  List<Enemy> enemies=new List<Enemy>();//생성한 적들
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void LoadMap()
    {
        wayPoints = pointTiles.GetComponentsInChildren<Transform>();
    }
    public Transform[] GetWayPoints()
    {
        return wayPoints;
    }
    public void CreateEnemy()
    {
        //추후 수정
        GameObject cat = Instantiate(enemy[0]);
        Enemy _enemy = cat.GetComponent<Enemy>();
        _enemy.SetUp(wayPoints);

        //리스트에 추가
        enemies.Add(_enemy);

        //리스트에서 삭제
        _enemy.OnDeath += () => enemies.Remove(_enemy);
        //남은 적 수,재화 변경 (gm메소드)
        //_enemy.OnDeath += () => 

        //통과한 적숫자 누적 후 게임오버 관련 메서드
        //_enemy.OnSurvive += () =>


    }

}
