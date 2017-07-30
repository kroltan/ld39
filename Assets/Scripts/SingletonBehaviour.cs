using System;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T: SingletonBehaviour<T> {
    private static readonly Type Type = typeof(T);

    private static T _instance;
    public static T Instance {
        get {
            if (_instance != null) {
                return _instance;
            }

            var results = FindObjectsOfType<T>();
            if (results.Length >= 1) {
                if (results.Length > 1) {
                    Debug.LogWarning($"More than one instance of {Type.FullName} in scene! Using first result.");
                }
                _instance = results[0];
                return _instance;
            }

            var go = new GameObject($"__Singleton_{Type.FullName}");
            return go.AddComponent<T>();
        }
    }

    public bool KeepOnSceneChange = true;

    protected virtual void Awake() {
        if (_instance != null) {
            Destroy(_instance);
        }
        if (KeepOnSceneChange) {
            DontDestroyOnLoad(gameObject);
        }
        _instance = (T) this;
    }
}