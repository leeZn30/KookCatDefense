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
    private TowerSpawner towerSpawner;
    public bool isOnSkillButton = false;
    public GameObject followSkillClone = null;
    private GameObject skillClone = null;
    private int skillType;

    //ObjectDetector objectDetector;
    private void Start()
    {
        towerSpawner = GameObject.Find("TowerSpawner").GetComponent<TowerSpawner>();
               
        for (int i = 0; i < skillPrefab.Length; i++)
        {
            skillPrefab[i] = Resources.Load<GameObject>("Prefabs/Skill/Skill" + GameData.Instance.selectedSkills[i]);
            followskillPrefab[i] = Resources.Load<GameObject>("Prefabs/UI/FollowSkill" + GameData.Instance.selectedSkills[i]); 
        }
    }
    public void ReadytoSpawnSkill(int type)
    {

        isOnSkillButton = false;
        towerSpawner.isOnTowerButton = false;

        Destroy(followSkillClone);
        Destroy(towerSpawner.followTowerClone);

        skillType = type;

        towerDataViewer.OnPanelSkill(skillPrefab[skillType]);

        Skill skill = skillPrefab[skillType].GetComponent<Skill>();

        if (isOnSkillButton == true)
        {
            return;
        }

        if (skill.Price > GameManager.Instance.coin)
        {
            SoundManager.Instance.PlaySFX(SFX.bbibbi);
            return;
        }
        SoundManager.Instance.PlaySFX(SFX.ButtonClick);
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
        SoundManager.Instance.PlayGameSFX(GameSFX.Pang);
        //Tile tile = tileTransform.GetComponent<Tile>();

        isOnSkillButton = false;

        Skill skill = skillPrefab[skillType].GetComponent<Skill>();


        GameManager.Instance.coin -= (int)skill.Price;

        skillClone = Instantiate(skillPrefab[skillType], tileTransform, Quaternion.identity);

        Destroy(followSkillClone);

        StopCoroutine("OnSkillCancelSystem");

        towerDataViewer.OffPanel();

        //Destroy(skillClone, 5); �̰� ��ų���� �ٸ��� ����ǰ� �ٲ��ּ���
        //if 
        //skill script�� �Ҹ�ð� �޾Ƽ� �ڷ�ƾ���� ¥��(��ü �Ҹ������ʴ� �� �̶� ���� )
        //��ų 3�� �ִϸ��̼� �̺�Ʈ�� �ڷ�ƾ ���� �Ҹ��ؼ� ���⼭ ���� ������� �ȵ� �� ���ƿ�
        if(skill.info.id==0)//�ӽ�
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
