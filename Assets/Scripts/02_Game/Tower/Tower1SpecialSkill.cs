using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower1SpecialSkill : MonoBehaviour
{
    public RectTransform rectTransform;
    Vector3 mousePos;
    bool move_flag;

    public float attackDmg;

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
        if (Input.GetMouseButtonDown(0))
        {
            fixingObject();
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
        move_flag = false;
        attack();
    }

    void attack()
    {

        if (collEnemys.Count > 0)
        {
            foreach (GameObject go in collEnemys)
            {
                go.GetComponent<Enemy>().AddAffection(attackDmg);
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

    private void OnDestroy()
    {
        transform.parent.GetComponent<Tower>().Startco();
    }
}
