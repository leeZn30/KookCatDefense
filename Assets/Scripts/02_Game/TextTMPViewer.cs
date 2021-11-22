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
    [SerializeField]
    private TextMeshProUGUI textStage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textPlayerGold.text = GameManager.Instance.coin.ToString();
        textEnemyCount.text = GameManager.Instance.EnemyCnt.ToString();
        textWaveCount.text = (GameManager.Instance.WaveNum + 1).ToString();
        textStage.text = "Stage " + (GameManager.Instance.mapIdx+1).ToString();
    }
}
