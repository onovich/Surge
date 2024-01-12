using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surge.Business.Game {
    public static class GameRoleDomain {

        // Role
        public static RoleEntity Spawn(GameBusinessContext ctx,
                                     int typeID,
                                     RoleType roleType,
                                     AllyStatus allyStatus,
                                     AIType aiType,
                                     Vector2 pos) {
            var role = GameFactory.Role_Spawn(ctx.templateInfraContext,
                                              ctx.idRecordService,
                                              ctx.poolService,
                                              typeID,
                                              roleType,
                                              allyStatus,
                                              aiType,
                                              pos);
            Role_SpawnFinished(ctx, role);
            return role;
        }

        public static void Role_FakeDead(GameBusinessContext ctx, RoleEntity role) {

            int buffLen = role.Buff_TakeAll(out var buffs);
            for (int i = 0; i < buffLen; i += 1) {
                var buff = buffs[i];
                buff.Release();
                ctx.poolService.Buff_Return(buff);
                role.Buff_Remove(buff);
            }

            role.FSM_EnterFakeDead(1f);

        }

        static void Role_SpawnFinished(GameBusinessContext ctx, RoleEntity role) {
            Role_RecordLastPos(ctx, role);
            ctx.Role_Add(role);
        }

        public static void Role_RecordLastPos(GameBusinessContext ctx, RoleEntity role) {
            role.Pos_RecordLastPosInt();
        }

        public static void Role_UpdatePosDict(GameBusinessContext ctx, RoleEntity role) {
            if (!role.Pos_IsDifferentFromLast()) {
                return;
            }
            ctx.Role_AddOrUpdatePosDict(role);
            Role_RecordLastPos(ctx, role);
        }

        // Skill
        public static void Skill_CDTick(GameBusinessContext ctx, RoleEntity role, float dt) {
            role.Skill_Foreach(skill => {
                skill.cd -= dt;
                if (skill.cd < 0) {
                    skill.cd = 0;
                }
            });
        }

        public static void Skill_TryAutoCast(GameBusinessContext ctx, RoleEntity role) {
            var skill = role.Skill_FindAutoCastNoCD_OnlyOneCycle();
            if (skill == null) {
                return;
            }

            float attackRange = role.Attr_AttackRange();
            bool hasTarget = QueryUtil.TryGetNearestTarget(ctx,
                                                                 role.allyStatus.GetOpposite(),
                                                                 role.Pos_GetPos(),
                                                                 attackRange,
                                                                 out EntityType targetType,
                                                                 out int targetID,
                                                                 out var targetPos);
            if (hasTarget) {
                Skill_PreCast(ctx, role, skill, hasTarget, targetType, targetID, targetPos, targetPos - role.Pos_GetPos());
            }

        }

        public static void Skill_TryCastByPos(GameBusinessContext ctx, RoleEntity caster, SkillSubEntity skill, Vector2 inputTargetPos, Vector2Int inputPosInt) {

            bool hasTarget;
            EntityType targetType = EntityType.None;
            int targetID = 0;
            Vector2 targetPos;

            hasTarget = false;
            targetPos = Vector2.zero;

            Skill_PreCast(ctx, caster, skill, hasTarget, targetType, targetID, targetPos, targetPos - caster.Pos_GetPos());

        }

        public static void Skill_TryCastByID(GameBusinessContext ctx, RoleEntity caster, SkillSubEntity skill, EntityType targetType, int targetID, Vector2 targetPos) {
            Skill_PreCast(ctx, caster, skill, true, targetType, targetID, targetPos, targetPos - caster.Pos_GetPos());
        }

        public static void Skill_PreCast(GameBusinessContext ctx, RoleEntity role, SkillSubEntity skill, bool hasTarget, EntityType targetType, int targetID, Vector2 targetPos, Vector2 targetDir) {
            bool allowCast = true;
            allowCast = role.FSM_GetStatus() == RoleFSMStatus.Normal;
            if (!allowCast) {
                return;
            }
            role.hasTarget = hasTarget;
            role.targetEntityType = targetType;
            role.targetEntityID = targetID;
            role.targetPos = targetPos;
            role.targetDir = targetPos - role.Pos_GetPos();
            role.FSM_EnterCasting(skill);
        }

        public static void Skill_Cast(GameBusinessContext ctx, RoleEntity role, SkillSubEntity skill) {

            Vector2 targetPos;
            Vector2 flyDir;
            if (role.hasTarget) {
                targetPos = role.targetPos;
                flyDir = targetPos - role.Pos_GetPos();
            } else {
                targetPos = role.targetPos + role.Pos_GetFaceDir();
                flyDir = role.Pos_GetFaceDir();
            }

            if (skill.hasCastBullet) {
                _ = GameBulletDomain.Spawn(ctx,
                                           skill.castBulletTypeID,
                                           role.allyStatus,
                                           role.hasTarget,
                                           role.entityType,
                                           role.entityID,
                                           role.Pos_GetPos(),
                                           role.targetEntityType,
                                           role.targetEntityID,
                                           targetPos,
                                           flyDir);
            }

            skill.cd = skill.cdMax;

        }

        // Buff
        public static void Buff_Attach(GameBusinessContext ctx, RoleEntity role, int buffTypeID) {
            BuffSubEntity buff = GameFactory.Buff_Spawn(ctx.templateInfraContext, ctx.idRecordService, ctx.poolService, buffTypeID);
            role.Buff_Add(buff);
        }

        public static void Buff_Calc(GameBusinessContext ctx, RoleEntity role, float dt) {
            int buffLen = role.Buff_TakeAll(out var buffs);
            for (int i = 0; i < buffLen; i += 1) {
                var buff = buffs[i];
                buff.TimeTick(dt);
                if (buff.dotIntervalTimer <= 0) {
                    buff.dotIntervalTimer = buff.dotIntervalSec + buff.dotIntervalTimer;
                    Buff_DotDamage(ctx, role, buff);
                }
                if (buff.IsDead) {
                    role.Buff_Remove(buff);
                }
            }
        }

        public static void Buff_DotDamage(GameBusinessContext ctx, RoleEntity victim, BuffSubEntity buff) {
            // Dot
            int oldHp = victim.Attr_Hp();
            int hp = PFAttr.HurtHP(oldHp, buff.dotAtk, 0);
            victim.Attr_SetHp(hp);
        }

    }

}