using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject target;
    public float attackDmg;

    public float attackTime;

    public float max_distance;

    Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = (target.transform.position - transform.parent.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = (target.transform.position - transform.parent.position).normalized;
        transform.Translate(targetPosition * Time.deltaTime * attackTime);

        // �Ѿ��� �ִ� �Ÿ� ������ ������ ����
        float distance = Vector3.Distance(transform.position, transform.parent.position);
        if (distance > max_distance)
        {
            Destroy(gameObject);
        }

        // ȭ�� ��
        //if (transform.position.x < -0.64f || transform.position.x > 13.44f || transform.position.y < -0.64f || transform.position.y > 8.32f)
        //{
            //Destroy(gameObject);
        //}
    }

    // ���� ������ ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {

            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy.isDead == false)
                enemy.AddAffection(attackDmg);

            Destroy(gameObject);
        }
    }
}
