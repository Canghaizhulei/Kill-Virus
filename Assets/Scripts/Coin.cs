using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{

    private ulong addCoin = 100;
    public static  Vector3 Target = Vector3.one;
    private bool isMoving;


    void Update()
    {
        if (!isMoving) return;
        if ((transform.position - Target).sqrMagnitude > 0.1)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, Time .deltaTime*300);
        }
        else
        {
            isMoving = false;
            PlayerController.GetInstance.LevelData.GetCoin += addCoin;
            AudioManager.GetInstance.PlayAudio("EatCoin");
            ObjectPool.GetInstance().RecycleObj(gameObject);
        }
    }

    public void Init(Vector3 pos)
    {
        transform.position = pos;
        transform .localScale = Vector3.one;
        isMoving = false;
        Invoke("Move", 0.3f);
    }

    void Move()
    {
        isMoving = true;
    }
}
