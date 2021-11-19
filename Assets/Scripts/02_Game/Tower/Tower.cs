using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    //public int TowerId;
    public ItemInfo info;

    // skill ���� ����
    public float skillGague;
    float maxSkillGauge = 100.0f;
    float chargeTime = 0.5f;
    public float attackTime;

    // �Ѿ�
    public GameObject Bullet = null;

    // Ư����ų ����
    public GameObject specialSkill = null;

    // �⺻ ���� �ð�
    private float fTime = 0.0f;

    // ���콺 ����
    bool isOver = false;

    public float hitSize;

    public float Price => info.price;
    public int TowerId => info.id;

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
        specialSkillAttack();
    }
    public void Startco()
    {
        StartCoroutine("chargeSkillGauge", chargeTime);
    }

    public IEnumerator chargeSkillGauge(float chargeTime)
    {
        while (skillGague < maxSkillGauge)
        {
            yield return new WaitForSeconds(chargeTime);
            skillGague += 10.0f;
        }
    }

    GameObject targetSearch(Collider2D[] collEnmeys, int TowerId)
    {
        switch (TowerId)
        {
            case 3:
                for (int i = 0; i < collEnmeys.Length; i++)
                {
                    if (collEnmeys[i].tag == "Enemy")
                    {
                        return collEnmeys[i].gameObject;
                    }
                    else
                        return null;
                }
                break;

            default:
                for (int i = 0; i < collEnmeys.Length; i++)
                {
                    GameObject enemy = collEnmeys[i].gameObject;
                    if (enemy.tag == "Enemy" && enemy.GetComponent<Enemy>().Speed != 0)
                    {
                        return enemy;
                    }
                    else return null;
                }
                break;
        }
        return null;
    }

    void attack()
    {
        Collider2D[] collEnemys = Physics2D.OverlapCircleAll(transform.position, hitSize);

        fTime += Time.deltaTime;
        if (collEnemys.Length > 0)
        {
            GameObject target = targetSearch(collEnemys, TowerId);
            
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
        if (skillGague >= maxSkillGauge && (isOver && Input.GetMouseButtonUp(0)))
        {
            switch (TowerId)
            {
                case 2: // no special skill
                    break;

                case 3:
                    var tower3SpecialSkill = Instantiate(specialSkill, transform.position, Quaternion.identity, transform);
                    break;

                default:
                    var speciaAttack = Instantiate(specialSkill, transform.position, Quaternion.identity, transform);
                    break;
            }
            skillGague = 0.0f;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitSize);
    }
    
    void OnMouseOver()
    {
        
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
        if (isOver == true)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                isOver = false;
            }
        }
    }
    

}
