using System;
using UnityEngine;

namespace Surge {

    public struct EffectorModel {

        public GameObject hitVFXPrefab;
        public float hitVFXDuration;

        public AudioClip hitSFX;

        public bool hasHitAttachBuff;
        public bool allowAttachBuilding;
        public int hitAttachBuffTypeID;

        public bool hasImpact;
        public float impactRange;
        public int impactAtk;
        public GameObject impactRangeVFXPrefab;
        public float impactRangeVFXDuration;

        public bool hasImpactAttachBuff;
        public int impactAttachBuffTypeID;

        public void FromTM(in EffectorTM tm) {

            hitVFXPrefab = tm.hitVFXPrefab;
            hitVFXDuration = tm.hitVFXDuration;

            hitSFX = tm.hitSFX;

            hasHitAttachBuff = tm.hasHitAttachBuff;
            allowAttachBuilding = tm.allowAttachBuilding;
            hitAttachBuffTypeID = tm.hitAttachBuffTypeID;

            hasImpact = tm.hasImpact;
            impactRange = tm.impactRange;
            impactAtk = tm.impactAtk;
            impactRangeVFXPrefab = tm.impactRangeVFXPrefab;
            impactRangeVFXDuration = tm.impactRangeVFXDuration;

            hasImpactAttachBuff = tm.hasImpactAttachBuff;
            impactAttachBuffTypeID = tm.impactAttachBuffTypeID;
        }

    }

}