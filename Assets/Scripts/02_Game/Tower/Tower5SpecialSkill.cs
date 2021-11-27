using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower5SpecialSkill : MonoBehaviour
{
    public RectTransform rectTransform;
    Vector3 mousePos;
    bool move_flag;

    public GameObject beam = null;

    TowerTile tt = null;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent.GetComponent<Tower>().skillGague = 0.0f;
        rectTransform = GetComponent<RectTransform>();
        move_flag = true;
        transform.parent.GetComponent<Tower>().attackMode = false;

    }

    // Update is called once per frame
    void Update()
    {
        moveObjcet();
        fixingObject();

        if (Input.GetMouseButtonDown(1) && move_flag)
        {
            Destroy(gameObject);
            transform.parent.GetComponent<Tower>().skillGague = transform.parent.GetComponent<Tower>().maxSkillGauge;
        }
        if (!move_flag) Destroy(gameObject, 5f);
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

                if (hit.transform.CompareTag("Tile"))
                {
                    tt = hit.transform.gameObject.GetComponent<TowerTile>();
                    
                    if (tt.tower == null)
                    {
                        move_flag = false;
                        attack();
                        tt.tower = gameObject;
                    }
                }
            }
        }
    }

    void attack()
    {
        SoundManager.Instance.PlayGameSFX(GameSFX.RazerSkill, 0.1f);
        var beamObj = Instantiate(beam, transform.position, Quaternion.identity, transform);

        beamObj.GetComponent<beam>().target = transform.parent.position;
        beamObj.GetComponent<beam>().start = transform.position;
    }
    private void OnDestroy()
    {
        transform.parent.GetComponent<Tower>().attackMode = true;
        if (tt != null)
        {
            tt.tower = null;
        }
        transform.parent.GetComponent<Tower>().Startco();
    }
}
