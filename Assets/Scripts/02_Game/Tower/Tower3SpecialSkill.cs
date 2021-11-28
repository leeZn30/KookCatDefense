using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower3SpecialSkill : MonoBehaviour
{
    private List<GameObject> collEnemys = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        transform.parent.GetComponent<Tower>().skillGague = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //attack();
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.Instance.PlayGameSFX(GameSFX.CatnipSkill, 0.1f);
        if (collision.tag == "Enemy")
            collision.transform.GetComponent<Enemy>().SpeedDownAndReset(0, 1.5f);
    }
    

    private void OnDestroy()
    {
        if (transform.parent.gameObject.GetComponent<Tower>().isActiveAndEnabled)
            transform.parent.GetComponent<Tower>().Startco();
    }


}
