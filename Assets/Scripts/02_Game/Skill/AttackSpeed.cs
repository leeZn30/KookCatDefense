using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayGameSFX(GameSFX.DuDung,0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return;
        }

        Enemy enemy = collision.GetComponent<Enemy>();

        enemy.AttackSpeed *= 0.5f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return;
        }
        Enemy enemy = collision.GetComponent<Enemy>();

        enemy.ResetAttackSpeed();
    }
}
