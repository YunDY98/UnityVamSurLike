using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType
    {
        Exp,
        Level,
        Kill,
        Time,
        Health

    }
    public InfoType type;
    
    TextMeshProUGUI myText;
    Slider mySlider;



    void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch(type)
        {
            case InfoType.Exp:
                float curExp = GameManager.instance.Exp;
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.Level,GameManager.instance.nextExp.Length-1)];
                mySlider.value = curExp / maxExp;
                break;
                
            case InfoType.Level:
                myText.text = $"Lv. {GameManager.instance.Level}";
                break;

            case InfoType.Kill:
                myText.text = $"{GameManager.instance.Kill}";
                break;

            case InfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = $"{min} : {sec}";
                break;

            case InfoType.Health:
                float curHealth = GameManager.instance.Health;
                float maxHealth = GameManager.instance.MaxHealth;
                mySlider.value = curHealth / maxHealth;
                break;
                
        }
    }

 

}
