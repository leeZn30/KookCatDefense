using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingBullet : MonoBehaviour
{
    public GameObject target;
    public float attackDmg;

    public float attackSpeed;

    Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManager.Instance.PlayGameSFX(GameSFX.Bullet);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            //targetPosition = (target.transform.position - transform.parent.position).normalized;
            Vector3 pos = (target.transform.position - transform.position).normalized;
            // 내적(dot)을 통해 각도를 구함. (Acos로 나온 각도는 방향을 알 수가 없음)
            float dot = Vector3.Dot(transform.up, pos);
            if (dot < 1.0f)
            {
                float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

                // 외적을 통해 각도의 방향을 판별.
                Vector3 cross = Vector3.Cross(transform.up, pos);
                // 외적 결과 값에 따라 각도 반영
                if (cross.z < 0)
                {
                    angle = transform.rotation.eulerAngles.z - Mathf.Min(10, angle);
                }
                else
                {
                    angle = transform.rotation.eulerAngles.z + Mathf.Min(10, angle);
                }

                // angle이 윗 방향과 target의 각도.
                // do someting.
                transform.Translate(pos * Time.deltaTime * attackSpeed);
            }

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

}
