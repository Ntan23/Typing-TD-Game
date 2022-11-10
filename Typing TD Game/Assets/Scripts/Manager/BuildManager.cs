using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Debug.Log("More than one BuildManager in scene !");
            return;
        }   
    }

    private TurretBlueprint turretToBuild;
    public GameObject standardTurret;
    public GameObject buildEffect;

    // Start is called before the first frame update
    void Start()
    {
        // turretToBuild = standardTurret;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public GameObject GetTurretToBuild()
    // {
    //     return turretToBuild;
    // }

    // public void SetTurretToBuild(GameObject turret)
    // {
    //     turretToBuild = turret;
    // }

    public bool CanBuild
    {
        get 
        {
            return turretToBuild != null;
        }
    }

    public bool HasMoney
    {
        get 
        {
            return PlayerStats.money >= turretToBuild.cost;
        }
    }

    public void BuildTurret(TurretContainer container)
    {
        if(PlayerStats.money < turretToBuild.cost)
        {
            Debug.Log("Not Enough Money To Build That!");
            return;
        }

        PlayerStats.money -= turretToBuild.cost;
        MoneyUI.needUpdate = true;

        GameObject turret = (GameObject) Instantiate(turretToBuild.turret,container.GetBuildPosition(),Quaternion.identity);
    
        container.turret = turret;
        Debug.Log("Turret Build! Money left : "+PlayerStats.money);
    
        GameObject effect = (GameObject) Instantiate(buildEffect,container.GetBuildPosition(),Quaternion.identity);
        Destroy(effect,1.0f);
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}
