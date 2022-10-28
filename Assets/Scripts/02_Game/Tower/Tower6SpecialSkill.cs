using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower6SpecialSkill : MonoBehaviour
{
    List<string> buffList = new List<string>() { "speedBuff", "DmgBuff", "fillSkillGauge" };
    public Sprite speedBuff;
    public Sprite DmgBuff;
    public Sprite fillSkillGauge;

    public RectTransform rectTransform;
    public SpriteRenderer renderer;
    Vector3 mousePos;
    bool move_flag = true;

    string BuffType;

    Tower tower;


    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        ChooseBuff();
        transform.parent.GetComponent<Tower>().skillGague = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        moveObjcet();
        fixingObject();
        if (Input.GetMouseButtonDown(1) && move_flag)
        {
            Destroy(gameObject);
        }
    }

    void ChooseBuff()
    {
        int randomNum = Random.Range(0, buffList.Count);
        BuffType = buffList[randomNum];

        Debug.Log("BuffType: " + BuffType);

        switch (BuffType)
        {
            case "speedBuff":
                renderer.sprite = speedBuff;
                break;

            case "DmgBuff":
                renderer.sprite = DmgBuff;
                break;

            case "fillSkillGauge":
                renderer.sprite = fillSkillGauge;
                break;

            default:
                Debug.Log("wrong");
                break;
        }
    }

    void moveObjcet()
    {
        if (move_flag)
        {
            Vector3 pos = Input.mousePosition;
            pos.z += 10.0f;

            mousePos = Camera.main.ScreenToWorldPoint(pos);
            rectTransform.position = mousePos;
        }
    }


    void fixingObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Transform hitTransform = null;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                hitTransform = hit.transform;

                if (hit.transform.parent.CompareTag("Tower"))
                {
                    tower = hit.transform.parent.gameObject.GetComponent<Tower>();
                    Debug.Log(tower);
                    tower.specialattackMode = false;
                    attack(tower);
                }
            }
        }
    }

    void attack(Tower tower)
    {
        switch (BuffType)
        {
            case "speedBuff":
                tower.isBuffed = true;
                tower.changeAndresetattackTime(5f, 0.5f);
                break;

            case "DmgBuff":
                tower.isBuffed = true;
                tower.changeAndresetattackDmg(5f, 1.5f);
                break;

            case "fillSkillGauge":
                tower.skillGague = tower.maxSkillGauge;
                break;

            default:
                Debug.Log("wrong");
                break;
        }
        Destroy(gameObject, 0.2f);
    }

    private void OnDestroy()
    {
        if (transform.parent.gameObject.GetComponent<Tower>().isActiveAndEnabled)
            transform.parent.GetComponent<Tower>().Startco();
        if (tower != null) tower.onSpecialSkillMode();
    }

}
