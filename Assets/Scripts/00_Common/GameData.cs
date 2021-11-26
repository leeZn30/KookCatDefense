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

    public int width;
    public int height;
    public bool isResolutionChanged = false;


    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void ClearSelectedThings()
    {
        selectedStage = -1;
        for(int i=0; i<4; i++)
        {
            selectedTowers[i] = -1;
            selectedSkills[i] = -1;
        }
    }

    void Update()
    {
        
    }
}
