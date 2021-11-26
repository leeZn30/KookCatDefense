using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StageInfo : MonoBehaviour
{
    public int num;
    public string stageName;
    public string content;
    public string Image;

    public SpriteRenderer backRenderer;
    public SpriteRenderer starRenderer;

    public void SetImage(int i)
    {
        switch (i)
        {
            case -1:
                backRenderer.sprite=Resources.Load<Sprite>("Image/UI/lock");
                SetAlpha(0);
                break;
            case 0:
                backRenderer.sprite = Resources.Load<Sprite>("Image/UI/smallstar");
                SetAlpha(0);
                break;
            default:
                backRenderer.sprite = Resources.Load<Sprite>("Image/UI/smallstar");
                starRenderer.sprite = Resources.Load<Sprite>("Image/UI/smallstar" + i);
                SetAlpha(255);
                break;
        }
    }
    private void SetAlpha(int i)
    {
        Color color= starRenderer.color;
        color.a = i;
        starRenderer.color = color;
    }
}