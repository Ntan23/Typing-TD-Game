using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	public float startHealth = 100;
	public float startSpeed;
	private float health;
	private int reward;
	public GameObject deathEffect;

	[Header("Unity Stuff")]
	public Image healthBar;
	public GameObject healthImage;
	private bool isDead = false;
	NavMeshAgent navMeshAgent;
	AoETurret aoETurret;
	GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
		navMeshAgent = GetComponent<NavMeshAgent>();
		gameManager = GameManager.instance;
		aoETurret = FindObjectOfType<AoETurret>();
        health = startHealth;
		startSpeed = navMeshAgent.speed;
		healthBar.color = Color.green;
		
		if(aoETurret != null)
		{
			InvokeRepeating("UnSlow",0.0f,aoETurret.slowDuration);
		}
    }

	public void TakeDamage (float damage)
	{
		health -= damage;
		
		UpdateHealthBar();

		if(health <= 0 && !isDead) 
		{
			Debug.Log("Enemy Killed!");
			Die();
		}
	}

	void Die ()
	{
		isDead = true;

		PlayerStats.moneyBar++;
		gameManager.UpdateMoneyBar();

		GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
		
        Destroy(effect,3f);
		
		WaveSpawner.enemiesAlive--;

		Destroy(gameObject);
	}

	void UpdateHealthBar()
    {
        healthBar.fillAmount = health/startHealth;

        if(healthBar.fillAmount > 0.5f)
        {
            healthBar.color = Color.green;
        }
        else if(healthBar.fillAmount <= 0.5f && healthBar.fillAmount > 0.2f)
        {
            healthBar.color = Color.yellow;
        }
        else if(healthBar.fillAmount <= 0.2f)
        {
            healthBar.color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthImage.transform.rotation = Quaternion.Euler(40,0,0);
    }

	public void Slow(float slowSpeed)
	{
		navMeshAgent.speed = slowSpeed;
		navMeshAgent.acceleration = slowSpeed;
	}

	public void UnSlow()
	{
		if(navMeshAgent.speed != startSpeed || navMeshAgent.acceleration != startSpeed)
		{
			navMeshAgent.speed = startSpeed;
			navMeshAgent.acceleration = startSpeed;
		}
		else 
		{
			return;
		}
	}
}
