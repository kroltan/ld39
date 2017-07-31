using UnityEngine;

namespace UI {
    public class ClipboardLevelSaver : MonoBehaviour {
        public void Save() {
            GUIUtility.systemCopyBuffer = Level.Instance.SaveCurrent();
        }

        public void Load() {
            Level.Instance.LoadLevel(GUIUtility.systemCopyBuffer);
        }
    }
}
