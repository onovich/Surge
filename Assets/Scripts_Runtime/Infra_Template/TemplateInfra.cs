using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Surge {

    public static class TemplateInfra {

        public static async Task LoadAssets(TemplateInfraContext ctx) {
            var roles = await Addressables.LoadAssetsAsync<RoleSO>("TM_Role", null).Task;
            foreach (var item in roles) {
                ctx.Role_Add(item.tm);
            }

            var skills = await Addressables.LoadAssetsAsync<SkillSO>("TM_Skill", null).Task;
            foreach (var item in skills) {
                ctx.Skill_Add(item.tm);
            }

            var bullets = await Addressables.LoadAssetsAsync<BulletSO>("TM_Bullet", null).Task;
            foreach (var item in bullets) {
                ctx.Bullet_Add(item.tm);
            }

            var buffs = await Addressables.LoadAssetsAsync<BuffSO>("TM_Buff", null).Task;
            foreach (var item in buffs) {
                ctx.Buff_Add(item.tm);
            }

        }

    }

}