using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    // public ParticleSystem dust;
    //public GameObject pauseMenu;
    public Animator animator;
    public Rigidbody2D rb;
    public float force;
    private bool pause, doubleJump;
    public int health = 4;
    public ScoreManager score;
    public DeadthCreen deathMenu;
    public AudioSource source;

    public GameObject deathSound;

    public bool secondChance;
    public static bool isDead = false;

    public PauseMenu P;

    public void Start()
    {
        CreateDust();
        Time.timeScale = 1;
        if (!source) source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
            //Debug.Log("reinicio");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (PauseMenu.GameIsPaused)
            {
                P.Resume();
            }
            else
            {
                P.Pause();
            }
        }

        Jump();
        Anima();

        if (doubleJump && rb.velocity.y == 0)
        {
            doubleJump = false;
            animator.SetBool("isjump", false);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<Weapon>().Shoot();
        }
        //如果机器人的x位置小于等于-12，则游戏结束
        if (transform.position.x <= -12)
        {
            if (!isDead)
            {
                MapDamage();
                isDead = true;
                Instantiate(deathSound, transform.position, Quaternion.identity);
            }
        }

    }

    public void SFX(AudioClip clip, float volume)
    {
        source.PlayOneShot(clip, volume);
    }

    public void OnTriggerEnter2D(Collider2D box)
    {
        if (box.CompareTag("Enemy"))
        {
            Damage();
        }

    }

    public void Damage()
    {
        // animator.Play("Damage");
        //Instantiate(hurtSound, transform.position, Quaternion.identity);

    }

    public void EnemyDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0 && !secondChance)
        {
            score.Save();

            Death();
            Instantiate(deathSound, transform.position, Quaternion.identity);
        }
        else if (health <= 0 && secondChance)
        {
            health = 4;
            // Efectitos lindos
        }
    }

    public void MapDamage()
    {
        score.Save();
        Death();
        Time.timeScale = 0f;
        Pause();
    }

    public void Restart()
    {
        isDead = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CreateDust();
        Time.timeScale = 1;
        if (!source) source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        PauseMenu.GameIsPaused = false;
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scene1");
    }

    public void Anima()
    {
        if (rb.velocity.y == 0)
        {
            animator.Play("robot_run");
        }
        if (rb.velocity.y > 0)
        {
            animator.Play("robot_jump");
        }
    }

    public void Jump()
    {

        //animator.Play("robot_jump");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rb.velocity.y == 0)
            {
                rb.AddForce(Vector2.up * force);


            }

            else if (rb.velocity.y != 0 && !doubleJump)
                DoubleJump();

        }
    }

    private void DoubleJump()
    {
        rb.AddForce(Vector2.up * (force / 1f));
        //rb.AddForce(Vector2.up * force);
        doubleJump = true;


    }

    public void Death()
    {
        Debug.Log("sile");
        deathMenu.ToggleEndMenu();
    }

    public void Pause()
    {
        pause = !pause;
        //pauseMenu.SetActive(pause);
        score.scoreIncreasing = !pause;
        if (pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    void CreateDust()
    {
        //dust.Play();
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetInt("highscore", 0);
    }
}
