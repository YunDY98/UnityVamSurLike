using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    Player player;

    float _timer;

   
   

    // Update is called once per frame
    void Update()
    {
        
        print(damage);
        
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                _timer += Time.deltaTime;
                if(_timer > speed)
                {
                    _timer = 0;
                    Fire();
                }
                break;
        }

        //.. Text Code ..
        if(Input.GetButtonDown("Jump"))
        {
            LevelUp(5,1);
            print("?>");
        }
        
    }

    public void LevelUp(float damage,int count)
    {
        this.count += count; 
        this.damage += damage;

        if(id == 0)
            Batch();

        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
        
    }

    public void Init(ItemData data)
    {
        player = GameManager.instance.player;
       
        // Basic Set
        name = "Weapon" + data.itemID;
       
        transform.parent = player.transform;
       
          
     
       
   
        transform.localPosition = Vector3.zero;
        //Property Set
        id = data.itemID;
        damage = data.baseDamage;
        count = data.baseCount;

        for(int i =0;i< GameManager.instance.pool.prefabs.Length;++i)
        {
            if(data.projectile == GameManager.instance.pool.prefabs[i])
            {
                prefabId = i;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                speed = 0.3f;
                break;
        }
        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
    }

    void Batch()
    {
        for(int i = 0; i < count;++i)
        {
            Transform bullet;
            if(i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }
            

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);

           // bullet.position = transform.position + Quaternion.Euler(rotVec) * Vector3.up * 1.5f;

           
            bullet.Translate(bullet.up * 1.5f, Space.World);



            bullet.GetComponent<Bullet>().Init(damage,-1,Vector3.zero); // -1 is Infinity per
        }
    }

    void Fire()
    {
    
        if(!player.scanner.nearestTarget) 
            return;
        
      

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
       
        dir.Normalize();

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;

        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up,dir);
        bullet.GetComponent<Bullet>().Init(damage,count,dir);
       
    }



}
