using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{   //���������� �� �ε�
    //���� �� 
    //�÷��̾� ���ӿ��� ���°���
    //ui ����
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

    private bool isWaveFinish = false;//���̺갡 �ٳ�������
    private bool isGameOver = false;

    public event System.Action GameOverEvent;

    public float WaveNum => waveNum;
    public float EnemyCnt => enemyCnt;

    // Start is called before the first frame update
    void Start()
    {
        mapIdx = GameData.Instance.selectedStage;
        InitMap();
        NextWave();

    }
    void InitMap()
    {
        SoundManager.Instance.PlayBGM((BGM)mapIdx + 1);
        stage = Instantiate(maps[mapIdx]);
        stage.LoadMap();
        coin = stage.startCoin;
        

    }
    public void ClearGame()
    {
        Debug.Log("GameClear");
        //1,2,3 이렇게 별 구분 나중에 가능 
        GameData.Instance.stageLocks[mapIdx] = 1;

        if(GameData.Instance.stageLocks.Length>mapIdx+1)//다음맵 개방
            GameData.Instance.stageLocks[mapIdx+1] = 0;
    }
    public void GameOver()
    {
        if (isGameOver == false)
        {
            GameData.Instance.ClearSelectedThings();
            isGameOver = true;
            Debug.Log("GameOver");
            if (GameOverEvent != null)
            {
                GameOverEvent();
            }

        }
    }
    public void NextWave()
    {
        if (++waveNum + 1>=stage.waves.Count)
        {
            //��� ���̺갡 ����. ���� ���� �� ���ָ� Ŭ����
            isWaveFinish = true;
        }

        //���� ���̺�
        stage.StartWave(waveNum);
        enemyCnt += stage.currentWave.enemyCnt;

        Debug.Log("in GameManager.cs NextWave() - wave Num :" + waveNum+" enemyCnt :"+enemyCnt);
        //ui����
    }
    public void UpdateEnemyDeath(Enemy enemy)
    {
        coin += enemy.coin;
        enemyCnt--;

        if (isWaveFinish == true && enemyCnt<=0)
        {
            ClearGame();
        }
        else if (enemyCnt <= 0)
        {
            NextWave();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public StageManager Stage
    {
        get => stage;
    }
}
