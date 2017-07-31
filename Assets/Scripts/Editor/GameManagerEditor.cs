using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Editor {
	[CustomEditor(typeof(GameManager))]
	public class GameManagerEditor : UnityEditor.Editor {
		[UsedImplicitly]
		private void OnEnable() {
			var manager = (GameManager) target;
			manager.AllPrefabs = AssetDatabase.FindAssets("t:Prefab", new[] {Utils.LevelPrefabsPath})
				.Select(AssetDatabase.GUIDToAssetPath)
				.Select(AssetDatabase.LoadAssetAtPath<GameObject>)
				.ToArray();
		}
	}
}
