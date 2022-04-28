using System;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour
{
    public static T Instance { get; private set; }
    protected void CreateSingleton(T referenceToInstance, Action eventIfInstanceExists)
    {
        if (Instance != null)
            eventIfInstanceExists?.Invoke();
        Instance = referenceToInstance;
        DontDestroyOnLoad(gameObject);
    }
}
