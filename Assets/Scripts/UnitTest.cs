using UnityEngine;
using System.Collections;

public class UnitTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    //MessageManager .GetInstance .AddListener<int>(3,F);
     //   MessageManager .GetInstance .AddListener<int,string>(2,MyCallback);
     //   int a =  MessageManager .GetInstance .Dispatch<int>(3);
	    //print("get: " + a);
     //   MessageManager .GetInstance .Dispatch<int ,string>(2,55,"aaa");
	}
    
    int F()
    {
        Debug .Log(10);
        return 10;
    }
    private void MyCallback(int n, string s)
    {
        Debug.Log(string.Format("param1 {0}, parma2 {1}", n, s));
    }
}
