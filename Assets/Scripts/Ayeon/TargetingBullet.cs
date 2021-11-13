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
        targetPosition = (target.transform.position - transform.parent.position).normalized;
        transform.Translate(targetPosition * Time.deltaTime * attackTime);

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
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy.isDead == false)
                enemy.AddAffection(attackDmg);
            Destroy(gameObject);
        }
    }

}
