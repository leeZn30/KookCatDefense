using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
    public Vector3 start;
    public Vector3 target;

    LineRenderer lr;
    EdgeCollider2D coll;

    CapsuleCollider2D capsule;
    public float LineWidth; // use the same as you set in the line renderer.

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, start);
        lr.SetPosition(1, target);

        LineWidth = lr.startWidth;

        capsule = gameObject.AddComponent<CapsuleCollider2D>();
        capsule.size = new Vector2(LineWidth/2, (target - start).y);
        capsule.transform.position = start + (target - start) / 2;

    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("Enemy!");
        }
    }
}
