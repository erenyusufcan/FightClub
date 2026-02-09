using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Scripts")]
    public GameObject[] gameScripts;
    public GameObject menuScript;
    public bool startGame;

    [Header("Wind")]
    public Image leftwindImg;
    public Image rightwindImg;
    public int windDirection;
    public bool direction = false;
    public bool showwind;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public float randomvalue;
    public float windForceP1;
    public float windForceP2;

    public GameObject mainBackground;
    public Sprite[] backgrounds;
    public UIManager manager;
    public SpriteRenderer sr;

    public bool player2throw;
    public Player1 player1;
    public Player2 player2;
    public bool player1turnend;
    public bool player2turnend; 


    public void Awake()
    {
         sr = mainBackground.GetComponent<SpriteRenderer>();
         sr.sprite=backgrounds[0];

        windDirection = Random.Range(0, 2);
        showwind = true;
        player2.turn.SetActive(false);
        player2throw = false;
    }
    public void Update()
    {
        sr.sprite = backgrounds[UIManager.mapindex];
        startGame = UIManager.startGame;
        if (!startGame)
        {
            foreach (var s in gameScripts)
            {
                s.SetActive(false);
            }
            menuScript.SetActive(true);

        }
        else
        {
            foreach (var s in gameScripts)
            {
                s.SetActive(true);
            }
            menuScript.SetActive(false);
        }

        if (!player2throw && player2turnend && !player1.gameover)
        {
            showwind =true;
          player1.turn.SetActive(true);
          player2.turn.SetActive(false);
        }
      
        else if(player2throw && player1turnend && !player2.gameover)
        {
            showwind = true;
            player1.turn.SetActive(false);
            player2.turn.SetActive(true);
        }
        if(windDirection == 0)
        {
            
            if (!direction && showwind)
            {
                    rightArrow.SetActive(false);
                    leftArrow.SetActive(true);
                    rightwindImg.fillAmount = 0;
                    int intrandomvalue = Random.Range(2,10);
                    randomvalue = intrandomvalue / 10f;
                    leftwindImg.fillAmount = randomvalue;
                    showwind =false;
                    direction = true;
                windForceP1 = -randomvalue;
                windForceP2 = randomvalue;
                
               
            }
           
          
        }
        else if(windDirection == 1)
        {
            if (!direction && showwind)
            {
                    rightArrow.SetActive(true);
                    leftArrow.SetActive(false);
                    leftwindImg.fillAmount = 0;
                    int intrandomvalue = Random.Range(2, 10);
                    randomvalue = intrandomvalue / 10f;
                    rightwindImg.fillAmount = randomvalue;
                    showwind =false;
                    direction = true;
                windForceP1 = randomvalue;
                windForceP2 = -randomvalue;

            }
           
           
        }
       
    }



}
