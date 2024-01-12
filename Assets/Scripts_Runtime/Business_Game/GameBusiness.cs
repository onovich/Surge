using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surge.Business.Game {

    public static class GameBusiness {

        public static void Init(GameBusinessContext ctx) {

            // Game
            Physics.IgnoreLayerCollision(8, 8, true);

            // Pool
            ctx.poolService = new PoolService(
              () => GameFactory.Role_Create(ctx.assetsInfraContext),
              () => GameFactory.Bullet_Create(ctx.assetsInfraContext),
              () => new SkillSubEntity(),
              () => new BuffSubEntity()
            );

            // Input
            var inputEntity = ctx.inputEntity;
            inputEntity.Ctor();
            inputEntity.Keybinding_Set(InputKeyEnum.MoveLeft, new KeyCode[] { KeyCode.A });
            inputEntity.Keybinding_Set(InputKeyEnum.MoveRight, new KeyCode[] { KeyCode.D });
            inputEntity.Keybinding_Set(InputKeyEnum.MoveForward, new KeyCode[] { KeyCode.W });
            inputEntity.Keybinding_Set(InputKeyEnum.MoveBackward, new KeyCode[] { KeyCode.S });
            inputEntity.Keybinding_Set(InputKeyEnum.Jump, new KeyCode[] { KeyCode.Space });
            inputEntity.Keybinding_Set(InputKeyEnum.Interact, new KeyCode[] { KeyCode.E });
            inputEntity.Keybinding_Set(InputKeyEnum.Cancel, new KeyCode[] { KeyCode.Escape, KeyCode.Mouse1 });
            inputEntity.Keybinding_Set(InputKeyEnum.Attack, new KeyCode[] { KeyCode.Mouse0 });
            inputEntity.Keybinding_Set(InputKeyEnum.Skill1, new KeyCode[] { KeyCode.Alpha1 });
            inputEntity.Keybinding_Set(InputKeyEnum.Skill2, new KeyCode[] { KeyCode.Alpha2 });
            inputEntity.Keybinding_Set(InputKeyEnum.Skill3, new KeyCode[] { KeyCode.Alpha3 });
            inputEntity.Keybinding_Set(InputKeyEnum.Skill4, new KeyCode[] { KeyCode.Alpha4 });
        }

        public static void TearDown(GameBusinessContext ctx) {
            ExitGame(ctx);
        }

        static void ExitGame(GameBusinessContext ctx) {

            // Tear Down
            // - Entities

            // - UI

            // - HUD

        }

        public static void ExitApplicaiton(GameBusinessContext ctx) {
            ExitGame(ctx);
            Application.Quit();
            SLog.Log("Application.Quit()");
        }

        static void RestartGame(GameBusinessContext ctx) {
            ExitGame(ctx);
            StartGame(ctx, ctx.gameEntity.gameTypeID);
        }

        public static void StartGame(GameBusinessContext ctx, int gameTypeID) {

            var gameEntity = ctx.gameEntity;
            gameEntity.random = new RandomService(101052099, 0);
            gameEntity.Stage_EnterInBattle();

            // - Role
            var owner = GameRoleDomain.Spawn(ctx,
                1001,
                RoleType.Player,
                AllyStatus.Justice,
                AIType.None,
                new Vector2(0, -5));
            var player = GamePlayerDomain.Spawn(ctx, gameTypeID);
            player.ownerRoleEntityID = owner.entityID;

            // - Role: Enemy's
            // TODO
            // foreach (var roleSpawnTM in gameTM.roleSpawnArr) {
            //     _ = GameRoleDomain.Spawn(ctx, roleSpawnTM.typeID, roleSpawnTM.level, RoleType.Monster, roleSpawnTM.allyStatus, roleSpawnTM.aiType, roleSpawnTM.pos, true, 0);
            // }

            StartFinished(ctx, player, owner);

        }

        static void StartFinished(GameBusinessContext ctx, PlayerEntity player, RoleEntity owner) {

            // Camera
            // TODO

            // UI 
            // TODO

            // BGM
            // TODO
        }

        public static void Tick(GameBusinessContext ctx, float dt) {
            var player = ctx.playerEntity;
            if (player == null) {
                return;
            }

            var gameEntity = ctx.gameEntity;
            dt *= gameEntity.gameTimeSpeedRate;

            ProcessInput(ctx, dt);

            LogicTick(ctx, dt);

            float restTime = dt;
            float intervalTime = gameEntity.intervalTime;
            for (; restTime >= intervalTime; restTime -= intervalTime) {
                FixedTick(ctx, intervalTime);
            }
            FixedTick(ctx, restTime);

            LateTick(ctx, dt);
        }

        static void ProcessInput(GameBusinessContext ctx, float dt) {
            InputEntity inputEntity = ctx.inputEntity;
            inputEntity.ProcessInput(ctx.cameraCoreContext.MainCamera, dt);
        }

        static void LogicTick(GameBusinessContext ctx, float dt) {
            // ==== Game ====
            var game = ctx.gameEntity;
            var player = ctx.playerEntity;

            // - Role: Owner
            var owner = ctx.Role_GetOwner();
            GamePlayerDomain.Owner_BakeInput(ctx, owner);

            var stage = game.battleStage;
            if (stage == BattleStage.InBattle) {
                player.gameTime += dt;
            }

            // TODO: Camera
        }

        static void FixedTick(GameBusinessContext ctx, float dt) {
            // - Battle Stage
            var stage = ctx.gameEntity.battleStage;
            if (stage == BattleStage.InBattle) {
                InBattle_FixedTick(ctx, dt);
            } else if (stage == BattleStage.FocusBoss) {

            } else if (stage == BattleStage.End) {

            } else if (stage == BattleStage.Pause) {

            }
        }

        static void InBattle_FixedTick(GameBusinessContext ctx, float dt) {
            GameStageDomain.ApplyResult(ctx);

            // - Role: Owner
            var owner = ctx.Role_GetOwner();

            // - Bullet
            ctx.Bullet_ForEach(bullet => {
                if (bullet.IsDead) {
                    return;
                }
                GameBulletFSMController.FixedTick(ctx, bullet, dt);
            });

            // - Role
            ctx.Role_ForEach(role => {
                if (role.isDead) {
                    return;
                }
                GameRoleFSMController.FixedTickFSM(ctx, role, dt);
            });

            // - UpdateLastPos
            ctx.Role_ForEach(role => {
                GameRoleDomain.Role_UpdatePosDict(ctx, role);
            });

            Physics2D.Simulate(dt);
        }

        static void LateTick(GameBusinessContext ctx, float dt) {

            var player = ctx.playerEntity;
            if (player == null) {
                return;
            }

            var gameEntity = ctx.gameEntity;
            var stage = gameEntity.battleStage;
            if (stage == BattleStage.InBattle) {
                InBattle_LateTick(ctx, dt);
            } else if (stage == BattleStage.End) {

            } else if (stage == BattleStage.FocusBoss) {

            } else if (stage == BattleStage.Pause) {

            }

            // Camera
            // TODO

            // BGM
            // TODO

            InputEntity inputEntity = ctx.inputEntity;
            inputEntity.Reset();

        }

        static void InBattle_LateTick(GameBusinessContext ctx, float dt) {
            var gameEntity = ctx.gameEntity;
            var player = ctx.playerEntity;
            dt *= gameEntity.gameTimeSpeedRate;

            var owner = ctx.Role_GetOwner();

            // Input & UI
            // TODO

            if (ctx.inputEntity.skillKeyDown != InputKeyEnum.None) {
                GameUIDomain.SkillShortcut_ChooseSkillByKey(ctx, player, ctx.inputEntity.skillKeyDown);
            }

            // Role: All
            int roleLen = ctx.Role_TakeAll(out var roles);
            for (int i = 0; i < roleLen; i += 1) {
                var role = roles[i];
                if (role.isDead) {
                    if (role.roleType == RoleType.Monster) {
                        // TODO: Add Exp & Drop Dead Props
                    } else {
                        // Reborn
                        GameRoleDomain.Role_FakeDead(ctx, role);
                    }
                }
            }

            // Bullet
            int bulletLen = ctx.Bullet_TakeAll(out var bullets);
            for (int i = 0; i < bulletLen; i += 1) {
                var bullet = bullets[i];
                if (bullet.IsDead) {
                    GameBulletDomain.Unspawn(ctx, bullet);
                }
            }

        }

        // ==== UI Event ====
        // TODO

    }

}