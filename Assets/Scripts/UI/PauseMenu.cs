using UnityEngine;
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
            item.maxHP.text = "\\ "+(int)currentHP.currentMaxHP;
            IPlayerCurrentWeapon weap = Interface.Find<IPlayerCurrentWeapon>(interfaceSupplier);
            if(weap != null)
            {
                int weapAtk = 0;
                switch (weap.weaponType) { 
                    case WeaponType.Wooden: weapAtk = 10; break;
                    case WeaponType.Bronze: weapAtk = 50; break;
                    case WeaponType.Silver: weapAtk = 500; break;
                    case WeaponType.Gold: weapAtk = 750; break;
                    case WeaponType.Epic: weapAtk = 2000; break;
                    default: break;
                }
                    item.atkText.text = ((int)GameInfo.PlayerAtk + weapAtk) + "";
            }
            item.defText.text = (GameInfo.PlayerDef)*100f+"";
        }
    }

}
