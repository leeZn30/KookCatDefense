using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam : MonoBehaviour
{
    public Vector3 towerpos;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 dir = (towerpos - transform.position).normalized;
        float angle = Vector2.SignedAngle(Vector2.down, dir);
        Quaternion qut = new Quaternion();
        qut.eulerAngles = new Vector3(0, 0, angle);
        transform.rotation = qut;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
