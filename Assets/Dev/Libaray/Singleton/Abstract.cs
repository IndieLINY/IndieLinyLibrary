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

    public abstract class BaseMonoBehaviourSingleton : MonoBehaviour, IMonoBehaviourSingleton
    {
        public abstract void Initialize();
        public abstract void Release();
    }
    public abstract class MonoBehaviourSingleton<T> : BaseMonoBehaviourSingleton
        where T : class, IMonoBehaviourSingleton
    {
        public static T Instance { get; private set; }

        public sealed override void Initialize()
        {
            Instance = this as T;
            
            PostInitialize();
        }

        public abstract void PostInitialize();
    }

    public interface IMonoBehaviourSingleton : ISingleton
    {
        
    }
    public interface IGeneralSingleton : ISingleton
    {
    }
}