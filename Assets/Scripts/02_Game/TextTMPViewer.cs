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
    [SerializeField]
    private TextMeshProUGUI textStageName;
    [SerializeField]
    private TextMeshProUGUI textDoorHP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textPlayerGold.text = GameManager.Instance.coin.ToString();
        textEnemyCount.text = GameManager.Instance.EnemyCnt.ToString();
        textWaveCount.text = "Wave " + (GameManager.Instance.WaveNum + 1).ToString()+"/"+GameManager.Instance.Stage.waves.Count;
        textStage.text = "Stage " + (GameManager.Instance.mapIdx+1).ToString();
        textStageName.text = GameManager.Instance.Stage.mapName;
        textDoorHP.text = GameManager.Instance.Stage.lastWall.hp.ToString();

    }
}
