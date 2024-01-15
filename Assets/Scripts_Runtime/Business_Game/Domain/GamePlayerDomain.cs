using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surge.Business.Game {

    public static class GamePlayerDomain {

        public static void Owner_BakeInput(GameBusinessContext ctx, RoleEntity owner) {
            InputEntity inputEntity = ctx.inputEntity;
            owner.Input_SetMoveAxis(inputEntity.moveAxis);
            owner.Input_SetSkillKeyDown(inputEntity.skillKeyDown);
            owner.Input_SetSkillKeyHold(inputEntity.skillKeyHold);
        }

    }

}