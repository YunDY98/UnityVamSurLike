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
    private StringBuilder _levelSB = new();
    private StringBuilder _killSB = new();
    private StringBuilder _timeSB = new();



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
                float maxExp = GameManager.instance.nextExp[GameManager.instance.Level];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                UpdateText(_levelSB,"Lv. ","{0:F0}",GameManager.instance.Level);
                break;
            case InfoType.Kill:
                UpdateText(_killSB,"","{0:F0}",GameManager.instance.Kill);
                break;
            case InfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                UpdateText(_timeSB,"","{0:D2}",min);
                UpdateText(_timeSB," : ","{0:D2}",sec,false);
                break;
            case InfoType.Health:
                float curHealth = GameManager.instance.Health;
                float maxHealth = GameManager.instance.MaxHealth;
                mySlider.value = curHealth / maxHealth;
                break;
                
        }
    }

    void UpdateText<T>(StringBuilder sb,string text,string format,T data,bool clear = true)
    {
        if(clear)
        {
            sb.Clear();
        }
        
        sb.Append(text).AppendFormat(format, data);
        myText.text = sb.ToString();

    }



}
