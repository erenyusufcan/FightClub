using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public LevelManager LevelManager;
    public int throwcount1 = 0;
    public int throwcount2 = 0;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Prefab"))
        {
           Destroy(collision.gameObject);
           LevelManager.player1turnend = true;     
           LevelManager.player2turnend = false;
           throwcount1++;
            if (throwcount1 ==2)
            {
                SoundManager.instance.LaughEffect();
                throwcount1 = 0;
            }
        }

        if (collision.gameObject.CompareTag("Prefab2"))
        {
            Destroy(collision.gameObject);
            LevelManager.player2turnend = true;
            LevelManager.player1turnend = false;
            throwcount2++;
            if (throwcount2 ==2)
            {
                SoundManager.instance.LaughEffect();
                throwcount2= 0;
            }
        }
    }
}
