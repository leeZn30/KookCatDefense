using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpawner : MonoBehaviour
{
    [SerializeField]
    private TowerDataViewer towerDataViewer;
    [SerializeField]
    private GameObject[] skillPrefab;
    [SerializeField]
    private GameObject[] followskillPrefab;
    private bool isOnSkillButton = false;
    private GameObject followSkillClone = null;
    private GameObject skillClone = null;
    private int skillType;

    ObjectDetector objectDetector;
    private void Start()
    {
        
        for (int i = 0; i < skillPrefab.Length; i++)
        {
            skillPrefab[i] = Resources.Load<GameObject>("Prefabs/Skill/Skill" + GameData.Instance.selectedSkills[i]);
            followskillPrefab[i] = Resources.Load<GameObject>("Prefabs/UI/FollowSkill" + GameData.Instance.selectedSkills[i]); 
        }
    }
    public void ReadytoSpawnSkill(int type)
    {
        skillType = type;

        towerDataViewer.OnPanelSkill(skillPrefab[skillType]);

        Skill skill = skillPrefab[skillType].GetComponent<Skill>();

        if (isOnSkillButton == true)
        {
            return;
        }

        if (skill.Price > GameManager.Instance.coin)
        {
            return;
        }
        
        isOnSkillButton = true;

        followSkillClone = Instantiate(followskillPrefab[skillType]);

        StartCoroutine("OnSkillCancelSystem");
    }

    public void SpawnSkill(Vector3 tileTransform)
    {
        if (isOnSkillButton == false)
        {
            return;
        }

        //Tile tile = tileTransform.GetComponent<Tile>();

        isOnSkillButton = false;

        Skill skill = skillPrefab[skillType].GetComponent<Skill>();

        GameManager.Instance.coin -= (int)skill.Price;

        skillClone = Instantiate(skillPrefab[skillType], tileTransform, Quaternion.identity);

        Destroy(followSkillClone);

        StopCoroutine("OnSkillCancelSystem");

        towerDataViewer.OffPanel();

        //Destroy(skillClone, 5); 이거 스킬마다 다르게 적용되게 바꿔주세요
        //if 
        //skill script에 소멸시간 받아서 코루틴으로 짜기(자체 소멸하지않는 것 이랑 구분 )
        //스킬 3은 애니메이션 이벤트로 코루틴 없이 소멸해서 여기서 먼저 사라지면 안될 것 같아요
        if(skill.info.id==0)//임시
        {
            //yield return new WaitForSeconds(5);
            Destroy(skillClone, 5);
        }
        else if (skill.info.id == 1)
        {
            //yield return new WaitForSeconds(5);
            //List<Enemy> slowEnemyList = skillClone.transform.Find("SlowCollider2D").GetComponent<Slow>().collidedEnemy;
            
            Destroy(skillClone, 5);

            /*if (slowEnemyList != null){
                //Enemy[] slowEnemyArr = ToArray();
                for (int i=0; i<slowEnemyList.Count; i++){
                    slowEnemyList[i].ResetMoveSpeed();
                }
            }*/
        }
        else if (skill.info.id == 2 || skill.info.id == 5)
        {
            //yield return new WaitForSeconds(5);
            //List<Enemy> stopEnemyList = skillClone.transform.Find("StopCollider2D").GetComponent<Stop>().collidedEnemy;
            
            Destroy(skillClone, 5);

            /*if (stopEnemyList != null){
                //Enemy[] slowEnemyArr = ToArray();
                for (int i=0; i<stopEnemyList.Count; i++){
                    stopEnemyList[i].ResetMoveSpeed();
                }
            }*/
        }
        //objectDetector = GameObject.Find("ObjectDetector").GetComponent<ObjectDetector>();
        //objectDetector.runningCoroutine = null;
    }

    private IEnumerator OnSkillCancelSystem()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                isOnSkillButton = false;
                Destroy(followSkillClone);
                towerDataViewer.OffPanel();
                break;
            }

            yield return null;
        }
    }
}
