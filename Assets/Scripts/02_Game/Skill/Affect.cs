using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Affect : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return;
        }

        Enemy enemy = collision.GetComponent<Enemy>();

        enemy.AddAffection(10);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return;
        }
    }
}
