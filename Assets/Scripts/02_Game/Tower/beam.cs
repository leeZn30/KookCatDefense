using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam : MonoBehaviour
{

    public Vector3 start;
    public Vector3 target;

    public float attackDmg;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 dir = (target - start).normalized;
        float angle = Vector2.SignedAngle(Vector2.down, dir);
        Quaternion qut = new Quaternion();
        qut.eulerAngles = new Vector3(0, 0, angle);
        gameObject.transform.rotation = qut;
        Vector3 scale = transform.localScale;
        scale.y = (target - start).magnitude;
        transform.localScale = scale;
        gameObject.transform.position += dir * scale.y * 0.5f; // °Å¸®
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().AddAffection(attackDmg);
        }
    }
}
