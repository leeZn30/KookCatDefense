using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower2SpecialSkill : MonoBehaviour
{
    private List<GameObject> collEnemys = new List<GameObject>();
    public float attackDmg;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent.GetComponent<Tower>().skillGague = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.Instance.PlayGameSFX(GameSFX.ChurSkill, 0.1f);
        if (collision.tag == "Enemy")
        {
            collision.transform.gameObject.GetComponent<Enemy>().AddAffection(attackDmg);
        }
    }


    private void OnDestroy()
    {
        if (transform.parent.gameObject.GetComponent<Tower>().isActiveAndEnabled)
            transform.parent.GetComponent<Tower>().Startco();
    }
}
