using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveEnemy
{
    public int amount;
    public GameObject type;

}

[System.Serializable]
public class Wave 
{
    public float spawnTime; //利 积己林扁
    public int enemyCnt=0;//醚 利 箭磊
    public WaveEnemy[] enemyPrefabs;//利 辆幅



}
