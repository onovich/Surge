using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surge.Business.Game {

    public class GameFactory {

        // Game
        public static GameEntity Game_Spawn(TemplateInfraContext templateInfraContext, int typeID) {
            var config = templateInfraContext.GameConfig_Get();
            var gameEntity = new GameEntity();
            gameEntity.gameTypeID = typeID;
            gameEntity.chapterTypeID = config.startChapterTypeID;
            gameEntity.random = new RandomService(101052099, 0);
            gameEntity.Stage_EnterInBattle();
            return gameEntity;
        }

        // Role
        public static RoleEntity Role_Create(AssetsInfraContext assetsInfraContext) {
            var prefab = assetsInfraContext.Entity_GetRole();
            RoleEntity role = GameObject.Instantiate(prefab).GetComponent<RoleEntity>();
            role.Ctor();
            return role;
        }

        public static RoleEntity Role_Spawn(TemplateInfraContext templateInfraContext,
                                            IDRecordService idRecordService,
                                            PoolService poolService,
                                            int typeID,
                                            RoleType roleType,
                                            AllyStatus allyStatus,
                                            AIType aiType,
                                            Vector2 pos,
                                            float dir) {

            bool has = templateInfraContext.Role_TryGet(typeID, out var roleTM);
            if (!has) {
                SLog.LogError($"GameFactory.SpawnRoleEntity: TypeID={typeID} not found");
                return null;
            }

            RoleEntity role = poolService.Role_Take();
            role.Pool_Reuse();

            role.entityID = ++idRecordService.role;
            role.typeID = typeID;
            role.typeName = roleTM.typeName;
            role.aiType = aiType;
            role.RoleType_Set(roleType);
            role.allyStatus = allyStatus;

            role.Pos_SetPos(pos);

            // Input
            role.Input_SetMoveAxis(Vector2.zero);

            // Dead Drop
            // TODO

            // FSM
            role.FSM_EnterNormal();

            // Attr
            role.Attr_InitAll(roleTM.moveSpeed, roleTM.hpMax, roleTM.atk, roleTM.def, roleTM.attackRange, roleTM.pickRange);

            // Skill
            var skillTypeIds = roleTM.skillTypeIds;
            if (skillTypeIds != null) {
                foreach (var skillTypeId in skillTypeIds) {
                    var skillSub = GameFactory.Skill_Spawn(templateInfraContext, idRecordService, poolService, skillTypeId);
                    role.Skill_Add(skillSub);
                }
            }

            // Mesh
            role.Spr_Set(roleTM.spr);

            return role;

        }

        public static SkillSubEntity Skill_Spawn(TemplateInfraContext templateInfraContext, IDRecordService idRecordService, PoolService poolService, int typeID) {

            bool has = templateInfraContext.Skill_TryGet(typeID, out var tm);
            if (!has) {
                SLog.LogError($"GameFactory.SpawnSkillSubEntity: TypeID={typeID} not found");
                return null;
            }

            SkillSubEntity skill = poolService.SkillSub_Take();
            skill.Reuse();

            skill.typeName = tm.typeName;
            skill.desc = tm.desc;

            skill.entityID = ++idRecordService.skill;
            skill.typeID = typeID;
            skill.cd = tm.cdMax;
            skill.cdMax = tm.cdMax;

            skill.isAutoCast = tm.isAutoCast;
            skill.castByHold = tm.castByHold;
            skill.preCastSecMax = tm.preCastSec;
            skill.castingMaintainSecMax = tm.castingMaintainSec;
            skill.castingIntervalSecMax = tm.castingIntervalSec;
            skill.endCastSecMax = tm.endCastSec;

            skill.hasCastBullet = tm.hasCastBullet;
            skill.castBulletTypeID = tm.castBulletTypeID;

            skill.hasDestroySelf = tm.hasDestroySelf;

            return skill;

        }

        public static BulletEntity Bullet_Create(AssetsInfraContext assetsInfraContext) {
            var prefab = assetsInfraContext.Entity_GetBullet();
            BulletEntity bullet = GameObject.Instantiate(prefab).GetComponent<BulletEntity>();
            bullet.Ctor();
            return bullet;
        }

        public static BulletEntity Bullet_Spawn(TemplateInfraContext templateInfraContext,
                                                IDRecordService idRecordService,
                                                PoolService poolService,
                                                int typeID,
                                                AllyStatus allyStatus,
                                                bool hasTarget,
                                                EntityType casterEntityType,
                                                int casterEntityID,
                                                Vector2 casterPos,
                                                EntityType victimEntityType,
                                                int victimEntityID,
                                                Vector2 victimPos,
                                                Vector2 flyDir) {

            bool has = templateInfraContext.Bullet_TryGet(typeID, out var tm);
            if (!has) {
                SLog.LogError($"GameFactory.SpawnBulletEntity: TypeID={typeID} not found");
                return null;
            }

            BulletEntity bullet = poolService.Bullet_Take();
            bullet.Reuse();

            bullet.entityID = ++idRecordService.bullet;
            bullet.typeID = typeID;
            bullet.allyStatus = allyStatus;

            bullet.hasTarget = hasTarget;
            bullet.casterEntityType = casterEntityType;
            bullet.casterEntityID = casterEntityID;
            bullet.targetEntityType = victimEntityType;
            bullet.targetEntityID = victimEntityID;
            bullet.fromPos = casterPos;
            bullet.targetPos = victimPos;
            bullet.pos = casterPos;
            bullet.dir = flyDir;

            bullet.flyType = tm.flyType;
            bullet.searchRangeIfTrack = tm.searchRangeIfTrack;
            bullet.flySpeed = tm.flySpeed;
            bullet.radius = tm.radius;
            bullet.atk = tm.atk;
            bullet.crossTimes = tm.crossTimes;
            bullet.lifeSec = tm.lifeSec;
            bullet.trackFlyPreSec = 0.5f;

            bullet.hitEffector.FromTM(tm.hitEffector);

            bullet.Pos_UpdatePos();
            bullet.SR_SetSprite(tm.spr);


            return bullet;
        }

        // Buff
        public static BuffSubEntity Buff_Spawn(TemplateInfraContext templateInfraContext, IDRecordService idRecordService, PoolService poolService, int typeID) {
            bool has = templateInfraContext.Buff_TryGet(typeID, out var tm);
            if (!has) {
                SLog.LogError($"GameFactory.SpawnBuffSubEntity: TypeID={typeID} not found");
                return null;
            }

            BuffSubEntity buff = poolService.Buff_Take();
            buff.Reuse();

            buff.entityID = ++idRecordService.buff;
            buff.typeID = typeID;

            buff.lifeTimer = tm.lifeSec;
            buff.lifeSecMax = tm.lifeSec;

            buff.hasDot = tm.hasDot;
            buff.dotIntervalSec = tm.dotIntervalSec;
            buff.dotIntervalTimer = tm.dotIntervalSec;
            buff.dotAtk = tm.dotAtk;

            buff.hasIce = tm.hasIce;
            buff.iceSlowRate = tm.iceSlowRate;
            buff.hasIceImpact = tm.hasIceImpact;
            buff.iceImpactNeecCount = tm.iceImpactNeecCount;
            buff.iceImpactAtk = tm.iceImpactAtk;

            return buff;
        }

    }

}