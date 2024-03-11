using System.Collections;
using System.Collections.Generic;
using IndieLINY.Singleton;
using UnityEngine;

[Singleton(ESingletonType.Scope)]
public class TestScopeSingleton : MonoBehaviourSingleton<TestScopeSingleton>
{
    public int number;

    public override void Release()
    {
    }

    public override void PostInitialize()
    {
        
    }
}
