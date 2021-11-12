using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float attackDmg;

    public float attackTime;

    public float max_distance;

    public Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(targetPosition * Time.deltaTime * attackTime);

        // �Ѿ��� �ִ� �Ÿ� ������ ������ ����
        float distance = Vector3.Distance(transform.position, transform.parent.position);
        if (distance > transform.parent.GetComponent<CircleCollider2D>().radius)
        {
            Destroy(gameObject);
        }

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
