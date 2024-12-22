using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100; 
    public int currentHealth; 

    public GameObject deathEffect; 
    public HealthBar healthBar; 

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if (data != null) 
        {
            currentHealth = data.PlayerHealth; 

            Vector3 position = new Vector3(
                data.position[0],
                data.position[1],
                data.position[2]
            );
            transform.position = position;

            healthBar.SetHealth(currentHealth);
        }
        else
        {
            Debug.LogWarning("Нет сохраненных данных для загрузки!");
        }
    }

    void Start()
    {
        if (TempPlayerData.PlayerHealth > 0)
        {
            currentHealth = TempPlayerData.PlayerHealth;
            transform.position = TempPlayerData.Position;
            healthBar.SetMaxHealth(health);
            healthBar.SetHealth(currentHealth);
        }
        else
        {
            currentHealth = health; 
            healthBar.SetMaxHealth(health); 
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; 

        healthBar.SetHealth(currentHealth); 

        StartCoroutine(DamageAnimation()); 

        if (currentHealth <= 0)
        {
            Die(); 
        }
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    IEnumerator DamageAnimation()
    {
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < 3; i++)
        {
            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 0;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);

            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 1;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);
        }
    }
}
