using UnityEngine;

public class PlayerMove
{
    Vector3 offset;
    public Vector3 Target;
    // Use this for initialization
    private float radius;
    public void Init(Vector3 origin)
    {
        radius = PlayerController.GetInstance.CurrentPlayer.GetComponent<Transform>().localScale.x/2;
        Target = origin;
    }

   public void Update()
    {
#if UNITY_EDITOR
       if (Input.GetMouseButtonDown(0))
       {
                ComputeOffset(Input.mousePosition);

        }else if (Input.GetMouseButton(0))
        {
            OnDrag(Input.mousePosition);
        }
#endif
        if (Input.touchCount == 1)
        {

            if (Input.touches[0].phase == TouchPhase.Began)
            {
                ComputeOffset(Input.touches[0].position);
            }
            else if (Input.touches[0].phase == TouchPhase.Moved)
            {
                OnDrag(Input.touches[0].position);
            }
        }
    }

    private void OnDrag(Vector2 position)
    {
        var pos = Camera.main.ScreenToWorldPoint(position);
        Target = pos + offset;

        // limit
        //上
        if (Target.y + radius >= CamData .GetInstance.TopBorder )
        {
            Target.y = CamData.GetInstance.TopBorder - radius;
        }
        //下
        if (Target.y - radius  <= CamData.GetInstance.DownBorder)
        {
            Target.y = CamData.GetInstance.DownBorder + radius;
        }
        //左
        if (Target.x - radius <= CamData.GetInstance.LeftBorder)
        {
            Target.x = CamData.GetInstance.LeftBorder + radius;
        }
        //右
        if (Target.x + radius >= CamData.GetInstance.RightBorder)
        {
            Target.x = CamData.GetInstance.RightBorder - radius;
        }

    }
    
    private void ComputeOffset(Vector2 position)
    {
        var pos = Camera.main.ScreenToWorldPoint(position);
        offset = PlayerController.GetInstance.CurrentPlayer.GetComponent<Transform>().position - pos;
    }
}
