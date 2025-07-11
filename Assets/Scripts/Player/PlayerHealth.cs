using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100; //players health
    public int maxHealth = 100;
    public Slider HealthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashspeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, .1f);

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        health = maxHealth;

    }
    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashspeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        health -= amount;
        HealthSlider.value = health;
        playerAudio.Play();
        if (health <= 0 && !isDead) 
        {
            Death();

        }
          
    }

    private void Death()
    {
        isDead = true;
        playerShooting.DisableEffects();
        anim.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play();
        playerMovement.enabled = false;
        playerShooting.enabled = false;

        
    }
    public void RestartLevel ()
    {
        Debug.Log("Restarting level...");
    }
}
