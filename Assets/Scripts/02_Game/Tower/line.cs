using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
    public Vector3 start;
    public Vector3 target;

    LineRenderer lr;

    BoxCollider2D coll;

    public float LineWidth; // use the same as you set in the line renderer.

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, start);
        lr.SetPosition(1, target);

        LineWidth = lr.startWidth;

        coll = gameObject.AddComponent<BoxCollider2D>();

        coll.size = new Vector2(LineWidth / 2, 3);
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
