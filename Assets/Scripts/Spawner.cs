

using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    float timer;
    int level;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f),spawnData.Length - 1);
        if(timer > spawnData[level].spawnTime)
        {
            Spawn();
            timer = 0;
        }
       
        
    }

    void Spawn()
    {
        var enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1,spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}