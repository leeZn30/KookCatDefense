using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    public int TowerId;

    public float price;

    // skill ���� ����

    public float skillGague;
    float maxSkillGauge = 100.0f;
    float chargeTime = 0.5f;
    public float attackTime;

    // �� ����Ʈ
    public List<GameObject> collEnemys = new List<GameObject>();

    // �Ѿ�
    public GameObject Bullet = null;

    // Ư����ų ����
    public GameObject specialSkill;

    // �⺻ ���� �ð�
    private float fTime = 0.0f;

    // ���콺 ����
    bool isOver = false;

    public float Price => price;

    void Start()
    {
        skillGague = 0;
        // maxSkillGauge���� skillGauge�� �۴ٸ�, �ð����� �������ֱ�
        StartCoroutine("chargeSkillGauge", chargeTime);

    }

    // Update is called once per frame
    void Update()
    {
        attack();
        deleteCollEnemys();

        if (isOver && Input.GetMouseButtonDown(0))
        {
            if (skillGague >= maxSkillGauge)
            {
                Debug.Log("max charge");
                
                specialSkillAttack();
                
            }
        }
        /*
        if (skillGague >= maxSkillGauge)
        {
            Debug.Log("max charge");
            if (isOver && Input.GetMouseButtonDown(0))
            {
                specialSkillAttack();
            }
        }
    */

    }
    public void Startco()
    {
        skillGague = 0.0f;
        StartCoroutine("chargeSkillGauge", chargeTime);
    }
    public IEnumerator chargeSkillGauge(float chargeTime)
    {
        /*
        if (skillGague < maxSkillGauge)
        {
            yield return new WaitForSeconds(chargeTime);
            StartCoroutine("chargeSkillGauge", chargeTime);
            //Debug.Log("Gauge: " + skillGague);
            skillGague += 10.0f;
        }
        */
        while(skillGague < maxSkillGauge)
        {
            yield return new WaitForSeconds(chargeTime);
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
                switch (TowerId)
                {
                    case 1:
                        fTime = 0.0f;
                        var aTargettingBullet = Instantiate(Bullet, transform.position, Quaternion.identity, transform);
                        aTargettingBullet.GetComponent<TargetingBullet>().target = target;
                        break;

                    case 3:
                        fTime = 0.0f;
                        var aCatnip = Instantiate(Bullet, transform.position, Quaternion.identity, transform);
                        foreach (GameObject enemy in collEnemys)
                            if (enemy.GetComponent<Enemy>().Speed == 0)
                                continue;
                            else target = enemy;
                            aCatnip.GetComponent<Catnip>().target = target;
                        break;

                    case 5:
                        fTime = 0.0f;
                        var aRazer = Instantiate(Bullet, transform.position, Quaternion.identity, transform);
                        Vector3 dir = (target.transform.position - transform.position).normalized;
                        float angle = Vector2.SignedAngle(Vector2.down, dir);
                        Quaternion qut = new Quaternion();
                        qut.eulerAngles = new Vector3(0, 0, angle);
                        aRazer.transform.rotation = qut;
                        aRazer.transform.position += dir * 1.0f;
                        break;

                    default:
                        fTime = 0.0f;
                        var aBullet = Instantiate(Bullet, transform.position, Quaternion.identity, transform);
                        aBullet.GetComponent<Bullet>().target= target;
                        break;
                }
            }

        }
    }

    void specialSkillAttack()
    {
        switch (TowerId)
        {
            case 2: // no special skill
                break;
            default:
                var speciaAttack = Instantiate(specialSkill, transform.position, Quaternion.identity, transform);
                /*
                skillGague = 0.0f;
                StartCoroutine("chargeSkillGauge", chargeTime);
                */
                break;


        }
    }

    // collenemys를 검사해서 죽은 몬스터는 지워줌
    void deleteCollEnemys()
    {
        foreach (GameObject go in collEnemys)
        {
            if (go == null) // or isDead?
            {
                collEnemys.Remove(go);
                break;
            }
        }
    }

    void OnMouseOver()
    {
        //Debug.Log("Enter!");
        if (isOver == false)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                isOver = true;
            }

        }
    }

    void OnMouseExit()
    {
        //Debug.Log("Exit!");
        if (isOver == true)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                isOver = false;
            }


        }

    }

}
