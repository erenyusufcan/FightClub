using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2 : MonoBehaviour
{
    public LevelManager LevelManager;
    public Player1 Player1;

    public GameObject[] prefabs;
    public Animator animator;
    public float throw_value ;
    private bool isClick;
    private float timer;
    public GameObject turn;
    public bool gameover;
    public Plane plane;

    [Header("UI")]
    public UIManager uýManager;
    public Image backgroundImg;
    public Image healthImg;
    public float maxHealth;
    float currentHealth;
    public float damage = 10;

    [Header("Power Bar")]
    public ClassicProgressBar powerBarPrefab;   
    public Transform powerBarSpawnPoint;       
    public float chargeSpeed = 1f;              
    public float maxForceCap = 999f; 


    private ClassicProgressBar spawnedBar;      
    private float charge01;                   


    public void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
    }
    public void OnMouseDown()
    {
        if (!LevelManager.player2throw) return;

        timer = 0f;
        isClick = true;
        animator.SetTrigger("ThrowIdle");

        
        charge01 = 0f;

        if (spawnedBar != null)
            Destroy(spawnedBar.gameObject);

        spawnedBar = Instantiate(powerBarPrefab, powerBarSpawnPoint);
        spawnedBar.SetFill(0f);

        
        RectTransform rt = spawnedBar.GetComponent<RectTransform>();
        rt.anchoredPosition = Vector2.zero; 
        rt.localScale = Vector3.one;        

       
    }
    public void OnMouseDrag()
    {
        if (!LevelManager.player2throw) return;

        if (isClick)
        {
            timer += Time.deltaTime;

            float maxHoldTime = 1.75f; 
            timer = Mathf.Min(timer, maxHoldTime);
            charge01 = Mathf.Clamp01(timer / maxHoldTime);



            if (spawnedBar != null)
                spawnedBar.SetFill(charge01);
        }
    }

    public void OnMouseUp()
    {
        if (!LevelManager.player2throw) return;
        SoundManager.instance.ThrowEffect2();

        isClick = false;
        animator.SetTrigger("Throw");

        LevelManager.direction = false;
        LevelManager.showwind = false;
        LevelManager.windDirection = Random.Range(0, 2);
       
        if (spawnedBar != null)
        {
            Destroy(spawnedBar.gameObject);
            spawnedBar = null;
        }

        GameObject prefab = Instantiate(prefabs[Random.Range(0,3)], new Vector3(5.638f, -1.45f, 0f), Quaternion.identity);
        Rigidbody2D rb = prefab.GetComponent<Rigidbody2D>();

        float force = ((charge01 * throw_value) * 75 / 100) + (LevelManager.windForceP2 * 3);
        force = Mathf.Min(force, maxForceCap);

        rb.AddForce(new Vector2(-force, force * 2f), ForceMode2D.Impulse);
        rb.angularVelocity = 720f;
        turn.SetActive(false);
        LevelManager.player2throw = false;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Prefab"))
        {
            
            Destroy(collision.gameObject);
            LevelManager.player1turnend = true;
            LevelManager.player2turnend = false;
            currentHealth -= damage;
            SoundManager.instance.DamageEffect();
            plane.throwcount1 = 0;
            healthImg.fillAmount = (float)currentHealth / (float)maxHealth;

            if (currentHealth == 0)
            {
                SoundManager.instance.VictoryEfect();
                gameover = true;
                turn.SetActive(false);
                Player1.Victory1();
                animator.SetTrigger("Death");
                uýManager.EndGameFunc();
            }
            else
            {
                animator.SetTrigger("Damage");
            }
        }
    }
    public void Victory2()
    {
        animator.SetTrigger("Victory");
    }
    public void Update()
    {
        if (backgroundImg.fillAmount != healthImg.fillAmount)
        {
            backgroundImg.fillAmount = Mathf.Lerp(backgroundImg.fillAmount, healthImg.fillAmount, 0.04f);

        }

    }


}
