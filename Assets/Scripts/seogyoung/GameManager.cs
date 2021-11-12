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

    private bool isGameOver = false;
    
    // Start is called before the first frame update
    void Start()
    {
        InitMap();
    }
    void InitMap()
    {
        stage = Instantiate(maps[mapIdx]);
        stage.LoadMap();
        coin = stage.startCoin;
        stage.OnWaveFinish += NextWave;

    }
    void NextWave()
    {
        if (++waveNum>=stage.waves.Count)
        {
            //모든 웨이브가 끝남. 클리어
            //여기서  finish메서드 호출
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
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextWave();
        }
    }
}
