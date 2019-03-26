using UnityEngine;
using System.Collections;

public class Util {

    public static string ToString(int num)
    {
        if (num < Mathf.Pow(10, 3))
        {
            return num.ToString();
        }
        if (num < Mathf.Pow(10, 6))
        {
            return (num/Mathf.Pow(10, 3)).ToString("F1") + "K";
        }
        if (num < Mathf.Pow(10, 9))
        {
            return (num/Mathf.Pow(10, 6)).ToString("F1") + "M";
        }
        if (num < Mathf.Pow(10, 12))
        {
            return (num/Mathf.Pow(10, 9)).ToString("F1") + "B";
        }
        if (num < Mathf.Pow(10, 15))
        {
            return (num/Mathf.Pow(10, 12)).ToString("F1") + "T";
        }
        return "999T+";
    }
    public static string ToString(uint num)
    {
        if (num < Mathf.Pow(10, 3))
        {
            return num.ToString();
        }
        if (num < Mathf.Pow(10, 6))
        {
            return (num / Mathf.Pow(10, 3)).ToString("F1") + "K";
        }
        if (num < Mathf.Pow(10, 9))
        {
            return (num / Mathf.Pow(10, 6)).ToString("F1") + "M";
        }
        if (num < Mathf.Pow(10, 12))
        {
            return (num / Mathf.Pow(10, 9)).ToString("F1") + "B";
        }
        if (num < Mathf.Pow(10, 15))
        {
            return (num / Mathf.Pow(10, 12)).ToString("F1") + "T";
        }
        return "999T+";
    }
    public static string ToString(ulong num)
    {
        if (num < Mathf.Pow(10, 3))
        {
            return num.ToString();
        }
        if (num < Mathf.Pow(10, 6))
        {
            return (num / Mathf.Pow(10, 3)).ToString("F1") + "K";
        }
        if (num < Mathf.Pow(10, 9))
        {
            return (num / Mathf.Pow(10, 6)).ToString("F1") + "M";
        }
        if (num < Mathf.Pow(10, 12))
        {
            return (num / Mathf.Pow(10, 9)).ToString("F1") + "B";
        }
        if (num < Mathf.Pow(10, 15))
        {
            return (num / Mathf.Pow(10, 12)).ToString("F1") + "T";
        }
        return "999T+";
    }
    public static string ToString(long num)
    {
        if (num < Mathf.Pow(10, 3))
        {
            return num.ToString();
        }
        if (num < Mathf.Pow(10, 6))
        {
            return (num / Mathf.Pow(10, 3)).ToString("F1") + "K";
        }
        if (num < Mathf.Pow(10, 9))
        {
            return (num / Mathf.Pow(10, 6)).ToString("F1") + "M";
        }
        if (num < Mathf.Pow(10, 12))
        {
            return (num / Mathf.Pow(10, 9)).ToString("F1") + "B";
        }
        if (num < Mathf.Pow(10, 15))
        {
            return (num / Mathf.Pow(10, 12)).ToString("F1") + "T";
        }
        return "999T+";
    }
}
