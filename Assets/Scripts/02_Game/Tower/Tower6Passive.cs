using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower6Passive : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isUpActive = true;
    public bool isDownActive = true;
    public bool isRightActive = true;
    public bool isLeftActive = true;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*
    void UpTowerCheck()
    {
        Vector2 a = new Vector2(transform.position.x, transform.position.y + 2.5f);
        Vector2 b = new Vector2(transform.position.x, transform.position.y);
        Collider[] Towers = Physics.OverlapCapsule(a, b, transform.localScale.x/2);

        foreach (Collider coll in Towers)
        {
            if (coll.tag == "Tower")
            {
                coll.transform.parent.GetComponent<Tower>().hitSize *= 1.5f;
                isUpActive = false;
            }
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tower")
        {
            other.transform.parent.GetComponent<Tower>().hitSize *= 1.5f;
        }
    }

}
