using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
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

    public static class SingletonFactory
    {
        internal static T CreateMonoBehaviour<T>() where T : MonoBehaviourSingleton
            => CreateMonoBehaviour(typeof(T)) as T;
        internal static MonoBehaviourSingleton CreateMonoBehaviour(Type type)
        {
            var gameObject = new GameObject(type.Name);
            MonoBehaviourSingleton singleton = gameObject.AddComponent(type) as MonoBehaviourSingleton;
            
            Debug.Assert(singleton != null);
            return singleton;
        }
    }

    [System.AttributeUsage(AttributeTargets.Class)]
    public class FirstInitializeSingletonAttribute : Attribute
    {
        
    }

    public class Singleton : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        static void BeforeSplashScreen()
        {
            Type baseClassType = typeof(MonoBehaviourSingleton);

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                Type[] types = assembly.GetTypes()
                    .Where(type => type.GetCustomAttributes(typeof(FirstInitializeSingletonAttribute), true).Length > 0)
                    .Where(type => type.IsClass)
                    .ToArray();
            
                foreach (var type in types)
                {
                    MonoBehaviourSingleton singleton = SingletonFactory.CreateMonoBehaviour(type);
                    Debug.Assert(singleton != null);
                
                    singleton.Initialize();
                    _singletonList.Add(singleton);
                }
            }


            var obj = new GameObject("__Singleton__").AddComponent<Singleton>();
            DontDestroyOnLoad(obj);
        }

        private void OnApplicationQuit()
        {
            foreach (var singleton in _singletonList)
            {
                singleton.Release();
            }
            
            _singletonList.Clear();
        }

        private static List<ISingleton> _singletonList = new(5);
        
        private static T TryGetSingleton<T>() where T : class, ISingleton
        {
            foreach (ISingleton obj in _singletonList)
            {
                if (obj is T singleton)
                {
                    return singleton;
                }
            }

            return null;
        }

        public static T GetSingleton<T>() where T : MonoBehaviourSingleton
        {
            T singleton = TryGetSingleton<T>();

            if (object.ReferenceEquals(singleton, null))
            {
                singleton = SingletonFactory.CreateMonoBehaviour<T>();
                singleton.Initialize();
                _singletonList.Add(singleton);
            }

            return singleton;
        }
    }
}