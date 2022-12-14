using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class WaveSpawner : MonoBehaviour
{
    public static int enemiesAlive = 0;
    public Wave[] waves;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5.0f;
    public float countdown = 2.0f;
    public static int waveIndex = 0;
    public TextMeshProUGUI waveCountdownText;
    public TextMeshProUGUI waveIndexText;
    public bool nextwave = false;
    
    GameManager gm;
    
    void Start()
    {
        gm = GameManager.instance;
        waveIndexText.text = "Wave " + waveIndex.ToString() + "/" + waves.Length.ToString();
    }

    private void Update()
    {
        if(enemiesAlive > 0)
        {   
            return;
        }

        if(enemiesAlive == 0 && nextwave == false)
        {
            ConvertManaToMoney();
            nextwave= true;
        }

        if(countdown <= 0 && nextwave)
        {   
            // ConvertManaToMoney();
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            nextwave = false;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown,0f,Mathf.Infinity);

        waveCountdownText.text = countdown.ToString("00.00");
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];

        enemiesAlive = wave.enemyCount;

        for(int i = 0;i < wave.enemyCount;i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1.0f/wave.rate);
        }

        waveIndex++;
       
        waveIndexText.text = "Wave " + waveIndex.ToString() + "/" + waves.Length.ToString();

        if(waveIndex == waves.Length)
        {
            this.enabled = false;
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy,spawnPoint.position,spawnPoint.rotation);
    }

    void ConvertManaToMoney()
    {
        PlayerStats.money += Mathf.Floor(gm.ManaCount*0.1f);
        MoneyUI.needUpdate = true;
        gm.ManaCount = 0;
        gm.UpdateManaBar();
    }
}
