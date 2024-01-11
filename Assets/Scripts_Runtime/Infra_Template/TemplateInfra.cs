using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Surge {

    public static class TemplateInfra {

        public static async Task LoadAssets(TemplateInfraContext ctx) {
            var roles = await Addressables.LoadAssetsAsync<RoleTM>("TM_Role", null).Task;
            foreach (var role in roles) {
                ctx.Role_Add(role.typeID, role);
            }
        }

    }

}