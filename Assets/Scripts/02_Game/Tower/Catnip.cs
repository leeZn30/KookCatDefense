using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catnip : MonoBehaviour
{
    public float attackTime;

    public GameObject target = null;
    public Vector3 targetPosition;

    public List<Enemy> enterEnemys = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
        {
            targetPosition = (target.transform.position - transform.position).normalized;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.Translate(targetPosition * Time.deltaTime * attackTime);
        }
        else
        {
            Destroy(gameObject);
        }

        float distance = Vector3.Distance(transform.position, transform.parent.position);
        if (distance > transform.parent.GetComponentInChildren<CircleCollider2D>().radius)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            if (!enemy.isDead)
            {
                enterEnemys.Add(enemy);

                enterEnemys[0].SpeedDownAndReset(3.0f);
                Destroy(gameObject);
            }

        }
    }
}