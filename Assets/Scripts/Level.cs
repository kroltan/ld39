using System;
using System.Collections.Generic;
using System.Linq;
using Builder;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

public class Level : SingletonBehaviour<Level> {
    [Serializable]
    private class LevelData {
        [Serializable]
        public class Entry {
            public Vector3 Position;
            public string Asset;
        }
        [Serializable]
        public class Layer {
            public Entry[] Data;
        }

        public float Duration;
        public Layer[] Layers;
    }

    public float TileSize;

    public TextAsset[] Levels;

    private int _levelIndex;
    public int LevelIndex {
        get { return _levelIndex; }
        set {
            _levelIndex = value % Levels.Length;
            LoadLevel(Levels[_levelIndex].text);
        }
    }

    [UsedImplicitly]
    private void Start() {
        LoadLevel(Levels[_levelIndex].text);
    }

    public void LoadLevel(string definition) {
        var data = JsonUtility.FromJson<LevelData>(definition);

        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }

        var assets = data.Layers
            .SelectMany(l => l.Data)
            .Select(e => e.Asset)
            .Distinct()
            .ToDictionary(name => name, name => {
                var all = GameManager.Instance.AllPrefabs;
                var proto = all.First(go => go.name == name);
                return proto;
            });
        
        for (var layerIndex = 0; layerIndex < data.Layers.Length; layerIndex++) {
            var layer = data.Layers[layerIndex];
            foreach (var entry in layer.Data) {
                var proto = assets[entry.Asset];
                if (proto == null) {
                    continue;
                }
                InstantiatePrototype(proto, entry.Position, layerIndex);
            }
        }

        GameManager.Instance.LevelDurationSeconds.Value = data.Duration;
    }

    public GameObject InstantiatePrototype(GameObject prototype, Vector3 position, int layer) {
        var go = Instantiate(prototype);
        go.name = prototype.name;
        go.transform.SetParent(transform);
        go.transform.position = position;
        go.AddComponent<LayerMarker>().Layer = layer;

        var stack = GetExisting(position).Where(g => g != go);
        if (stack.Count() > 1) {
            foreach (var renderer in go.GetComponentsInChildren<SpriteRenderer>()) {
                renderer.sortingOrder += layer;
            }
        }
        return go;
    }

    public string SaveCurrent() {
        var dict = transform.Cast<Transform>()
            .GroupBy(t => t.GetComponent<LayerMarker>().Layer)
            .ToDictionary(g => g.Key, g => g.ToArray());
        var layers = new LevelData.Layer[dict.Keys.Count];
        foreach (var kvp in dict) {
            layers[kvp.Key] = new LevelData.Layer {
                Data = kvp.Value
                    .Select(t => new LevelData.Entry {
                        Asset = t.name,
                        Position = t.position
                    })
                    .ToArray()
            };
        }
        var data = new LevelData {
            Duration = GameManager.Instance.LevelDurationSeconds.Value,
            Layers = layers
        };
        return JsonUtility.ToJson(data);
    }

    public IEnumerable<GameObject> GetExisting(Vector3 worldPosition) => transform
        .Cast<Transform>()
        .Where(t => Vector3.Distance(t.position, worldPosition) < TileSize / 2)
        .Select(t => t.gameObject);
}