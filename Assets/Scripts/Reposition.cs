using Unity.Mathematics;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector2 playerPos = GameManager.instance.player.transform.position;
        Vector2 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);
        
        Vector2 playerDir = GameManager.instance.player.inputVec;

        

        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        dirX = dirX > 0 ? 1 : -1;
        dirY = dirY > 0 ? 1 : -1;

        
       

        switch (transform.tag)
        {
            case "Ground":

                if (Mathf.Abs(diffX - diffY) <= 1f) {
                    transform.Translate(Vector2.up * dirY * 40);
                    transform.Translate(Vector2.right * dirX * 40);
                }


                else if (diffX > diffY)
                {
                    transform.Translate(Vector2.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector2.up * dirY * 40);
                }

               
                break;
            case "Enemy":


                break;
        }

    }

}