using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surge.Business.Game {

    public static class GameBulletDomain {
        public static BulletEntity Spawn(GameBusinessContext ctx,
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
            var bullet = GameFactory.Bullet_Spawn(ctx.templateInfraContext,
                                                  ctx.idRecordService,
                                                  ctx.poolService,
                                                  typeID,
                                                  allyStatus,
                                                  hasTarget,
                                                  casterEntityType,
                                                  casterEntityID,
                                                  casterPos,
                                                  victimEntityType,
                                                  victimEntityID,
                                                  victimPos,
                                                  flyDir);
            SpawnFinished(ctx, bullet);
            return bullet;
        }

        static void SpawnFinished(GameBusinessContext ctx, BulletEntity bullet) {
            ctx.Bullet_Add(bullet);
        }

        public static void Unspawn(GameBusinessContext ctx, BulletEntity bullet) {
            ctx.Bullet_Remove(bullet);

            ctx.poolService.Bullet_Return(bullet);

            bullet.Release();
        }

        public static void BulletFly(GameBusinessContext ctx, BulletEntity bullet, float fixdt) {

            BulletFlyType flyType = bullet.flyType;
            if (flyType == BulletFlyType.Line) {
                Bullet_Fly_Straight(ctx, bullet, fixdt);
            } else if (flyType == BulletFlyType.ToTarget) {
                Bullet_Fly_ToTarget(ctx, bullet, fixdt);
            } else if (flyType == BulletFlyType.Track) {
                Bullet_Fly_Track(ctx, bullet, fixdt);
            } else {
                // Debug.LogError($"BulletFlyType {flyType} not support");
                OverlapCheck(ctx, bullet);
            }

            bullet.lifeSec -= fixdt;

        }

        static void Bullet_Fly_Straight(GameBusinessContext ctx, BulletEntity bullet, float fixdt) {
            bullet.pos = bullet.pos + bullet.dir.normalized * bullet.flySpeed * fixdt;
            bullet.Pos_UpdatePos();
            OverlapCheck(ctx, bullet);
        }

        static void Bullet_Fly_ToTarget(GameBusinessContext ctx, BulletEntity bullet, float fixdt) {
            bullet.pos = bullet.pos + bullet.dir.normalized * bullet.flySpeed * fixdt;
            bullet.Pos_UpdatePos();
            if (Vector2.Dot(bullet.targetPos - bullet.pos, bullet.dir) <= 0) {
                bullet.lifeSec = 0;
                OverlapCheck(ctx, bullet);
            }
        }

        static void Bullet_Fly_Track(GameBusinessContext ctx, BulletEntity bullet, float fixdt) {

            if (bullet.trackFlyPreSec > 0) {
                bullet.trackFlyPreSec -= fixdt;
                Vector2 inverseDir = ctx.gameEntity.random.Rotation() * -bullet.dir;
                bullet.pos = bullet.pos + inverseDir.normalized * bullet.flySpeed * fixdt;
                bullet.Pos_UpdatePos();
                return;
            }

            Vector2 targetPos = Vector2.zero;
            bool hasTarget = bullet.hasTarget;

            if (!hasTarget) {
                // 找目标
                hasTarget = QueryUtil.TryGetNearestTarget(ctx,
                                              bullet.allyStatus.GetOpposite(),
                                              bullet.pos,
                                              bullet.searchRangeIfTrack,
                                              out bullet.targetEntityType,
                                              out bullet.targetEntityID,
                                              out targetPos);
            }
            if (hasTarget) {
                bullet.targetPos = targetPos;
                bullet.dir = (targetPos - bullet.pos).normalized;
                bullet.pos += bullet.dir * bullet.flySpeed * fixdt;
                bullet.Pos_UpdatePos();
                OverlapCheck(ctx, bullet);
            } else {
                bullet.targetEntityType = EntityType.None;
                bullet.targetEntityID = 0;
                // 往前飞
                bullet.pos += bullet.dir.normalized * bullet.flySpeed * fixdt;
                bullet.Pos_UpdatePos();
            }
        }

        static void OverlapCheck(GameBusinessContext ctx, BulletEntity bullet) {

            var targetEntityType = bullet.targetEntityType;

            if (bullet.crossTimes <= 0) {
                return;
            }

            // Has Coll With Physics
            var overlaps = ctx.overlapTemp;
            int targetLayerMask;
            if (targetEntityType == EntityType.Role) {
                targetLayerMask = 1 << LayerConst.ROLE;
            } else {
                // Debug.LogError($"BulletEntityType {targetEntityType} not support");
                return;
            }

            int count = Physics2D.OverlapCircleNonAlloc(bullet.pos, bullet.radius, overlaps, targetLayerMask);
            for (int i = 0; i < count; i += 1) {
                if (bullet.crossTimes <= 0) {
                    break;
                }
                var coll = overlaps[i];
                if (coll == null) {
                    continue;
                }
                if (targetEntityType == EntityType.Role) {
                    var role = coll.transform.parent.GetComponent<RoleEntity>();
                    if (role != null) {
                        Bullet_HitRole(ctx, bullet, role);
                        continue;
                    }
                }
            }
        }

        static void Bullet_HitRole(GameBusinessContext ctx, BulletEntity bullet, RoleEntity victim) {

            // 避免友军伤害
            if (bullet.allyStatus == victim.allyStatus) {
                return;
            }

            // Calc Hp
            if (!ctx.DamageArbit_Has(bullet.entityType, bullet.entityID, victim.entityType, victim.entityID)) {
                GameSumupDomain.Role_DamageByBulletHit(ctx, victim, bullet);
            }

            // Effector
            Bullet_Role_HitEffect(ctx, bullet, victim);

            // Calc Bullet Life
            bullet.crossTimes -= 1;
            if (bullet.crossTimes <= 0) {
                // VFX
                var hitEffector = bullet.hitEffector;
                if (hitEffector.hitVFXPrefab != null) {
                    GameObject vfx = GameObject.Instantiate(hitEffector.hitVFXPrefab);
                    vfx.transform.position = bullet.pos;
                    GameObject.Destroy(vfx, hitEffector.hitVFXDuration);
                }

                // SFX
                if (hitEffector.hitSFX != null) {
                    GameSFXDomain.Bullet_Hit(ctx, bullet.pos, hitEffector.hitSFX);
                }
            }

        }

        static void Bullet_Role_HitEffect(GameBusinessContext ctx, BulletEntity bullet, RoleEntity victim) {
            var effector = bullet.hitEffector;
            if (effector.hasHitAttachBuff) {
                GameRoleDomain.Buff_Attach(ctx, victim, effector.hitAttachBuffTypeID);
            }
            if (effector.hasImpact) {
                GameSumupDomain.Roles_DamageByBulletEffectorImpact(ctx, bullet, effector);
            }

            // VFX
            var hitEffector = bullet.hitEffector;
            if (hitEffector.hasImpact && hitEffector.impactRangeVFXPrefab != null) {
                GameObject vfx = GameObject.Instantiate(hitEffector.impactRangeVFXPrefab);
                vfx.transform.position = bullet.pos;
                vfx.transform.localScale = Vector3.one * hitEffector.impactRange * 2;
                GameObject.Destroy(vfx, hitEffector.impactRangeVFXDuration);
            }

            // SFX
            if (hitEffector.hitSFX != null) {
                GameSFXDomain.Bullet_Hit(ctx, bullet.pos, hitEffector.hitSFX);
            }
        }


    }

}