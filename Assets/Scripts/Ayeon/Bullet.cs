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

        // 나와 부모의 사이가 일정거리 도달하면 삭제
        float distance = Vector3.Distance(transform.position, transform.parent.position);
        if (distance > max_distance)
        {
            Destroy(gameObject);
        }

        // 화면 밖
        //if (transform.position.x < -0.64f || transform.position.x > 13.44f || transform.position.y < -0.64f || transform.position.y > 8.32f)
        //{
            //Destroy(gameObject);
        //}
    }

    // 적을 만나면 삭제
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {

            collision.GetComponent<Enemy>().AddAffection(attackDmg);
            Destroy(gameObject);
        }
    }
}
