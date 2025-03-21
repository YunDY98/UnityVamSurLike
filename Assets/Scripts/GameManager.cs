using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 20f;

    [Header("# PlayerInfo")]
    [SerializeField]
    int _maxHealth;
    [SerializeField]
    int _health;
    [SerializeField]
    int _level;
    [SerializeField]
    int _kill;
    [SerializeField]
    int _exp;
    public int[] nextExp = {3,5,10,100,150,210,280,500,600,1000,2000};
    public int MaxHealth
    {
        get { return _maxHealth; }
    }

    public int Health
    {
        get { return _health; }
        set { _health = value; }

    }

    public int Exp
    {
        get 
        { 
           
            return _exp;
        }
        set { _exp = value;}


    }
    public int Kill
    {
        get { return _kill;}    
        set { _kill = value;}
    }
    public int Level
    {
        get { return _level;}  
        set { _level = value;}

    }


    [Header("# GameObject")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;

    void Awake()
    {
        instance = this;
        

        
    }
    void Start()
    {   
        Health = MaxHealth;
        //임시 스크립트 (첫번째 캐릭터 선택)
        uiLevelUp.Select(0);
        
        
    }
    void Update()
    {
        if(!isLive) return;
        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
           gameTime = maxGameTime;
        }
       
        
    }

    public void GetExp()
    {
        Exp++;
        if(Exp == nextExp[Mathf.Min(Level,nextExp.Length-1)])
        {
            Level++;
            Exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
        
    }
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1f;
    }

}
