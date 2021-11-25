using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
    public List<Enemy> collidedEnemy = new List<Enemy>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return;
        }

        Enemy enemy = collision.GetComponent<Enemy>();

        collidedEnemy.Add(enemy);

        enemy.Speed *= 0.6f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return;
        }

        collision.GetComponent<Enemy>().ResetMoveSpeed();       
    }

    void OnDestroy(){
        if (collidedEnemy != null){
            //Enemy[] slowEnemyArr = ToArray();
            for (int i=0; i<collidedEnemy.Count; i++){
                collidedEnemy[i].ResetMoveSpeed();
            }
        }
    }
}
