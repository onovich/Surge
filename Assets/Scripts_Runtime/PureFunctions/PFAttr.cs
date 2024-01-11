using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Surge {

    public static class PFAttr {

        public static float GetRadius(float radius, float nightReducedRange, bool isNight) {
            if (isNight) {
                if (radius <= 1.5f) {
                    return radius;
                }
                return Mathf.Max(radius - nightReducedRange, 1.5f);
            } else {
                return radius;
            }
        }

        public static int HurtHP(int hp, int casterAtk, int victimDef) {
            return hp - Math.Clamp(casterAtk - victimDef, 0, hp);
        }

        public static int HealHP(int hp, int hpMax, int restorePower) {
            return Math.Clamp(hp + restorePower, 0, hpMax);
        }

        public static bool IsInRadius(in Vector2 posA, in Vector2 posB, float radius) {
            return (posA - posB).sqrMagnitude <= (radius * radius);
        }

    }
}