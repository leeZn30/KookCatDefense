using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAffect : MonoBehaviour
{
    public int spaceCnt;
    public int waitTime;
    public int clickTime;
    public float oneTimeDmg;
    
    public Sprite[] imgs;
    private SpriteRenderer renderer;
    private int timeSec;
    bool isStart;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        timeSec = waitTime;
        renderer = GetComponent<SpriteRenderer>();
        isStart = false;
        StartCoroutine(waitAttack());
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine( createClickImg());
                spaceCnt++;
            }
        }
        
    }
    void Attack()
    {
        Debug.Log(spaceCnt);
        Debug.Log(spaceCnt * oneTimeDmg);
        List<Enemy>es= GameManager.Instance.Stage.enemies;
        for(int i=0; i<es.Count; i++)
        {
            es[i].AddAffection(spaceCnt * oneTimeDmg);
        }
        Destroy(gameObject);
    }
    IEnumerator createClickImg()
    {
        SoundManager.Instance.PlayGameSFX(GameSFX.Pang);
        Vector3 v = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
        GameObject pang=Instantiate(Resources.Load<GameObject>("Prefabs/Skill/Pang"), transform);

        pang.transform.localPosition = v;
        


        yield return new WaitForSeconds(1.0f);
        Destroy(pang);
    }
    IEnumerator waitAttack()
    {
        for(int i=0; i < timeSec; i++)
        {
            if (isStart == false) {
                SoundManager.Instance.PlayGameSFX(GameSFX.SkillCount);
                renderer.sprite = imgs[i]; 
            }
            yield return new WaitForSeconds(1.0f);
        }
        isStart = !isStart;
        if(isStart==false) Attack();
        else {
            timeSec = clickTime;
            renderer.sprite = imgs[2];
            StartCoroutine(waitAttack());
            
        }
    }
}
