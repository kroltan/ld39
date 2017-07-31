using System.Linq;
using JetBrains.Annotations;
using UI;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Builder {
    [RequireComponent(typeof(Level))]
    public class LevelEditor : SingletonBehaviour<LevelEditor> {
        public AssetButtons Buttons;
        public Transform Cursor;
        public Transform[] ToggleOnEdit;

        public ReactiveProperty<int> Layer { get; } = new ReactiveProperty<int>(0);
        public bool Editing => _editing;

        private bool _editing;
        private GameObject _selectedTemplate;
        private Level _level;

        [UsedImplicitly]
        private void Start() {
            this.AssignComponent(out _level);
            Buttons.OnButtonClicked.AddListener(obj => _selectedTemplate = obj);
        }

        [UsedImplicitly]
        private void Update() {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetButtonDown("ToggleEditMode")) {
                _editing = !_editing;
                foreach (var toToggle in ToggleOnEdit) {
                    toToggle.gameObject.SetActive(!toToggle.gameObject.activeSelf);
                }
            }

            var camera = Camera.main;
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, camera.farClipPlane, 1 << gameObject.layer)) {
                return;
            }
            var point = SnapToGrid(hit.point);
            point.y = 0;
            
            Cursor.position = Vector3.Lerp(Cursor.position, point, Time.time);

            if (Input.GetMouseButtonDown(1)) {
                DestroyExistingOnCurrentLayer(point);
            }

            if (
                !Input.GetMouseButtonDown(0)
                || _selectedTemplate == null
                || EventSystem.current.IsPointerOverGameObject()
            ) {
                return;
            }

            DestroyExistingOnCurrentLayer(point);
            _level.InstantiatePrototype(_selectedTemplate, point, Layer.Value);
        }

        private void DestroyExistingOnCurrentLayer(Vector3 worldPosition) {
            var existing = _level.GetExisting(worldPosition)
                .Where(t => t.GetComponent<LayerMarker>().Layer == Layer.Value);

            foreach (var obj in existing) {
                Destroy(obj);
            }
        }

        private Vector3 SnapToGrid(Vector3 worldPosition) {
            var offset = _level.TileSize / 2 * new Vector3(1, 0, 2);
            return new Vector3(
                SnapAxis(worldPosition.x),
                SnapAxis(worldPosition.y),
                SnapAxis(worldPosition.z)
            ) - offset;
        }

        private float SnapAxis(float value) => _level.TileSize * Mathf.Ceil(value / _level.TileSize);
    }
}