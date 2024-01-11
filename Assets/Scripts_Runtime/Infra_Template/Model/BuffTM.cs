using System;

namespace Surge {

    [Serializable]
    public struct BuffTM {

        public int typeID;
        public int level;
        public string name;
        public string desc;

        public float lifeSec;

        public bool allowAttachBuilding;

        public bool hasDot;
        public float dotIntervalSec;
        public int dotAtk;

        public bool hasIce;
        public float iceSlowRate;
        public bool hasIceImpact;
        public int iceImpactNeecCount;
        public int iceImpactAtk;

    }

}