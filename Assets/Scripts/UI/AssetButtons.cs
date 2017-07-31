using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI {
    [RequireComponent(typeof(RectTransform))]
    public class AssetButtons : MonoBehaviour {
        [Serializable]
        public class AssetButtonClickEvent : UnityEvent<GameObject> { }

        public RectTransform ButtonTemplate;

        public AssetButtonClickEvent OnButtonClicked;

        [UsedImplicitly]
        private void Start() {
            foreach (var prefab in GameManager.Instance.AllPrefabs) {
                CreatePrefabButton(prefab);
            }
        }

        private void CreatePrefabButton(GameObject template) {
            var button = Instantiate(ButtonTemplate, transform);
            button.Find("Text").GetComponent<Text>().text = template.name;
            button.GetComponent<Button>().onClick.AddListener(() => {
                OnButtonClicked.Invoke(template);
            });
        }
    }
}
