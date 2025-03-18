using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 20f;

    [Header("# PlayerInfo")]
    [SerializeField]
    int _level;
    [SerializeField]
    int _kill;
    [SerializeField]
    int _exp;
    public int[] nextExp = {3,5,10,100,150,210,280,500,600,1000,2000};


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

    void Awake()
    {
        instance = this;

        
    }
    void Update()
    {
        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
           gameTime = maxGameTime;
        }
       
        
    }

    public void GetExp()
    {
        Exp++;
        if(Exp == nextExp[Level])
        {
            Level++;
            Exp = 0;
        }
    }

}
