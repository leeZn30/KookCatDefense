using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public event System.Action GameClearEvent;

    public float WaveNum => waveNum;
    public float EnemyCnt => enemyCnt;

    [SerializeField]
    private TextMeshProUGUI textWaveCounter;

    // Start is called before the first frame update
    void Start()
    {
        mapIdx = GameData.Instance.selectedStage;
        InitMap();
        textWaveCounter.enabled = false;
        StartCoroutine(NextWave());

    }
    void InitMap()
    {
        SoundManager.Instance.PlayBGM((BGM)mapIdx + 1);
        stage = Instantiate(maps[mapIdx]);
        stage.LoadMap();
        stage.OnCount += ShowCount;
        coin = stage.startCoin;
        

    }
    void ShowCount(int i)
    {
        textWaveCounter.enabled = true;
        if(i==-1)
            textWaveCounter.enabled = false;
        if (i==0)
            textWaveCounter.text = "Wave " + (waveNum + 1);
        else
            textWaveCounter.text = "" + i;

    }
    public void ClearGame()
    {
        Debug.Log("GameClear");

        GameData.Instance.ClearSelectedThings();
        
        GameData.Instance.stageLocks[mapIdx] = 1;//1,2,3 이렇게 별 구분 나중에 가능 

        if (GameData.Instance.stageLocks.Length>mapIdx+1)//다음맵 개방
            GameData.Instance.stageLocks[mapIdx+1] = 0;
        if (GameClearEvent != null)
        {
            GameClearEvent();
        }
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
    public IEnumerator NextWave()
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
        yield return new WaitForSeconds(0.1f);
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
            StartCoroutine(NextWave());;
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
