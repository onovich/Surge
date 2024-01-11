using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Surge.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Surge {

    public static class UIApp {

        public static async Task LoadAssets(UIAppContext ctx) {
            var list = await Addressables.LoadAssetsAsync<GameObject>("UI", null).Task;
            foreach (var item in list) {
                ctx.Assets_AddPrefab(item.name, item);
            }
        }

        public static void Init(UIAppContext ctx) {
        }

        public static void Tick(UIAppContext ctx, float dt) {

        }

        public static void Login_Open(UIAppContext ctx) {
            PanelLoginDomain.Open(ctx);
        }

        public static void Login_TryClose(UIAppContext ctx) {
            var panel = ctx.UniquePanel_Get<Panel_Login>();
            if (panel == null) {
                return;
            }
            PanelLoginDomain.Close(ctx);
        }

    }

}
