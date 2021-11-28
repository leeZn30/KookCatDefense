using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToVinyl : MonoBehaviour
{
    IEnumerator coroutine;
    public List<Enemy> collidedEnemy = new List<Enemy>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collidedEnemy != null){
            //Enemy[] slowEnemyArr = ToArray();
            for (int i=0; i<collidedEnemy.Count; i++){
                collidedEnemy[i].MoveToTarget(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return;
        }

        Enemy enemy = collision.GetComponent<Enemy>();

        collidedEnemy.Add(enemy);

        enemy.StopAllCoroutines();
        //coroutine = enemy.Move();
        //StopCoroutine(coroutine);
        //enemy.StartCoroutine(enemy.MoveToTarget(gameObject));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return;
        }

        //collision.GetComponent<Enemy>().ResetMoveSpeed();  
    }

    void OnDestroy(){
        if (collidedEnemy != null){
            //Enemy[] slowEnemyArr = ToArray();
            for (int i=0; i<collidedEnemy.Count; i++){
                collidedEnemy[i].ResetMoveSpeed();
                collidedEnemy[i].StartCoroutine(collidedEnemy[i].Move());
            }
        }
    }
}
