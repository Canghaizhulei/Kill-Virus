using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {

    private float MoveSpeed = 15;
    public ulong BulletPower = 5;//子弹的威力

     float radius;
    private Vector3  deltaOffset = Vector3.zero ;

    private int count =12;
    private int counter;
    public Sprite[] Sprites;
    void Start()
    {
        radius = transform.localScale.x/2;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * MoveSpeed);
        if (!CamData.GetInstance.IsVisible(transform.position, radius))
        {
            gameObject.name = "Bullet";
            ObjectPool .GetInstance() .RecycleObj(gameObject);
        }
        //if (Mathf.Abs(curOffset) < Mathf.Abs(offset*1.1f))
        //{
        //    curOffset += deltaOffset.x;
        //    transform.Translate(deltaOffset);
        //}


        counter++;
        if (counter <= count)
        {
            transform.Translate(deltaOffset);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
     
        if (other.CompareTag("Enemy") )
        {
            AudioManager .GetInstance .PlayHitEnemy();
            other.GetComponent<Enemy>().EneryDamage(BulletPower);
            ObjectPool .GetInstance() .RecycleObj(gameObject);
        }
    }

    public void Init(ulong power,Vector3 pos, float offset,int spriteIndex,Color addColor)
    {
        BulletPower = power;

        transform.position = pos;

        deltaOffset.x = offset/count;
        counter = 0;

        GetComponent<SpriteRenderer>().sprite = Sprites[spriteIndex];
        GetComponent<SpriteRenderer>().color = addColor;
    }
}
