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

    AnimatorControllerParameter animator = null;

    // Start is called before the first frame update
    void Start()
    {
       rectTransform = GetComponent<RectTransform>();
       move_flag = true;
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

            transform.parent.GetComponent<Tower>().skillGague = transform.parent.GetComponent<Tower>().maxSkillGauge;
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

                if (hit.transform.CompareTag("Road"))
                {
                    move_flag = false;
                    attack();
                }
            }
        }
    }

    void attack()
    {
        SoundManager.Instance.PlayGameSFX(GameSFX.Meteor,0.1f);
        SoundManager.Instance.PlayGameSFX(GameSFX.Meteor);
        SoundManager.Instance.PlayGameSFX(GameSFX.Meteor, 0.3f);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponentInChildren<Animator>().enabled = true;

        if (collEnemys.Count > 0)
        {
            foreach (GameObject go in collEnemys)
            {
                go.GetComponent<Enemy>().AddAffection(attackDmg);
            }
        }
        Destroy(gameObject, 1f);
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
