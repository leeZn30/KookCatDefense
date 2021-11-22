using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlow : MonoBehaviour
{
    public float hitSize;
    public bool useGizmo;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Collider2D[] collEnemys = Physics2D.OverlapCircleAll(transform.position, hitSize);

        for (int i = 0; i < collEnemys.Length; i++)
        {
            if (collEnemys[i] != null && collEnemys[i].tag == "Enemy")
            {
                collEnemys[i].GetComponent<Enemy>().Speed = 1f;
            }
        }
        */
    }

    /*
    void OnDrawGizmos()
    {
        if (useGizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, hitSize);
        }
    }
    */

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            enemy.Speed *= 0.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            enemy.ResetMoveSpeed();

        }
    }
    
}
