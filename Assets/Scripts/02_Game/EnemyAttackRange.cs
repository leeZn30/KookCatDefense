using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class EnemyAttackRange: MonoBehaviour
{
    private Wall attackTarget;

    public event System.Action OnFindWall;
    public event System.Action OnMissWall;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {


    }
    public Wall GetAttckTarget()
    {
        return attackTarget;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Wall")

        {//충돌한 오브젝트가 벽일 때
            
            
            if (attackTarget == null ){
                //타겟이 없을때 ->타겟으로 설정
                
                
                attackTarget = coll.gameObject.GetComponent<Wall>();

                if (OnFindWall != null)
                {
                    OnFindWall();
                }
            }
            else
            {//이미 타겟 이 있을때

            }
            
            //Enemy공격명령
        }
        

    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            if (attackTarget != null)
            {
                attackTarget = null;
                if (OnMissWall != null)
                {
                    OnMissWall();
                }
            }
        }
    }
}
