using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public string name;
    public string weapon;
    public int hp;
    public int attack;
    public int defense;
}

public class PauseMenu : MonoBehaviour {

    public GameObject sampleContent;
    public GameObject prefab;
    public Item item;

    public Transform contentPanel;

    void Start ()
    {
        item = new Item();
        UpdateItem();
        PopulateList();
    }

    void UpdateItem()
    {
        FindObjectOfType<Player>().SaveAttributes();

        item.name = GameInfo.PlayerName+"";
        item.weapon = GameInfo.CurrentWeapon.ToString();
        item.hp = (int)GameInfo.PlayerMaxHP;
        item.attack = (int)GameInfo.PlayerAtk;
        item.defense = (int)GameInfo.PlayerDef;


    }


    void PopulateList()
    {
        GameObject newItem = Instantiate(sampleContent) as GameObject;
        SampleItem sampleItem = newItem.GetComponent<SampleItem>();
        sampleItem.nameText.text = item.name;
        sampleItem.weaponText.text = item.weapon;
        sampleItem.hpText.text = item.hp + "";
        sampleItem.atkText.text = item.attack + "";
        sampleItem.defText.text = item.defense + "";
        newItem.transform.SetParent(contentPanel);

    }

}
