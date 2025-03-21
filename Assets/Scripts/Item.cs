using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;
    public Button button;

   
    Image icon;
    TextMeshProUGUI textLevel;
    TextMeshProUGUI textName;

    TextMeshProUGUI textDesc;


    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];

        textName.text = data.itemName;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

    

    }
  


    void OnEnable()
    {
        textLevel.text = "Lv." + (level);
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(data.itemDesc,data.damages[level] * 100,data.counts[level]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textDesc.text = string.Format(data.itemDesc,data.damages[level] * 100);
                break;
            default:
                textDesc.text = string.Format(data.itemDesc);
                break;


        }
       
    }

    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if(level == 0)
                {
                    GameObject newWeapon = new();
                    weapon = newWeapon.AddComponent<Weapon>();
                    
                    weapon.Init(data);
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;
                    nextDamage += data.baseCount * data.damages[level];
                    nextCount += data.counts[level];
                    weapon.LevelUp(nextDamage,nextCount);
                }
                level++;

                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if(level == 0)
                {
                    GameObject newGear = new();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                level++;
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.Health = GameManager.instance.Health;
                break;


        }
       

        if(level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
            
        

    }

}
