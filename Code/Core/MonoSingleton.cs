using System;
using System.IO;using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();
                if (_instance == null)
                {
                    Debug.Log("No manager");
                }
            }
            return _instance;
        }
        
    }

    private void Awake()
    {
        if(_instance == null) 
            _instance = this as T;
    }
}
