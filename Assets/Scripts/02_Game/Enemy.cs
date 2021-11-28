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

    public float speed;
    public float hitRange;
    public LayerMask layerMask;

    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private float currentAtkSpeed;
    [SerializeField]
    private float curAffection = 0;
    public Transform affection_bar;
    public GameObject objAffection_bar;

    public bool isDead=false;
    
    private bool isMoving = true;
    
    private List<Transform> wayPoints;
    private int currentWayPointIdx=0;
    
    private Rigidbody2D rigidbody2D;
    protected Animator animator;

    public event System.Action OnDeath;
    protected Vector3 forwardDir;

    private Wall attackTarget;

    private IEnumerator coroutine;

    public float AttackSpeed
    {
        set => currentAtkSpeed = Mathf.Max(0, value);
        get => currentAtkSpeed;
    }
    public float Speed
    {
        set => currentSpeed = Mathf.Max(0, value);
        get => currentSpeed;
    }

    void Start()
    {
        coroutine = Move();
    }
    void Update()
    {
        animator.SetFloat("Speed", currentSpeed);
        FindTarget();
    }
    public void SetUp(List<Transform> wps)
    {
        currentSpeed = speed;
        currentAtkSpeed = attackSpeed;


        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); animator.SetFloat("attackSpeed", currentAtkSpeed);
        UpdateAffectionBar();

        wayPoints = new List<Transform> ();
        for(int i=0; i<wps.Count; i++)
        {
            wayPoints.Add(wps[i]);
            if (wps[i].childCount>0)
            {
                int randomInt = Random.Range(0, wps[i].childCount);
                Transform randomWP = wps[i].GetChild(randomInt);
                wayPoints.Add(randomWP);
                for (int j = 0; j < randomWP.childCount; j++)
                {
                    wayPoints.Add(randomWP.GetChild(j));
                }
            }
            

            
        }
        
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

    public IEnumerator Move()
    {
        while (currentWayPointIdx < wayPoints.Count)
        {
            if (isMoving == true)
            {
                Vector3 dir = (wayPoints[currentWayPointIdx].position - transform.position).normalized;
                forwardDir = dir;

                animator.SetFloat("MoveX", dir.x);
                animator.SetFloat("MoveY", dir.y);


                transform.position += currentSpeed * dir * Time.deltaTime;
                if (Vector3.Distance(transform.position, wayPoints[currentWayPointIdx].position) < 0.02f * currentSpeed)
                {
                    if (currentWayPointIdx == wayPoints.Count - 1)//마지막 위치일때
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

    public void MoveToTarget(GameObject targetPosition)
    {
        Vector3 dir = (targetPosition.transform.position - gameObject.transform.position).normalized;
        animator.SetFloat("MoveX", dir.x);
        animator.SetFloat("MoveY", dir.y);
        //Debug.Log("Done");

        gameObject.transform.position += currentSpeed * dir * Time.deltaTime;
        /*transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition.transform.position, speed);*/

        if (Vector3.Distance(gameObject.transform.position, targetPosition.transform.position) < 0.02f * currentSpeed)
            {
                currentSpeed = 0.0f;
            }
    }

    public void test()
    {
        StopCoroutine(coroutine);
        Debug.Log("Done");
    }


    private void AttackEvent()
    {//attack �ִϸ��̼� ����� �̺�Ʈ �޼���� ȣ���

        if (attackTarget!= null)
        {
            attackTarget.AddHp(-attackDmg);   
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
    private void FindTarget()
    {
        RaycastHit hitinfo;
        Debug.DrawRay(transform.position, forwardDir * hitRange, Color.green);
        if (Physics.Raycast(transform.position, forwardDir, out hitinfo, hitRange, layerMask))
        {
            if (attackTarget == null)
            {
                attackTarget = hitinfo.transform.gameObject.GetComponent<Wall>();
                SetMoving(false);
            }
        }
        else
        {

            SetMoving(true);
            
        }
        
    }
    private void DieEvent()
    {//death �ִϸ��̼� ����� �̺�Ʈ �޼���� ȣ���.
        Destroy(gameObject);
    }

    public void ResetAttackSpeed()
    {
        currentAtkSpeed = attackSpeed;
    }
    public void ResetMoveSpeed()
    {
        currentSpeed = speed;
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
