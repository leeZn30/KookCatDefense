using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRange: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Wall")
        {//충돌한 오브젝트가 벽일 때
            //Enemy공격명령
        }
        

    }
}
