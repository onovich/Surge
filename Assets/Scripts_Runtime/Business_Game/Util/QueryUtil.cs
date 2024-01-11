using UnityEngine;

namespace Surge.Business.Game {

    public static class QueryUtil {

        public static bool TryGetNearestTarget(GameBusinessContext ctx, AllyStatus allyStatus, Vector2 curPos, float range, out EntityType entityType, out int entityID, out Vector2 targetPos) {
            bool hasTarget = false;
            entityType = EntityType.None;
            entityID = 0;
            targetPos = Vector2.zero;
            float nearestDist = float.MaxValue;
            var role = ctx.Role_GetNeareast(allyStatus, curPos, range);
            if (role != null && !role.isDead) {
                float dist = (role.Pos_GetPos() - curPos).sqrMagnitude;
                if (dist <= nearestDist) {
                    nearestDist = dist;
                    entityType = EntityType.Role;
                    entityID = role.entityID;
                    targetPos = role.Pos_GetPos();
                    hasTarget = true;
                }
            }
            return hasTarget;
        }

    }

}