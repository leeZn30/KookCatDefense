using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerInfo 
{
    public int id;
    public string towerName;
    public int price;
    public string content;
    public string skillContent;//일단 이렇게했는데 skill id로 저장하는게 나쁘지않아보이기도
}
