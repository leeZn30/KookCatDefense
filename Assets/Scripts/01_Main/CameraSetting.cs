using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameData.Instance.isResolutionChanged)
        {
            Camera.main.GetComponent<ResolutionFixed>().SetResolution(GameData.Instance.width, GameData.Instance.height, GameData.Instance.isFull);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
