using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower4SpecialSkill : MonoBehaviour
{
    public StageManager stage;

    public List<Enemy> enemies;

    //public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayGameSFX(GameSFX.catwheelSkill);
        transform.parent.GetComponent<Tower>().skillGague = 0.0f;
        StartCoroutine (Camera.main.GetComponent<CameraMove>().Shake(0.5f, 0.5f));
        stage = GameManager.Instance.Stage;
        enemies = stage.enemies;

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.SpeedDownAndReset(0.5f, 4.0f);
        }
        Destroy(gameObject, 0.5f);
    }

    private void OnDestroy()
    {
        transform.parent.GetComponent<Tower>().Startco();
    }
}
