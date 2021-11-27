using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{   //���������� �� �ε�
    //���� �� 
    //�÷��̾� ���ӿ��� ���°���
    //ui ����
    public float currentTime;

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
    private int currentStar;

    System.Diagnostics.Stopwatch watch;
    void Start()
    {
        mapIdx = GameData.Instance.selectedStage;
        InitMap();
        textWaveCounter.enabled = false;
        StartCoroutine(NextWave());

        currentStar = 3;
        watch = new System.Diagnostics.Stopwatch();
        watch.Start();

    }

    void Update()
    {
        currentTime = (int)watch.ElapsedMilliseconds/1000;
        if (currentStar > 1)
        {
            if (currentTime > stage.clearTime[3-currentStar])
            {
                currentStar--;
            }
        }

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
        if (i == 0)
        {
            SoundManager.Instance.PlaySFX(SFX.WaveStart);
            if (isWaveFinish == true)
            {
                textWaveCounter.text = "Boss Wave";
            }
            else
                textWaveCounter.text = "Wave " + (waveNum + 1);
        }
        else
        {
            SoundManager.Instance.PlaySFX(SFX.Pop);
            textWaveCounter.text = "" + i;
        }

    }
    public void ClearGame()
    {

        watch.Stop();
        Debug.Log(currentTime+"s");
        Debug.Log("GameClear");

        SoundManager.Instance.PlaySFX(SFX.GameClear);
        GameData.Instance.ClearSelectedThings();
        

        GameData.Instance.stageLocks[mapIdx] = Mathf.Max(GameData.Instance.stageLocks[mapIdx], currentStar);//1,2,3 이렇게 별 구분 

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
            SoundManager.Instance.PlaySFX(SFX.nang);
            GameData.Instance.ClearSelectedThings();
            isGameOver = true;
            watch.Stop();
            Debug.Log(currentTime + "s");
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
        SoundManager.Instance.PlayGameSFX(GameSFX.CatHappy);
        if (isWaveFinish == true && enemyCnt<=0)
        {
            ClearGame();
        }
        else if (enemyCnt <= 0)
        {
            StartCoroutine(NextWave());;
        }
    }


    public StageManager Stage
    {
        get => stage;
    }
    public int GetStar()
    {
        return currentStar;
    }
}
