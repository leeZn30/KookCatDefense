using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Razer : MonoBehaviour
{
    public float attackDmg;
    public float hitsize;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 0.3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (!enemy.isDead)
            {
                enemy.AddAffection(attackDmg);
            }
        }
    }

}
