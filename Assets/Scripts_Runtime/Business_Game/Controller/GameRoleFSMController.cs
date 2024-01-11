using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surge.Business.Game {

    public class GameRoleFSMController {
        public static void FixedTickFSM(GameBusinessContext ctx, RoleEntity role, float fixdt) {
            RoleFSMStatus status = role.FSM_GetStatus();
            if (status == RoleFSMStatus.Normal) {
                FixedTickFSM_Normal(ctx, role, fixdt);
            } else if (status == RoleFSMStatus.Normal) {
                FixedTickFSM_Casting(ctx, role, fixdt);
            } else if (status == RoleFSMStatus.FakeDead) {
                FixedTickFSM_FakeDead(ctx, role, fixdt);
            } else if (status == RoleFSMStatus.Reborn) {
                FixedTickFSM_Reborn(ctx, role, fixdt);
            } else {
                SLog.LogError($"GameRoleFSMController.FixedTickFSM: unknown status: {status}");
            }
        }

        static void FixedTickFSM_Normal(GameBusinessContext ctx, RoleEntity role, float fixdt) {
            if (role.roleType == RoleType.Player) {
                // Owner Move
                role.Move_Move(fixdt);

            } else if (role.roleType == RoleType.Monster) {
                var monster = role;
                if (monster.aiType == AIType.FlyStraightly) {
                    monster.Move_Straightly(fixdt);
                }
            }

            // Auto Cast Skill
            GameRoleDomain.Skill_TryAutoCast(ctx, role);

            // Calc Skill CD
            GameRoleDomain.Skill_CDTick(ctx, role, fixdt);

            // Calc Buff
            GameRoleDomain.Buff_Calc(ctx, role, fixdt);

        }

        static void FixedTickFSM_Casting(GameBusinessContext ctx, RoleEntity role, float fixdt) {
            var fsm = role.fsmCom;
            var skill = fsm.casting_skill;
            if (fsm.casting_isEntering) {
                fsm.casting_isEntering = false;
                return;
            }

            // 前摇
            ref var stage = ref fsm.casting_stage;
            if (stage == SkillCastStage.PreCast) {
                fsm.casting_preCastTimer -= fixdt;
                if (fsm.casting_preCastTimer <= 0) {
                    fsm.casting_preCastTimer = 0;
                    // Enter Casting Once
                    stage = SkillCastStage.Casting;
                }
            } else if (stage == SkillCastStage.Casting) {
                fsm.casting_castingIntervalTimer -= fixdt;
                if (fsm.casting_castingIntervalTimer <= 0) {
                    fsm.casting_castingIntervalTimer = skill.castingIntervalSecMax;
                    GameRoleDomain.Skill_Cast(ctx, role, fsm.casting_skill);
                }

                fsm.casting_castingTimer -= fixdt;
                if (fsm.casting_castingTimer <= 0) {
                    // Enter EndCast Once
                    stage = SkillCastStage.EndCast;
                }
            } else if (stage == SkillCastStage.EndCast) {
                fsm.casting_endCastTimer -= fixdt;
                if (fsm.casting_endCastTimer < 0) {
                    role.targetEntityType = EntityType.None;
                    role.targetEntityID = 0;
                    role.FSM_EnterNormal();
                }
            }
        }

        static void FixedTickFSM_FakeDead(GameBusinessContext ctx, RoleEntity role, float fixdt) {
            var fsm = role.fsmCom;
            if (fsm.fakeDead_isEntering) {
                fsm.fakeDead_isEntering = false;
                role.Coll_Deactive();
                role.Move_Stop();
                return;
            }

            fsm.fakeDead_timer -= fixdt;
            if (fsm.fakeDead_timer <= 0) {
                fsm.fakeDead_timer = 0;
                role.FSM_EnterReborn(role.rebornDuration);
            }
        }

        static void FixedTickFSM_Reborn(GameBusinessContext ctx, RoleEntity role, float fixdt) {
            var fsm = role.fsmCom;
            if (fsm.reborn_isEntering) {
                fsm.reborn_isEntering = false;

                // TODO: 生成地点
                var bornPos = role.allyStatus == AllyStatus.Justice ? new Vector2(0, -5) : new Vector2(0, 5);
                role.Pos_SetPos(bornPos);
                return;
            }

            fsm.reborn_timer -= fixdt;

            float hpMax = role.Attr_HpMax();
            float hp = (fsm.reborn_duration - fsm.reborn_timer) / fsm.reborn_duration * hpMax;
            role.Attr_SetHp((int)hp);

            if (fsm.reborn_timer <= 0) {
                fsm.reborn_timer = 0;
                role.Coll_Active();
                role.rebornDuration += 5;
                role.rebornDuration = Mathf.Min(role.rebornDuration, 35);
                role.FSM_EnterNormal();
            }
        }
    }

}