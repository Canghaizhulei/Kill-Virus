using UnityEngine;
using System.Collections;

public class CamData
{
    private static CamData instance;

    public static CamData GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new CamData();
            }
            return instance;
        }
    }

    public  float LeftBorder;
    public  float RightBorder;
    public  float TopBorder;
    public  float DownBorder;
    public  float Width;
    public  float Height;

    public void StartUp ()
    {
        Vector3 cornerPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f,
                Mathf.Abs(-Camera.main.transform.position.z)));
        //世界坐标左边界
        LeftBorder = Camera.main.transform.position.x - (cornerPos.x - Camera.main.transform.position.x);
        //世界坐标右边界
        RightBorder = cornerPos.x;
        //世界坐标上边界
        TopBorder = cornerPos.y;
        //世界坐标下边界
        DownBorder = Camera.main.transform.position.y - (cornerPos.y - Camera.main.transform.position.y);
        Width = RightBorder - LeftBorder;
        Height = TopBorder - DownBorder;
    }

    public bool IsVisible(Vector3 position, float radius)
    {
        if (position.y - radius >= TopBorder)
        {
            return false;
        }
        if (position.y + radius <= DownBorder)
        {
            return false;
        }
        if (position.x +radius <= LeftBorder)
        {
            return false;
        }
        if (position.x - radius >= RightBorder)
        {
            return false;
        }
        return true;
    }
}
