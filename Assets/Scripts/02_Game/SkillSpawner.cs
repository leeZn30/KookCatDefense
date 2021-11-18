using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] skillPrefab;
    [SerializeField]
    private GameObject followskillPrefab;
    private bool isOnSkillButton = false;
    private GameObject followTowerClone = null;
    private GameObject skill = null;
    private int skillType;
    private void Start()
    {
        
        for (int i = 0; i < skillPrefab.Length; i++)
        {
            skillPrefab[i] = Resources.Load<GameObject>("Prefabs/Skill/Skill" + GameData.Instance.selectedSkills[i]);
        }
    }
    public void ReadytoSpawnSkill(int type)
    {
        skillType = type;

        //towerPrefab[towerType].;

        if (isOnSkillButton == true)
        {
            return;
        }
        
        isOnSkillButton = true;

        followTowerClone = Instantiate(followskillPrefab);

        StartCoroutine("OnSkillCancelSystem");
    }

    public void SpawnSkill(Transform tileTransform)
    {
        if (isOnSkillButton == false)
        {
            return;
        }

        Tile tile = tileTransform.GetComponent<Tile>();

        isOnSkillButton = false;

        skill = Instantiate(skillPrefab[skillType], tileTransform.position, Quaternion.identity);

        Destroy(followTowerClone);

        StopCoroutine("OnSkillCancelSystem");

        Destroy(skill, 5);
    }

    private IEnumerator OnSkillCancelSystem()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                isOnSkillButton = false;
                Destroy(followTowerClone);
                break;
            }

            yield return null;
        }
    }
}
