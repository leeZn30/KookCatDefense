using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int id;
    public string enemyName;
    public float affection;
    public float speed;

    public Transform affection_bar;

    private float curAffection = 0;
    private Transform[] wayPoints;
    private int currentWayPointIdx=1;
    private Rigidbody2D rigidbody2D;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        UpdateAffectionBar();
    }
    public void SetUp(Transform[] wps)
    {
        wayPoints = new Transform[wps.Length];
        wayPoints = wps;

        transform.position = wayPoints[currentWayPointIdx++].position;
        StartCoroutine(Move());
    }
    void AddAffection(float value) 
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
            //남은 고양이수 나중에 여기서 처리
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Weapon")
        {//충돌한 오브젝트가 weapon일때
            float power = 2;
            Debug.Log("in Enemy.cs : OnTriggerEnter2D - 공격력 : "+power);
            AddAffection(power);
        }
    }
    IEnumerator Move()
    {
        while (currentWayPointIdx < wayPoints.Length)
        {
            Vector3 dir = (wayPoints[currentWayPointIdx].position - transform.position).normalized;
            transform.position += speed * dir *Time.deltaTime;
            if (Vector3.Distance(transform.position, wayPoints[currentWayPointIdx].position) < 0.02f*speed)
            {
                if (currentWayPointIdx == wayPoints.Length - 1)
                {
                    Destroy(gameObject);
                    break;
                }
                transform.position = wayPoints[currentWayPointIdx].position;
                currentWayPointIdx++;
            }
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.1f);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
