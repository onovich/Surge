using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surge.Business.Game {

    public static class GamePlayerDomain {


        public static PlayerEntity Spawn(GameBusinessContext ctx, int gameTypeID) {
            var player = GameFactory.Player_Spawn(ctx.templateInfraContext, gameTypeID);
            SpawnFinished(ctx, player);
            return player;
        }

        static void SpawnFinished(GameBusinessContext ctx, PlayerEntity player) {
            ctx.Player_Set(player);
        }

        public static void Owner_BakeInput(GameBusinessContext ctx, RoleEntity owner) {
            InputEntity inputEntity = ctx.inputEntity;
            owner.Input_SetMoveAxis(inputEntity.moveAxis);
            owner.Input_SetSkillKeyDown(inputEntity.skillKeyDown);
            owner.Input_SetSkillKeyHold(inputEntity.skillKeyHold);
        }

    }

}