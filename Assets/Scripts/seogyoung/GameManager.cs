using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{   //스테이지별 맵 로드
    //남은 적 
    //플레이어 게임오버 상태관리
    //ui 갱신
    public GameObject currentTowerObj;

    public StageManager[] maps;
    public int mapIdx = 0;
    private StageManager stage;
    public int coin;

    private bool isTowerSettingMode=false;

    [SerializeField]
    private int waveNum = -1;
    [SerializeField]
    private int enemyCnt;

    private bool isWaveFinish = false;//웨이브가 다끝났는지
    private bool isGameOver = false;


    //추후 타워 스포너로 옮길 코드
    int maxTowerCnt=4;
    [SerializeField]
    private GameObject[] towerPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        //타워스포너로 옮기기
        towerPrefabs = new GameObject[maxTowerCnt];
        for(int i=0; i<maxTowerCnt; i++) { 
            towerPrefabs[i] = Resources.Load<GameObject>("Prefabs/Tower/Tower"+GameData.Instance.selectedTowers[i]);
        }
        //

        InitMap();
    }
    void InitMap()
    {
        stage = Instantiate(maps[mapIdx]);
        stage.LoadMap();
        coin = stage.startCoin;
        

    }
    public void ClearGame()
    {
        Debug.Log("GameClear");
    }
    public void GameOver()
    {
        if (isGameOver == false)
        {
            isGameOver = true;
            Debug.Log("GameOver");    
        }
    }
    public void NextWave()
    {
        if (++waveNum>=stage.waves.Count)
        {
            //모든 웨이브가 끝남. 남은 적만 다 없애면 클리어
            isWaveFinish = true;
            return;
        }

        //다음 웨이브
        stage.StartWave(waveNum);
        enemyCnt += stage.currentWave.enemyCnt;

        Debug.Log("in GameManager.cs NextWave() - wave Num :" + waveNum+" enemyCnt :"+enemyCnt);
        //ui변경
    }
    public void UpdateEnemyDeath(Enemy enemy)
    {
        coin += enemy.coin;
        enemyCnt--;
        if (isWaveFinish == true && enemyCnt<=0)
        {
            ClearGame();
        }
    }
    // Update is called once per frame
    void Update()
    {
        //테스트용 코드
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextWave();
        }
    }
}
