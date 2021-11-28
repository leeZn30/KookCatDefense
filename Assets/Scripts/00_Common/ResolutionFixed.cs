using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionFixed : MonoBehaviour
{

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetResolution(int width, int height, bool isFull)
    {
        int setWidth = width;
        int setHeight = height;

        GameData.Instance.width = width;
        GameData.Instance.height = height;
        GameData.Instance.isFull = isFull;


        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;

        if (isFull) Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true);
        else Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), false);


        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight)
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight);
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);
        }
        else
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight);
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight);
        }

        GameData.Instance.isResolutionChanged = true;

    }

}
