using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TurretBlueprint[] turrets;
    BuildManager buildManager;
    public static bool isClicked = true;
    public Color selectedColor;
    public Image tempImage;
    
    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
    }
    
    public void SelectStandardTurret(Image buttonImg)
    {
        if(turrets[1].isSelected)
        {
            turrets[1].isSelected = false;
            tempImage.sprite = turrets[1].unselectedUI;
            UnSelectTurret();
        }
    
        tempImage = buttonImg;
        
        if(!turrets[0].isSelected)
        {
            buttonImg.color = selectedColor;
            Debug.Log("Standard Turret Selected");
            buildManager.SelectTurretToBuild(turrets[0]);
            turrets[0].isSelected = true;
        }
        else if(turrets[0].isSelected)
        {
            buttonImg.color = Color.white;
            UnSelectTurret();
            Debug.Log("Deselect Standar Turret");
            turrets[0].isSelected = false;
        }
    }

    public void SelectAoETurret(Image buttonImg)
    {   
        if(turrets[0].isSelected)
        {
            turrets[0].isSelected = false;
            tempImage.color = Color.white;
            tempImage.sprite = turrets[0].unselectedUI;
            UnSelectTurret();
        }
        
        tempImage = buttonImg;

        if(!turrets[1].isSelected)
        {
            buttonImg.sprite = turrets[1].selectedUI;
            // buttonImg.color = selectedColor;
            Debug.Log("AoE Turret Selected");
            buildManager.SelectTurretToBuild(turrets[1]);
            turrets[1].isSelected = true;
        }
        else if(turrets[1].isSelected)
        {
            buttonImg.sprite = turrets[1].unselectedUI;
            // buttonImg.color = Color.white;
            Debug.Log("Deselect AoE Turret");
            UnSelectTurret();
            turrets[1].isSelected = false;
        }
    }

    public void UnSelectTurret()
    {
        Debug.Log("Unselect Successful !");
        buildManager.SelectTurretToBuild(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
