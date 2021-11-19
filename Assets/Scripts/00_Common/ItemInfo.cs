using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInfo 
{
    public int id;
    public string name;
    [TextArea]
    public string content;
    public int price;
}
