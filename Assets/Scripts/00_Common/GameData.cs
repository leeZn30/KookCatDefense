using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData>
{
    public int[] stageLocks;
    public int[] stageStars;



    //game scene으로 넘어갈때 참조할 변수들
    public int selectedStage=-1;
    public int[] selectedTowers;  //사용자가 선택한 타워 id들
    public int[] selectedSkills;


    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        
    }
}
