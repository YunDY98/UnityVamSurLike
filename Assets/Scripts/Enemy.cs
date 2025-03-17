using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animatorCon;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;

    }

    void FixedUpdate()
    {
        if(!isLive) return;

        Vector2 dirVec = target.position - rigid.position; // 타켓 위치 - 나의 위치 = 방향 
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        
        rigid.linearVelocity = Vector2.zero;

        
    }

    void LateUpdate()
    {
        if(!isLive) return;
        spriteRenderer.flipX = target.position.x < rigid.position.x;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animatorCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
        

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Bullet")) return;

        health -= collision.GetComponent<Bullet>().damage;

        if(health < 0)
        {
            Dead();
        }
        
    }

    void Dead()
    {
        gameObject.SetActive(false);

    }

}
