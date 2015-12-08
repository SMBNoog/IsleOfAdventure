﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{    
    public GameObject attributePanel;

    private GameObject interfaceSupplier;
    
    void OnEnable ()
    {
        UpdateItem();
    }

    void UpdateItem()
    {
        interfaceSupplier = FindObjectOfType<Player>().gameObject;

        if (interfaceSupplier != null && attributePanel != null)
        {
            Player player = interfaceSupplier.GetComponent<Player>();
            player.SaveAttributes(false);
           
        
            SampleItem item = attributePanel.GetComponent<SampleItem>();
                      
            item.nameText.text = GameInfo.PlayerName+"";
            item.weaponText.text = GameInfo.CurrentWeapon.ToString();
            item.hpSlider.maxValue = (int)GameInfo.PlayerMaxHP;
            ICurrentHP currentHP = Interface.Find<ICurrentHP>(interfaceSupplier);
            if (currentHP != null)
                item.hpSlider.value = (int)currentHP.currentHP;
            else
                Debug.Log("ICurrentHP couldn't be found.");
            item.currentHP.text = (int)currentHP.currentHP+"";
            item.maxHP.text = "\\ "+(int)GameInfo.PlayerMaxHP;
            item.atkText.text = (int)GameInfo.PlayerAtk+"";
            item.defText.text = (GameInfo.PlayerDef)*100+" %";
        }
    }

}
