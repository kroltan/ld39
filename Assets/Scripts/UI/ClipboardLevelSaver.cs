#if (!UNITY_EDITOR && UNITY_WEBGL)
using System.Runtime.InteropServices;
using JetBrains.Annotations;
#endif
using UnityEngine;

namespace UI {
    public class ClipboardLevelSaver : MonoBehaviour {
#if (!UNITY_EDITOR && UNITY_WEBGL)
        [DllImport("__Internal")]
        private static extern void SaveLevel(string content);

        [DllImport("__Internal")]
        private static extern void ListenLevelChanged(string objectName, string callbackName);

        private string _lastWebContent;

        [UsedImplicitly]
        private void Start() {
           ListenLevelChanged(gameObject.name, nameof(WebLevelChangeCallback));
        }

        private void WebLevelChangeCallback(string content) {
            _lastWebContent = content;
            Debug.Log($"CHANGE: {content}");
        }
#endif

        public void Save() {
#if (!UNITY_EDITOR && UNITY_WEBGL)
            SaveLevel(Level.Instance.SaveCurrent());
#else
            GUIUtility.systemCopyBuffer = Level.Instance.SaveCurrent();
#endif
        }

        public void Load() {
#if (!UNITY_EDITOR && UNITY_WEBGL)
            Debug.Log($"CSHARP: {_lastWebContent}");
            Level.Instance.LoadLevel(_lastWebContent);
#else
            Level.Instance.LoadLevel(GUIUtility.systemCopyBuffer);
#endif
        }
    }
}
