using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    SpriteRenderer player;

    Vector3 rightPos = new Vector3(0.35f,-0.15f,0);
    Vector3 rightPosResverse = new Vector3(-0.15f,-0.15f,0);

    Quaternion leftRot = Quaternion.Euler(0,0,-35);
    Quaternion leftRotReverse = Quaternion.Euler(0,0,-135);


    void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
        
    }

    void LateUpdate()
    {
        bool _isReverse = player.flipX;

        if(isLeft)
        {
            //근접 무기 
            transform.localRotation = _isReverse ? leftRotReverse : leftRot;
            spriter.flipY = _isReverse;
            spriter.sortingOrder = _isReverse ? 4 : 6;
        }
        else
        {
            //원거리 무기 
            transform.localPosition = _isReverse ? rightPosResverse : rightPos;
            spriter.flipX = _isReverse;
            spriter.sortingOrder = _isReverse ? 6 : 4;
        }
    }

}
