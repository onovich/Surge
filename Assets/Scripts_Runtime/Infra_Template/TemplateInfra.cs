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

            var chapters = await Addressables.LoadAssetsAsync<ChapterSO>("TM_Chapter", null).Task;
            foreach (var item in chapters) {
                ctx.Chapter_Add(item.tm);
            }

            var waves = await Addressables.LoadAssetsAsync<WaveSO>("TM_Wave", null).Task;
            foreach (var item in waves) {
                ctx.Wave_Add(item.tm);
            }

            var configs = await Addressables.LoadAssetsAsync<GameSO>("TM_Config", null).Task;
            foreach (var item in configs) {
                ctx.GameConfig_Set(item.tm);
            }

        }

    }

}