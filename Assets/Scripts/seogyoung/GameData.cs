using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData>
{
    public int[] stageLocks;
    public int[] stageStars;

    public int[] selectedTowers;  //사용자가 선택한 타워 id들
    public int[] selectedSkills;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
