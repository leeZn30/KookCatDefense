using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{   //���������� �� �ε�
    //���� �� 
    //�÷��̾� ���ӿ��� ���°���
    //ui ����
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
            //��� ���̺갡 ����. Ŭ����
            //���⼭  finish�޼��� ȣ��
            return;
        }

        //���� ���̺�
        stage.StartWave(waveNum);
        enemyCnt += stage.currentWave.enemyCnt;

        Debug.Log("in GameManager.cs NextWave() - wave Num :" + waveNum+" enemyCnt :"+enemyCnt);
        //ui����
    }
    public void UpdateEnemyDeath(Enemy enemy)
    {
        Debug.Log(enemyCnt+" ���� ī��Ʈ");
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