using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTMPViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerGold;
    [SerializeField]
    private TextMeshProUGUI textEnemyCount;
    [SerializeField]
    private TextMeshProUGUI textWaveCount;
    //[SerializeField]
    //private TextMeshProUGUI textStage;
    [SerializeField]
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textPlayerGold.text = gameManager.coin.ToString();
        textEnemyCount.text = gameManager.EnemyCnt.ToString();
        textWaveCount.text = gameManager.WaveNum.ToString();
        //textStage.text = gameManager.WaveNum.ToString();
    }
}
