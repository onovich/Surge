using System;
using UnityEngine;

namespace Surge.Business.Game {

    // 结算
    public static class GameSumupDomain {

        // Role & Bullet: Hit Damage
        public static void Role_DamageByBulletHit(GameBusinessContext ctx, RoleEntity victim, BulletEntity bullet) {
            Role_Hurt(victim, bullet.atk, false);
            ctx.DamageArbit_Add(bullet.entityType, bullet.entityID, victim.entityType, victim.entityID);
        }

        public static void Roles_DamageByBulletEffectorImpact(GameBusinessContext ctx, BulletEntity bullet, EffectorModel effector) {
            ctx.Role_ForEach(role => {
                if (role.allyStatus == bullet.allyStatus) {
                    return;
                }
                if (!PFMath.IsInRange(role.Pos_GetPos(), bullet.pos, effector.impactRange)) {
                    return;
                }
                int oldHp = role.Attr_Hp();
                int hp = PFAttr.HurtHP(oldHp, effector.impactAtk, role.Attr_Def());
                role.Attr_SetHp(hp);
                if (effector.hasImpactAttachBuff) {
                    GameRoleDomain.Buff_Attach(ctx, role, effector.impactAttachBuffTypeID);
                }
            });
        }

        static void Role_Hurt(RoleEntity victim, int damage, bool isIgnoreDef) {
            int oldHp = victim.Attr_Hp();
            int def = isIgnoreDef ? 0 : victim.Attr_Def();
            int hp = PFAttr.HurtHP(oldHp, damage, def);
            victim.Attr_SetHp(hp);
        }

    }

}