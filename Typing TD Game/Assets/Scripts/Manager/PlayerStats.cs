using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static float money;
    public static float moneyBar;
    public float startMoney = 500;
    public static float health;
    public float startingHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        money = startMoney;
        health = startingHealth;
    }
}
