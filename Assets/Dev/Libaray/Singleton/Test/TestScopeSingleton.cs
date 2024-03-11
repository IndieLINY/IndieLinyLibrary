using System.Collections;
using System.Collections.Generic;
using IndieLINY.Singleton;
using UnityEngine;

[Singleton(ESingletonType.Scope)]
public class TestScopeSingleton : MonoBehaviourSingleton
{
    public int number;
    public override void Initialize()
    {
    }

    public override void Release()
    {
    }
}
