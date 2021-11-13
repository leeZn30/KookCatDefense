using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class Enemy : MonoBehaviour
{
    public int id;
    public string enemyName;
    public int coin;
    public float affection;
    public float speed;
    public float attackDmg;

    public Transform affection_bar;
    public GameObject objAffection_bar;
    public GameObject enemyAttackRangeObj;

    public bool isDead=false;

    private bool isMoving = true;
    private float curAffection = 0;
    private Transform[] wayPoints;
    private int currentWayPointIdx=1;

    private Transform transformAttackRange;
    private EnemyAttackRange enemyAttackRange;
    private Rigidbody2D rigidbody2D;
    private Animator animator;

    public event System.Action OnDeath;


    void Start()
    {
    }
    public void SetUp(Transform[] wps)
    {
        transformAttackRange = enemyAttackRangeObj.GetComponent<Transform>();
        enemyAttackRange = enemyAttackRangeObj.GetComponent<EnemyAttackRange>();
        enemyAttackRange.OnFindWall += Attack;
        enemyAttackRange.OnMissWall += () => SetMoving(true);

        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        UpdateAffectionBar();

        wayPoints = new Transform[wps.Length];
        wayPoints = wps;
        
        transform.position = wayPoints[currentWayPointIdx++].position;
        StartCoroutine(Move());
    }
    public void AddAffection(float value) 
    {
        if(value+ curAffection >= affection)
        {
            curAffection = affection;        
        }
        else
        {
            curAffection = Mathf.Max(0, curAffection + value);
        }
        UpdateAffectionBar();

    }
    void UpdateAffectionBar()
    {
        float perValue = curAffection / affection;
        affection_bar.localScale = new Vector3(perValue, 1.0f, 1.0f);
        if (curAffection >= affection)
        {
            Die();
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (isDead == false)
        {
            if (coll.gameObject.tag == "Weapon")
            {//충돌한 오브젝트가 weapon일때
            }
        }
       
    }
    IEnumerator Move()
    {
        while (currentWayPointIdx < wayPoints.Length)
        {
            if (isMoving == true)
            {
                Vector3 dir = (wayPoints[currentWayPointIdx].position - transform.position).normalized;
                
                //공격범위 컬라이더 방향 전환
                float roValue = 180;
                if (dir.x <= 0) roValue = 90;
                roValue += (90 * dir.x);
                roValue += (90 * -dir.y);
                transformAttackRange.rotation = Quaternion.Euler(new Vector3(0, 0,roValue));

                animator.SetFloat("MoveX", dir.x);
                animator.SetFloat("MoveY", dir.y);


                transform.position += speed * dir * Time.deltaTime;
                if (Vector3.Distance(transform.position, wayPoints[currentWayPointIdx].position) < 0.02f * speed)
                {
                    if (currentWayPointIdx == wayPoints.Length - 1)
                    {
                        ////나중에 수정
                        Die();
                        break;
                    }
                    transform.position = wayPoints[currentWayPointIdx].position;
                    currentWayPointIdx++;
                }
                
            }
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.1f);
    }

    public void Attack()
    {

        SetMoving(false);
        
    }
    private void AttackEvent()
    {//attack 애니메이션 실행시 이벤트 메서드로 호출됨
        Wall wall = enemyAttackRange.GetAttckTarget();
        if (wall != null)
        {
            wall.AddHp(-attackDmg);   
        }
    }
    private void SetMoving(bool value)
    {
        if (isMoving == !value)
        {
            isMoving = value;
            animator.SetBool("isAttack", !isMoving);
        }
    }
    private void Die()
    {
        isMoving = false;
        isDead = true;
        objAffection_bar.SetActive(false);
        animator.SetTrigger("isDeath");
        StopAllCoroutines();

        if (OnDeath != null)
        {
            OnDeath();
        }


    }
    private void DieEvent()
    {//death 애니메이션 실행시 이벤트 메서드로 호출됨.
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {

    }



}
