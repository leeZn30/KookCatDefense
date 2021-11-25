using System;
using System.Collections.Generic;
using UnityEngine;

public class DataParser : MonoBehaviour
{
    // generic ÀÀ¿ë
    T StringToEnum<T>(string e)
    {
        return (T)Enum.Parse(typeof(T), e);
    }

  
    public void StageInfoParse(StageInfo[] stageInfos ,string _CSVFileName)
    {
        List<StageInfo> stageInfoList = new List<StageInfo>();
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);
        string[] data = csvData.text.Split(new char[] { '\n' });

        for(int i=1; i<=stageInfos.Length; i++)
        {
            if (data.Length <= i) break;

            string[] row = data[i].Split(new char[] { ',' });

            if (row[0] == "") break;

            stageInfos[i-1].num= int.Parse(row[0]);
            stageInfos[i-1].stageName = row[1];
            stageInfos[i-1].content = row[2].Replace("'",",").Replace(";","\n");
            stageInfos[i-1].Image = row[3];


        }
     
    }

    private void Start()
    {


    }

}