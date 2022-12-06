using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrotatoM;

public class SimpleTest : MonoBehaviour
{
    void Start()
    {
        Icon icon = new();
        AttrRow[] ar = new AttrRow[4];
        string[] s = new string[] { "hello", "world" };
        int a = 8;
        Test(icon, ar, s, a);
        Log.Debug(s);
    }

    private void Test(params object[] objs)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            Log.Debug(objs[i].GetType().Name);
        }
        Log.Debug(objs);
    }
}
