using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower4SpecialSkill : MonoBehaviour
{
    //public GameManager gm;

    public List<Enemy> enemies;

    // Start is called before the first frame update
    void Start()
    {
        //enemies = sm.enemies;

        foreach(Enemy enemy in enemies)
        {
            Debug.Log(enemy);
            enemy.SpeedDownAndReset(0.5f, 4.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        Destroy(gameObject);
    }
}
