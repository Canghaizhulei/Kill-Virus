using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    float leftBorder;
    float rightBorder;
    float topBorder;
    float downBorder;
    float speed = 5;
    private float width;
    private float height;
    private Vector3 moveDir;
    private float radius;
    private bool isEnterZone;
    private Vector3 bornPosition;
    void Start()
    {
        //世界坐标的右上角  因为视口坐标右上角是1,1,点
        Vector3 cornerPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f,
            Mathf.Abs(-Camera.main.transform.position.z)));
        //世界坐标左边界
        leftBorder = Camera.main.transform.position.x - (cornerPos.x - Camera.main.transform.position.x);
        //世界坐标右边界
        rightBorder = cornerPos.x;
        //世界坐标上边界
        topBorder = cornerPos.y;
        //世界坐标下边界
        downBorder = Camera.main.transform.position.y - (cornerPos.y - Camera.main.transform.position.y);
        width = rightBorder - leftBorder;
        height = topBorder - downBorder;
        radius = transform.localScale.x / 2;
        InitPositon();
    }

    public void InitPositon()
    {
        bornPosition = new Vector3(Random.Range(leftBorder + 2 * radius, rightBorder - 2 * radius), topBorder + height/3+ radius, 0);
        transform.position = bornPosition;
        moveDir = new Vector3(Random.Range(-1, 1f), -Random.Range(0.3f, 1f), 0);
    }

    void Update()
    {
        if (!isEnterZone && transform.localPosition.y + radius <= topBorder)
        {
            isEnterZone = true;
        }
        transform.Translate(moveDir * Time.deltaTime * speed);
        //上 如果物体的Y轴和屏幕Y轴相等那么就是证明到达边界
        if (transform.localPosition.y + radius >= topBorder && isEnterZone)
        {
            transform.position = new Vector3(transform.localPosition.x, topBorder - radius, 0);
            //计算物体到达边界之后又按原来角度弹回去
            moveDir = Vector3.Reflect(moveDir, Vector3.down);
        }
        //下
        if (transform.localPosition.y + radius *2<= downBorder)
        {
            //位置重置
            InitPositon();
            isEnterZone = false;
        }
        //左
        if (transform.localPosition.x - radius <= leftBorder)
        {
            transform.position = new Vector3(leftBorder + radius, transform.localPosition.y, 0);
            moveDir = Vector3.Reflect(moveDir, Vector3.right);
        }
        //右
        if (transform.localPosition.x + radius >= rightBorder)
        {
            transform.position = new Vector3(rightBorder - radius, transform.localPosition.y, 0);
            moveDir = Vector3.Reflect(moveDir, Vector3.left);
        }
    }
}