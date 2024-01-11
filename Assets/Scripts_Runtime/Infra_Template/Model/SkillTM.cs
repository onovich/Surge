using System;
using UnityEngine;

namespace Surge {

    [Serializable]
    public struct SkillTM {

        public int typeID;
        public string typeName;
        public string desc;

        public float cdMax;

        public bool isAutoCast;
        public float preCastSec;
        public float castingMaintainSec;
        public float castingIntervalSec;
        public float endCastSec;

        public bool hasCastBullet;
        public int castBulletTypeID;

        public bool hasDestroySelf;

        // Render
        public Sprite icon;

    }

}