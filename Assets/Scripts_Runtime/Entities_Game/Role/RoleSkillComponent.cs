using System;
using System.Collections.Generic;
using System.Linq;

namespace Surge {

    public class RoleSkillComponent {

        List<SkillSubEntity> skills;
        Dictionary<InputKeyEnum, SkillSubEntity> keyToSkill;
        int index;
        SkillSubEntity attackSkill;

        SkillSubEntity[] tempArray;

        public RoleSkillComponent() {
            skills = new List<SkillSubEntity>();
            keyToSkill = new Dictionary<InputKeyEnum, SkillSubEntity>();
            index = 0;
            tempArray = new SkillSubEntity[10];
        }

        public void Reuse() {
            index = 0;
        }

        public SkillSubEntity Find(int skillTypeID) {
            return skills.Find(skill => skill.typeID == skillTypeID);
        }

        public int TakeAll(out SkillSubEntity[] dst) {
            dst = tempArray;
            int count = skills.Count;
            if (count == 0) {
                return 0;
            }
            if (count > dst.Length) {
                tempArray = new SkillSubEntity[count];
                dst = tempArray;
            }
            skills.CopyTo(dst);
            return count;
        }

        public SkillSubEntity GetByKey(InputKeyEnum key) {
            bool has = keyToSkill.TryGetValue(key, out SkillSubEntity skill);
            if (!has) {
                return null;
            }
            return skill;
        }

        public SkillSubEntity GetAttackSkill() {
            return attackSkill;
        }

        public SkillSubEntity FindAutoCastNoCD_OnlyOneCycle() {
            int cur = index;
            for (int i = cur; i < skills.Count; i++, cur++) {
                var skill = skills[i];
                if (skill.isAutoCast && skill.cd <= 0) {
                    index = i;
                    return skill;
                }
                cur %= skills.Count;
            }
            return null;
        }

        static readonly InputKeyEnum[] keys = new InputKeyEnum[] {
            InputKeyEnum.Skill1,
            InputKeyEnum.Skill2,
            InputKeyEnum.Skill3,
            InputKeyEnum.Skill4,
        };
        public void Add(SkillSubEntity skill) {
            skills.Add(skill);
            int count = keyToSkill.Count;
            if (count >= 4) {
                return;
            }
            InputKeyEnum key = keys[count];
            keyToSkill.Add(key, skill);
            if (skill.hasCastBullet) {
                attackSkill = skill;
            }
        }

        public void Remove(SkillSubEntity skill) {
            skills.Remove(skill);
        }

        public void Foreach(Action<SkillSubEntity> action) {
            skills.ForEach(action);
        }

        public void Clear() {
            skills.Clear();
            keyToSkill.Clear();
        }

    }

}