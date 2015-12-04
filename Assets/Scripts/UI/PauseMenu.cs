using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{    
    public GameObject prefab;
    
    void OnEnable ()
    {
        UpdateItem();
    }

    void UpdateItem()
    {
        Player player = FindObjectOfType<Player>();

        if (player != null && prefab != null)
        { 
            player.SaveAttributes();
            ICurrentHP currentHP = Interface.Find<ICurrentHP>(player.gameObject);
        
            SampleItem item = prefab.GetComponent<SampleItem>();
                      
            item.nameText.text = GameInfo.PlayerName+"";
            item.weaponText.text = GameInfo.CurrentWeapon.ToString();
            item.hpSlider.maxValue = (int)GameInfo.PlayerMaxHP;
            if (currentHP != null)
                item.hpSlider.value = (int)currentHP.currentHP;
            item.currentHP.text = currentHP.currentHP+"";
            item.maxHP.text = (int)GameInfo.PlayerMaxHP+"";
            item.atkText.text = (int)GameInfo.PlayerAtk+"";
            item.defText.text = (GameInfo.PlayerDef)*100+"%";
        }
    }

}
