using System.Collections.Generic;
using Surge.UI;
using UnityEngine;

namespace Surge {

    public class UIAppContext {

        // Internal
        public Canvas mainCanvas;
        public Transform canvasRoot => mainCanvas.transform;
        public Transform hudFakeCanvas;

        public UIEventCenter evt;

        public Dictionary<string, GameObject> prefabDict;
        public Dictionary<string, MonoBehaviour> openedUniqueDict;

        // External
        public CameraCoreContext cameraCoreContext;
        public TemplateInfraContext templateInfracContext;

        public UIAppContext() {

            evt = new UIEventCenter();

            prefabDict = new Dictionary<string, GameObject>();
            openedUniqueDict = new Dictionary<string, MonoBehaviour>();

        }

        public void Inject(Canvas mainCanvas, Transform hudFakeCanvas) {
            this.mainCanvas = mainCanvas;
            this.hudFakeCanvas = hudFakeCanvas;
        }

        public bool Assets_TryGetPrefab(string name, out GameObject prefab) {
            return prefabDict.TryGetValue(name, out prefab);
        }

        public void Assets_AddPrefab(string name, GameObject prefab) {
            prefabDict.Add(name, prefab);
        }

        // Panel_Unique
        public void UniquePanel_Add(string name, MonoBehaviour panel) {
            openedUniqueDict.Add(name, panel);
        }

        public void UniquePanel_Remove(string name) {
            openedUniqueDict.Remove(name);
        }

        public T UniquePanel_Get<T>() where T : MonoBehaviour {
            var name = typeof(T).Name;
            bool has = openedUniqueDict.TryGetValue(name, out var panel);
            if (!has) {
                return null;
            }
            return panel as T;
        }

    }

}