using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedLauncher : MonoBehaviour
{
    // Start is called before the first frame update

    // skill 관련 변수
    float skillGague;
    float maxSkillGauge = 100.0f;
    float chargeTime = 0.5f;
    float attackTime = 0.5f;

    // 적 리스트
    private List<GameObject> collEnemys = new List<GameObject>();

    // 총알
    public GameObject Feed = null;

    // 특수스킬 범위
    public GameObject specialSkillRange = null;

    // 기본 공격 시간
    private float fTime = 0.0f;

    void Start()
    {
        skillGague = 0;
        // maxSkillGauge보다 skillGauge가 작다면, 시간별로 충전해주기
        StartCoroutine("chargeSkillGauge", chargeTime);

    }

    // Update is called once per frame
    void Update()
    {
        attack();

        if (skillGague >= maxSkillGauge)
        {
            if (Input.GetMouseButtonDown(0))
            {
                specialSkillAttack();
                //skillGague = 0.0f;
                //StartCoroutine("chargeSkillGauge", chargeTime);
            }
        }

    }

    public IEnumerator chargeSkillGauge(float chargeTime)
    {
        if (skillGague < maxSkillGauge)
        {
            yield return new WaitForSeconds(chargeTime);
            StartCoroutine("chargeSkillGauge", chargeTime);
            Debug.Log("Gauge: " + skillGague);
            skillGague += 10.0f;
        }
    }

    void attack()
    {
        fTime += Time.deltaTime;
        if (collEnemys.Count > 0)
        {
            GameObject target = collEnemys[0];
            
            if (target != null && fTime > attackTime)
            {
                fTime = 0.0f;
                var aFeed = Instantiate(Feed, transform.position, Quaternion.identity, transform);
                aFeed.GetComponent<Feed>().targetPosition = (target.transform.position - transform.position).normalized;
                target.GetComponent<SpriteRenderer>().color = Color.blue;
            }

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            collEnemys.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (GameObject go in collEnemys)
        {
            if (go == collision.gameObject)
            {
                collEnemys.Remove(go);
                break;
            }
        }
    }

    void specialSkillAttack()
    {
        // 일단 시간을 멈추고
        // Time.timeScale = 0; 멈추면 특수 스킬 공격이 안먹어서 일단 보류

        // 커서를 따라다니면서 공격 범위 표시
        var specialSkill = Instantiate(specialSkillRange, transform.position, Quaternion.identity, transform);

        skillGague = 0.0f;
        StartCoroutine("chargeSkillGauge", chargeTime);
    }


}
