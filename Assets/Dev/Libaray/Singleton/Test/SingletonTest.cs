using System.Collections;
using System.Collections.Generic;
using IndieLINY.Singleton;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[Singleton]
public class TestMonoSingleton : MonoBehaviourSingleton
{
    private List<int> _list;

    public bool HasNumber(int value)
    {
        return _list.Contains(value);
    }

    public void SetNumber(int value)
    {
        if (HasNumber(value) == false)
        {
            _list.Add(value);
        }
    }

    public override void Initialize()
    {
        _list = new List<int>();
    }

    public override void Release()
    {
        _list.Clear();
    }
}

[Singleton]
public class TestGeneralSingleton : IGeneralSingleton
{
    private List<int> _list;

    public bool HasNumber(int value)
    {
        return _list.Contains(value);
    }

    public void SetNumber(int value)
    {
        if (HasNumber(value) == false)
        {
            _list.Add(value);
        }
    }

    public void Initialize()
    {
        _list = new List<int>();
    }

    public void Release()
    {
        _list.Clear();
    }
}

public class TestFailSingleton : IGeneralSingleton
{
    public void Initialize()
    {
    }

    public void Release()
    {
    }
}

public class SingletonTest
{
    [Test]
    public void TestMono()
    {
        Singleton.GetSingleton<TestMonoSingleton>().SetNumber(1);
        Singleton.GetSingleton<TestMonoSingleton>().SetNumber(2);

        var mgr = Singleton.GetSingleton<TestMonoSingleton>();

        Debug.Log(mgr.HasNumber(1) == true);
        Debug.Log(mgr.HasNumber(3) == false);
    }

    [Test]
    public void TestGeneral()
    {
        Singleton.GetSingleton<TestGeneralSingleton>().SetNumber(1);
        Singleton.GetSingleton<TestGeneralSingleton>().SetNumber(2);

        var mgr = Singleton.GetSingleton<TestGeneralSingleton>();
        
        Debug.Log(mgr.HasNumber(1) == true);
        Debug.Log(mgr.HasNumber(3) == false);
    }
    
    [Test]
    public void TestFail()
    {
        try
        {
            Singleton.GetSingleton<TestFailSingleton>();
        }
        catch
        {
            return;
        }
        
        Debug.Assert(false);
    }
}