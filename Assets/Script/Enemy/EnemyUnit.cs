using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    public int hp = 20;

    public void TakeDamage(int dmg)
    {
        hp -= dmg;

        Debug.Log("Enemy HP = " + hp);

        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
