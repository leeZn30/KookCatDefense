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
    public int TowerId => info.id;

    public float TowerHp;

    // 나중에 지울 코드
    public bool useGizmo = false;

    void Start()
    {
        skillGague = 0;
        GameManager.Instance.GameOverEvent += () => isStop = true; //게임오버면 멈추기
        // maxSkillGauge���� skillGauge�� �۴ٸ�, �ð����� �������ֱ�
        StartCoroutine("chargeSkillGauge", chargeTime);

        if (TowerId == 4)
        {
            Instantiate(Bullet, transform.position, Quaternion.identity, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStop)
        {
            attack();
            specialSkillAttack();
            showGauge();
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
    void showGauge()
    {
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
        if (collEnemys.Length > 0 && TowerId != 4)
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
                        break;

                    case 3:
                        fTime = 0.0f;
                        var aCatnip = Instantiate(Bullet, transform.position, Quaternion.identity, transform);
                        aCatnip.GetComponent<Catnip>().target = target;
                        break;

                    case 4: // start에서 한번 호출
                        break;

                    case 5:
                        fTime = 0.0f;
                        var aRazer = Instantiate(Bullet, transform.position, Quaternion.identity, transform);
                        
                        Vector3 dir = (target.transform.position - transform.position).normalized;
                        float angle = Vector2.SignedAngle(Vector2.down, dir);
                        Quaternion qut = new Quaternion();
                        qut.eulerAngles = new Vector3(0, 0, angle);
                        aRazer.transform.rotation = qut;
                        Vector3 scale = aRazer.transform.localScale;
                        scale.y = (target.transform.position - gameObject.transform.position).magnitude;
                        aRazer.transform.localScale = scale;
                        aRazer.transform.position += dir * 1.5f; // 거리
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
                case 0:
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
