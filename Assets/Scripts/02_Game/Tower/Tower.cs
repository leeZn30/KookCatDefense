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
    public float maxSkillGauge = 100.0f;
    float chargeTime = 0.5f;
    public float attackTime;
    public float attackDmg;

    public float baseAttackTime;
    public float baseAttackDmg;

    // �Ѿ�
    public GameObject Bullet = null;

    // Ư����ų ����
    public GameObject specialSkill = null;

    // �⺻ ���� �ð�
    private float fTime = 0.0f;

    // ���콺 ����
    bool isOver = false;

    public float hitSize;

    private bool isStop = false;
    public float Price => info.price;
    public string Name => info.name;
    public string Content => info.content;
    public int TowerId => info.id;

    public bool attackMode;
    public bool specialattackMode;

    // 나중에 지울 코드
    public bool useGizmo = false;

    public void Sell()
    {
        GameManager.Instance.coin += (int)Price;
        Destroy(gameObject);
    }

    void Start()
    {
        skillGague = 0;
        baseAttackTime = attackTime;
        baseAttackDmg = attackDmg;
        GameManager.Instance.GameOverEvent += () => isStop = true; //게임오버면 멈추기
        StartCoroutine("chargeSkillGauge", chargeTime);

        if (TowerId == 4)
        {
            Instantiate(Bullet, transform.position, Quaternion.identity, transform);
        }

        attackMode = true;
        specialattackMode = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStop)
        {
            if (attackMode)
            {
                attack();
            }

            if (specialattackMode)
            {
                specialSkillAttack();
            }
        }

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
                    GameObject enemy = collEnmeys[i].gameObject;
                    if (enemy.tag == "Enemy" && enemy.GetComponent<Enemy>().Speed != 0)
                    {
                        if (enemy != null)
                            return enemy;
                    }
                }
                break;

            default:
                for (int i = 0; i < collEnmeys.Length; i++)
                {
                    GameObject enemy = collEnmeys[i].gameObject;
                    if (enemy.tag == "Enemy")
                    {
                        if (enemy != null)
                            return enemy;
                    }
                }
                break;
        }
        return null;
    }

    void attack()
    {
        Collider2D[] collEnemys = Physics2D.OverlapCircleAll(transform.position, hitSize);

        fTime += Time.deltaTime;
        if (collEnemys.Length > 0 && (TowerId != 4 && TowerId != 0))
        {
            GameObject target = targetSearch(collEnemys, TowerId);
            
            if (target != null && fTime >= attackTime)
            {
                switch (TowerId)
                {
                    case 0:
                        break;

                    case 1:
                        fTime = 0.0f;
                        var aTargettingBullet = Instantiate(Bullet, transform.position, Quaternion.identity, transform);
                        aTargettingBullet.GetComponent<TargetingBullet>().target = target;
                        aTargettingBullet.GetComponent<TargetingBullet>().attackDmg = attackDmg;
                        break;

                    case 2:
                        fTime = 0.0f;
                        var aChur = Instantiate(Bullet, target.transform.position, Quaternion.identity, transform);
                        aChur.GetComponent<Chur>().target = target;
                        aChur.GetComponent<Chur>().attackDmg = attackDmg;
                        break;

                    case 3:
                        fTime = 0.0f;
                        var aCatnip = Instantiate(Bullet, transform.position, Quaternion.identity, transform);
                        aCatnip.GetComponent<Catnip>().target = target;
                        break;

                    case 5:
                        fTime = 0.0f;
                        var aRazer = Instantiate(Bullet, transform.position, Quaternion.identity, transform);
                        aRazer.GetComponent<Razer>().attackDmg = attackDmg;
                        Vector3 dir = (target.transform.position - transform.position).normalized;
                        float angle = Vector2.SignedAngle(Vector2.down, dir);
                        Quaternion qut = new Quaternion();
                        qut.eulerAngles = new Vector3(0, 0, angle);
                        aRazer.transform.rotation = qut;
                        aRazer.transform.position += dir * 2f; // 거리
                        break;

                    case 6:
                        break;

                    default:
                        fTime = 0.0f;
                        var aBullet = Instantiate(Bullet, transform.position, Quaternion.identity, transform);
                        aBullet.GetComponent<Bullet>().target= target;
                        aBullet.GetComponent<Bullet>().attackDmg = attackDmg;
                        break;
                }
            }
        }
    }

    void specialSkillAttack()
    {   
        if (TowerId != 4)
        {
            if (skillGague >= maxSkillGauge && (isOver && Input.GetMouseButtonUp(0)))
            {
                var specialAttack = Instantiate(specialSkill, transform.position, Quaternion.identity, transform);
                skillGague = 0.0f;
            }

        }
        else
        {
            if (skillGague >= maxSkillGauge && (Input.GetMouseButtonUp(0)))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Transform hitTransform = null;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    hitTransform = hit.transform;

                    if (hit.transform.parent == transform)
                    {
                        var specialAttack = Instantiate(specialSkill, transform.position, Quaternion.identity, transform);
                    }
                }
            }

        }

    }
    public void changeAndresetattackTime(float duration, float change)
    {
        attackTime *= change;
        StartCoroutine(resetattackTime(duration));
    }
    IEnumerator resetattackTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        attackTime = baseAttackTime;
    }

    public void changeAndresetattackDmg(float duration, float change)
    {
        attackDmg *= change;
        StartCoroutine(resetattackDmg(duration));
    }
    IEnumerator resetattackDmg(float duration)
    {
        yield return new WaitForSeconds(duration);
        attackDmg = baseAttackDmg;
    }

    public void onSpecialSkillMode()
    {
        if (!specialattackMode)
            specialattackMode = true;
    }

    void OnDrawGizmos()
    {
        if (useGizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, hitSize);
        }
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
