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
        //StartCoroutine(camera.GetComponent<CameraMove>().Shake(3f, 3f));
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
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        transform.parent.GetComponent<Tower>().Startco();
    }
}
