using System;
using UnityEngine;

namespace Surge {

    [Serializable]
    public struct BulletTM {

        public int typeID;
        public string typeName;
        public string desc;

        public BulletFlyType flyType;
        public float searchRangeIfTrack;
        public float flySpeed;
        public float radius;

        public int atk;

        public int crossTimes;
        public float lifeSec;

        public Sprite spr;

        // Hit Effector
        public EffectorTM hitEffector;

    }

}