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

    public float attackDmg;
    public float attackSpeed;

    public float baseSpeed;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float currentAtkSpeed;
    [SerializeField]
    private float curAffection = 0;
    public Transform affection_bar;
    public GameObject objAffection_bar;
    public GameObject enemyAttackRangeObj;

    public bool isDead=false;
    
    private bool isMoving = true;
    
    private Transform[] wayPoints;
    private int currentWayPointIdx=1;

    private Transform transformAttackRange;
    private EnemyAttackRange enemyAttackRange;
    private Rigidbody2D rigidbody2D;
    private Animator animator;

    public event System.Action OnDeath;

    public float AttackSpeed
    {
        set => currentAtkSpeed = Mathf.Max(0, value);
        get => currentAtkSpeed;
    }
    public float Speed
    {
        set => speed = Mathf.Max(0, value);
        get => speed;
    }

    void Start()
    {
        
    }
    public void SetUp(Transform[] wps)
    {
        speed = baseSpeed;
        currentAtkSpeed = attackSpeed;

        transformAttackRange = enemyAttackRangeObj.GetComponent<Transform>();
        enemyAttackRange = enemyAttackRangeObj.GetComponent<EnemyAttackRange>();
        enemyAttackRange.OnFindWall += Attack;
        enemyAttackRange.OnMissWall += () => SetMoving(true);

        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); animator.SetFloat("attackSpeed", currentAtkSpeed);
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
            if (isDead == false)
            {
                isDead = true;
                Die();
            }    
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (isDead == false)
        {
            if (coll.gameObject.tag == "Weapon")
            {//�浹�� ������Ʈ�� weapon�϶�
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
                
                //���ݹ��� �ö��̴� ���� ��ȯ
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
                    if (currentWayPointIdx == wayPoints.Length - 1)//마지막 위치일때
                    {
                        ////���߿� ����
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
    {//attack �ִϸ��̼� ����� �̺�Ʈ �޼���� ȣ���
        Wall wall = enemyAttackRange.GetAttckTarget();
        if (wall != null)
        {
            wall.AddHp(-attackDmg);   
        }
    }
    private void SetMoving(bool value)
    {//공격모션 같이나오게
        if (isMoving == !value)
        {
            isMoving = value;
            animator.SetBool("isAttack", !isMoving);
            
        }
    }
    private void Die()
    {
        
            isMoving = false;
            objAffection_bar.SetActive(false);
            animator.SetTrigger("isDeath");
            StopAllCoroutines();

            if (OnDeath != null)
            {
                OnDeath();
            }
        
        


    }
    private void DieEvent()
    {//death �ִϸ��̼� ����� �̺�Ʈ �޼���� ȣ���.
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void ResetAttackSpeed()
    {
        currentAtkSpeed = attackSpeed;
    }
    public void ResetMoveSpeed()
    {
        speed = baseSpeed;
        if (Speed > 0) isMoving = true;
    }
    public void SpeedDownAndReset(float downWeight, float waitTime)
    {
        Speed *= downWeight;
        if (Speed <= 0) isMoving = false;
        StartCoroutine(KeepDowningSpeed(waitTime));
    }

    public IEnumerator KeepDowningSpeed(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        ResetMoveSpeed();

    }

}
