using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower6Passive : MonoBehaviour
{
    // Start is called before the first frame update
    public float viewRadius;
    public LayerMask targetMask;
    public List<Transform> Targets = new List<Transform>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindTargets();
        ActivePassive();
    }

    
    void FindTargets()
    {
        List<Transform> transforms = new List<Transform>();
        Vector3[] dirList = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
        for(int i=0; i<dirList.Length; i++)
        {
            RaycastHit hitinfo;
            Debug.DrawRay(transform.position, dirList[i] * viewRadius, Color.green);
            if (Physics.Raycast(transform.position, dirList[i], out hitinfo, viewRadius, targetMask))
            {
                transforms.Add(hitinfo.transform);
                Tower t = hitinfo.transform.parent.gameObject.GetComponent<Tower>();
                t.isBuffed = true;
                
            }
        }
        Targets = transforms;
    }

    void ActivePassive()
    {
        foreach(Transform target in Targets)
        {
            Tower t = target.transform.parent.gameObject.GetComponent<Tower>();
            t.hitSize = t.baseHitSize + 1f;
        }
    }

    private void OnDestroy()
    {
        foreach (Transform target in Targets)
        {
            Tower t = target.transform.parent.gameObject.GetComponent<Tower>();
            t.hitSize = t.baseHitSize;
            t.isBuffed = false;
        }

    }

}
