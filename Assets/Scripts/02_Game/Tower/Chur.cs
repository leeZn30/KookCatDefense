using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chur : MonoBehaviour
{
    public GameObject target = null;
    public Vector3 targetPosition;
    public float attackDmg;
    public List<Enemy> enterEnemys = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayGameSFX(GameSFX.Chew,0.1f);
        targetPosition = target.transform.position;
        transform.position = targetPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            targetPosition = target.transform.position;
            transform.position = targetPosition;
        }

    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            if (!enemy.isDead)
            {

                enterEnemys.Add(enemy);

                enterEnemys[0].AddAffection(attackDmg);

                Destroy(gameObject, 0.8f);
            }

        }
    }

}
