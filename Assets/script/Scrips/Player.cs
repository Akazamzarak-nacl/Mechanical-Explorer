using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    // public ParticleSystem dust;
    public GameObject pauseMenu;
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
            Reset();
            Debug.Log("reinicio");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
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
            MapDamage();
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
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Jump()
    {

        //animator.Play("robot_jump");

        animator.SetBool("isjump", true);
        if (rb.velocity.y == 0)
        {
            rb.AddForce(Vector2.up * force);
            animator.SetBool("isjump", false);

        }

        else if (rb.velocity.y != 0 && !doubleJump)
            DoubleJump();
        //animator.SetBool("isjump", false);
        else if (rb.velocity.y == 0 && doubleJump)
            animator.SetBool("isjump", false);
    }

    private void DoubleJump()
    {
        rb.AddForce(Vector2.up * (force / 1f));
        //rb.AddForce(Vector2.up * force);
        doubleJump = true;
        animator.SetBool("isjump", true);

    }

    public void Death()
    {
        Instantiate(deathSound, transform.position, Quaternion.identity);
        Debug.Log("sile");
        deathMenu.ToggleEndMenu();

    }

    public void Pause()
    {
        pause = !pause;
        pauseMenu.SetActive(pause);
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
