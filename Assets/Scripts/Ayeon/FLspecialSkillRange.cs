using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLspecialSkillRange : MonoBehaviour
{
    public RectTransform rectTransform;
    Vector3 mousePos;
    bool move_flag;

    private List<GameObject> collEnemys = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
       rectTransform = GetComponent<RectTransform>();
        move_flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        moveObjcet();
        fixingObject();
        Debug.Log(collEnemys.Count);
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
            move_flag = false;
            //Debug.Log("fixed: " + rectTransform.position);
            attack();
        }
    }

    void attack()
    {

        if (collEnemys.Count > 0)
        {
            // 아직 몬스터 객체가 없어서 임시 코드
            foreach (GameObject go in collEnemys)
            {
                go.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            collEnemys.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (GameObject go in collEnemys)
        {
            if (go == collision.gameObject)
            {
                collEnemys.Remove(go);
                break;
            }
        }
    }
}
