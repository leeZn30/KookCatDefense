using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss :Enemy
{

    public Transform skillObj;
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public float skillTime;
    public LayerMask targetMask;
    public bool useGizmo;
    public List<Transform> visibleTargets = new List<Transform>();
    private List<Transform> hitList = new List<Transform>();

    void Start()
    { //플레이 시 FindTargetsDelay 코루틴을 실행한다. 0.5초 간격으로 
        StartCoroutine("FindTargetsDelay", 0.1f);

    }
    void OnDrawGizmos()
    {
        if (useGizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, viewRadius);
        }
    }
    void useSkill(Tower tower)
    {
        if (tower != null)
        {
            animator.SetTrigger("UseSkill");
            Debug.Log("Tower" + tower.info.name + " cut!");
            tower.SetAttckTime(1.5f);
            Vector3 dirToTarget = (tower.transform.position - transform.position).normalized;
            StartCoroutine(MoveSkillObj(dirToTarget));
        }

    }
    IEnumerator MoveSkillObj(Vector3 dirToTarget)
    {
        skillObj.gameObject.SetActive(true);
        float sec = 0.5f;
        for(int i=0; i < (int)(sec*20); i++)
        {
            skillObj.Translate(dirToTarget * viewRadius/(sec*20), Space.World);
            yield return new WaitForSeconds(0.05f);
        }
        skillObj.localPosition = Vector3.zero;
        skillObj.gameObject.SetActive(false);
    }
    
    
    IEnumerator FindTargetsDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            Tower t=FindTargets();
            if (t != null)
            {
                useSkill(t);                 
                yield return new WaitForSeconds(skillTime);
            }
        }
    }

    Tower FindTargets()
    {
        visibleTargets.Clear();
        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetInViewRadius.Length; i++)
        //ViewRadius 안에 있는 타겟의 개수 = 배열의 개수보다 i가 작을 때 for 실행 
        {
            Transform target = targetInViewRadius[i].transform; //타겟[i]의 위치 
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            //vector3타입의 타겟의 방향 변수 선언 = 타겟의 방향벡터, 타겟의 position - 이 게임오브젝트의 position) normalized  = 벡터 크기 정규화 = 단위벡터화
            if (Vector3.Angle(forwardDir, dirToTarget) < viewAngle / 2)
            // 전방 벡터와 타겟방향벡터의 크기가 시야각의 1/2이면 = 시야각 안에 타겟 존재 
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position); //타겟과의 거리를 계산 

                visibleTargets.Add(target.parent);
                Debug.DrawRay(transform.position, dirToTarget * viewRadius, Color.green, 2f);
                
            }
        }
        int idx = 0;
        if (visibleTargets.Count > 0)
        {
            while (visibleTargets.Count > 0)
            {
                idx = Random.Range(0, visibleTargets.Count);
                if (!hitList.Contains(visibleTargets[idx]))
                {
                    hitList.Add(visibleTargets[idx]);
                    return visibleTargets[idx].GetComponent<Tower>();
                }
                visibleTargets.RemoveAt(idx);
            }
            
        }
        return null;


    }
}
