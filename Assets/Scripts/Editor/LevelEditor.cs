using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Editor {
	[CustomEditor(typeof(Level))]
	public class LevelEditor : UnityEditor.Editor {
		private static readonly EventType[] SceneEvents = {
			EventType.MouseDown,
			EventType.MouseDrag
		};

		private LevelPlaceableMarker _brush;
		private Level _target;
		private bool _active;
		[UsedImplicitly]
		private int _currentLayer;

		[UsedImplicitly]
		private void OnEnable() {
			_target = (Level) target;
		}

		[UsedImplicitly]
		private void OnDrawGizmos() {
			if (!_active) {
				return;
			}
			var point = CastMousePositionOnPlane();
			if (point != null) {
				Gizmos.DrawCube(point.Value, Vector3.one * _target.TileSize);
			}
		}

		public override void OnInspectorGUI() {
			serializedObject.Update();
			base.OnInspectorGUI();
			EditorGUILayout.Space();
			_active = EditorGUILayout.Toggle("Enable Editing", _active);
			_brush = (LevelPlaceableMarker) EditorGUILayout.ObjectField("Brush", _brush, typeof(LevelPlaceableMarker), false);

			serializedObject.ApplyModifiedProperties();
		}

		[UsedImplicitly]
		private void OnSceneGUI() {
			var point = CastMousePositionOnPlane();
			if (point == null || !_active) {
				return;
			}
			SceneView.RepaintAll();
			Handles.color = Color.red;
			Handles.DrawWireCube(point.Value, Vector3.one * _target.TileSize);

			if (!SceneEvents.Contains(Event.current.type)) {
				return;
			}
			if (_brush == null) {
				Debug.LogWarning("Please assign a brush in the Level editor panel");
				return;
			}

			Event.current.Use();
			RemoveApproximatelyAt(point.Value, _currentLayer);
			if (!Event.current.shift) {
				InstantiateBrush(point.Value, _currentLayer);
			}
		}

		private Vector3? CastMousePositionOnPlane() {
			var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
			RaycastHit hit;
			if (!Physics.Raycast(ray, out hit, 1 << _target.gameObject.layer)) {
				return null;
			}
			return SnapToGrid(hit.point);
		}

		private void RemoveApproximatelyAt(Vector3 point, int layer) {
			var threshold = _target.TileSize / 4;
			var existing = _target.transform.Cast<Transform>()
				.Where(t => Vector3.Distance(t.position, point) < threshold)
				.Where(t => t.GetComponent<LevelPlaceableMarker>().Layer == layer);
			foreach (var transform in existing) {
				Undo.DestroyObjectImmediate(transform.gameObject);
			}
		}

		private void InstantiateBrush(Vector3 point, int layer) {
			var copy = Instantiate(_brush.gameObject);
			copy.GetComponent<LevelPlaceableMarker>().Layer = layer;
			copy.transform.SetParent(_target.transform);
			copy.transform.position = point;
			Undo.RegisterCreatedObjectUndo(copy, "Level Brush");
		}

		private Vector3 SnapToGrid(Vector3 pos) => pos - new Vector3(
			pos.x % _target.TileSize,
			pos.y % _target.TileSize,
			pos.z % _target.TileSize
		) - new Vector3(
			_target.TileSize / 2,
			0,
			_target.TileSize / 2
		);
	}
}
