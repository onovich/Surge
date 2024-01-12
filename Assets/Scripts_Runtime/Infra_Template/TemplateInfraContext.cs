using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surge {

    public class TemplateInfraContext {

        Dictionary<int, RoleTM> roles;
        Dictionary<int, SkillTM> skills;
        Dictionary<int, BulletTM> bullets;
        Dictionary<int, BuffTM> buffs;
        SFXTableSO sfxTableSO;
        VFXTableSO vfxTableSO;

        public TemplateInfraContext() {
            roles = new Dictionary<int, RoleTM>();
            skills = new Dictionary<int, SkillTM>();
            bullets = new Dictionary<int, BulletTM>();
            buffs = new Dictionary<int, BuffTM>();
        }

        // Role
        public void Role_Add(RoleTM role) {
            roles.Add(role.typeID, role);
        }

        public bool Role_TryGet(int typeID, out RoleTM role) {
            return roles.TryGetValue(typeID, out role);
        }

        // SKill
        public void Skill_Add(SkillTM skill) {
            int key = skill.typeID;
            skills.Add(key, skill);
        }

        public bool Skill_TryGet(int typeID, out SkillTM skill) {
            int key = typeID;
            return skills.TryGetValue(key, out skill);
        }

        // Bullet
        public void Bullet_Add(BulletTM bullet) {
            int key = bullet.typeID;
            bullets.Add(key, bullet);
        }

        public bool Bullet_TryGet(int typeID, out BulletTM bullet) {
            int key = typeID;
            return bullets.TryGetValue(key, out bullet);
        }

        // Buff
        public void Buff_Add(BuffTM buff) {
            int key = buff.typeID;
            buffs.Add(key, buff);
        }

        public bool Buff_TryGet(int typeID, out BuffTM buff) {
            int key = typeID;
            return buffs.TryGetValue(key, out buff);
        }

        // SFX
        public void SFX_SetTable(SFXTableSO sfxSO) {
            this.sfxTableSO = sfxSO;
        }

        public SFXTableSO SFX_GetTable() {
            return sfxTableSO;
        }

        // VFX
        public void VFX_SetTable(VFXTableSO vfxSO) {
            this.vfxTableSO = vfxSO;
        }

        public VFXTableSO VFX_GetTable() {
            return vfxTableSO;
        }

    }

}