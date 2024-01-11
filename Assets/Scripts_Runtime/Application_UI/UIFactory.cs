using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surge.UI {

    public static class UIFactory {

        // UniquePanel
        public static T UniquePanel_Open<T>(UIAppContext ctx) where T : MonoBehaviour {

            var dic = ctx.prefabDict;
            var name = typeof(T).Name;
            var prefab = GetPrefab(ctx, name);
            var panel = GameObject.Instantiate(prefab, ctx.canvasRoot).GetComponent<T>();
            ctx.UniquePanel_Add(name, panel);
            return panel;

        }

        public static void UniquePanel_Close<T>(UIAppContext ctx) where T : MonoBehaviour {
            var panel = ctx.UniquePanel_Get<T>();
            if (panel == null) {
                SLog.LogError($"UIFactory.UniquePanel_Close<{typeof(T).Name}>: panel not found");
                return;
            }
            ctx.UniquePanel_Remove(typeof(T).Name);
            GameObject.Destroy(panel.gameObject);
        }

        static GameObject GetPrefab(UIAppContext ctx, string name) {
            bool has = ctx.prefabDict.TryGetValue(name, out var prefab);
            if (!has) {
                SLog.LogError($"UIFactory.GetPrefab<{name}>: prefab not found");
                return null;
            }
            return prefab;
        }

    }

}