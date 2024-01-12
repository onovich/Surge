using System;
using System.Collections.Generic;
using UnityEngine;

namespace Surge.Business.Game {

    public static class GameUIDomain {

        public static void SkillShortcut_ChooseSkillByKey(GameBusinessContext ctx, PlayerEntity player, InputKeyEnum key) {
            var owner = ctx.Role_GetOwner();
            var skill = owner.Skill_GetByKey(key);
            if (skill == null) {
                return;
            }
            SkillShortcut_ChooseSkill(ctx, player, skill.typeID);
        }

        public static void SkillShortcut_ChooseSkill(GameBusinessContext ctx, PlayerEntity player, int skillTypeID) {

            var owner = ctx.Role_GetOwner();
            var skill = owner.Skill_Find(skillTypeID);
            if (skill == null) {
                return;
            }
            if (skill.cd > 0) {
                return;
            }
            if (skill.isAutoCast) {
                return;
            }

            GameRoleDomain.Skill_TryCastByPos(ctx, owner, skill, owner.Pos_GetPos(), owner.Pos_GetPosInt());

        }

    }

}