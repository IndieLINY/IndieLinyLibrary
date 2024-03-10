using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IndieLINY.Singleton
{
    public interface ISingleton
    {
        public void Initialize();
        public void Release();
    }
    public abstract class MonoBehaviourSingleton : MonoBehaviour, ISingleton
    {
        public abstract void Initialize();
        public abstract void Release();
    }

    public interface IGeneralSingleton : ISingleton
    {
    }
}