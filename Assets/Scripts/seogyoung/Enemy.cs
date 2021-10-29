using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int id;
    public string enemyName;
    public float affection;
    public float speed;

    private Transform[] wayPoints;
    private int currentWayPointIdx=1;

    public void SetUp(Transform[] wps)
    {
        wayPoints = new Transform[wps.Length];
        wayPoints = wps;

        transform.position = wayPoints[currentWayPointIdx++].position;
        StartCoroutine(Move());
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
