using System;
using UnityEngine;

namespace Surge {

    [Serializable]
    public struct EffectorTM {

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

    }

}