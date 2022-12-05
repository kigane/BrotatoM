using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTest
{
    public string name;
    public int age;

    public int GetAge()
    {
        return age * 2;
    }

    public void LogName()
    {
        Debug.Log(name);
    }

    public static void Hello(string msg)
    {
        Debug.Log(msg);
    }
}

public class ReflectionTest : MonoBehaviour
{
    void Start()
    {
        Type t = Type.GetType(nameof(MyTest));
        var instance = Activator.CreateInstance(t);
        FieldInfo[] fields = t.GetFields();
        FieldInfo ageInfo = t.GetField("age");
        ageInfo.SetValue(instance, 22);
        Debug.Log((instance as MyTest).age);

        MethodInfo mInfo = t.GetMethod("GetAge");
        object ret = mInfo.Invoke(instance, null);
        Debug.Log((int)ret);
    }
}
