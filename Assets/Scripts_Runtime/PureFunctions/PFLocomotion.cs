using System.Runtime.CompilerServices;
using UnityEngine;

namespace Surge {

    public static class PFLocomotion {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Move(Vector2 pos, Vector2 moveAxis, float moveSpeed, float dt) {
            return pos + moveAxis.normalized * moveSpeed * dt;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 GetVelocity(Vector2 moveAxis, float moveSpeed) {
            moveAxis = moveAxis.normalized * moveSpeed;
            return moveAxis;
        }

        public static bool IsInRange(Vector2 pos, Vector2 targetPos, float range) {
            return (pos - targetPos).sqrMagnitude <= range * range;
        }

    }
}