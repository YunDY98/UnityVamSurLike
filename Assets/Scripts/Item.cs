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

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        textLevel = texts[0];

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

    

    }
  


    void LateUpdate()
    {
        textLevel.text = "Lv." + (level + 1);
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
