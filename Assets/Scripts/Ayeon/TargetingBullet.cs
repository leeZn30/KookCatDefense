using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingBullet : MonoBehaviour
{
    public GameObject target;
    public float attackDmg;

    public float attackTime;

    Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = (target.transform.position - transform.parent.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            targetPosition = (target.transform.position - transform.parent.position).normalized;
            transform.Translate(targetPosition * Time.deltaTime * attackTime);

        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    // 타겟을 만나면 삭제
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy == target.GetComponent<Enemy>() && !enemy.isDead)
            {
                enemy.AddAffection(attackDmg);
                Destroy(gameObject);

            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
