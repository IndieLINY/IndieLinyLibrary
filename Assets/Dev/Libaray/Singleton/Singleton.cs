using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IndieLINY.Singleton
{
    [System.AttributeUsage(AttributeTargets.Class)]
    public class SingletonAttribute : Attribute
    {
    }

    public class Singleton : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        static void BeforeSplashScreen()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            
            foreach (var assembly in assemblies)
            {
                Type[] types = assembly.GetTypes()
                    .Where(type => type.GetCustomAttributes(typeof(SingletonAttribute), true).Length > 0)
                    .Where(type => type.IsClass)
                    .ToArray();
            
                foreach (var type in types)
                {
                    var singleton = SingletonFactory.CreateSingleton(type);
                    
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

        public static T GetSingleton<T>() where T : class, ISingleton
        {
            foreach (ISingleton obj in _singletonList)
            {
                if (obj is T singleton)
                {
                    return singleton;
                }
            }

            throw new NullReferenceException($"failed to get ({typeof(T).Name}) singleton");
        }
    }
}