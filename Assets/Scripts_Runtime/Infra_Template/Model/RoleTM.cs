using System;
using UnityEngine;

namespace Surge {

    [Serializable]
    public class RoleTM {

        public int typeID;
        public string typeName;

        public int moveSpeed;
        public int hpMax;
        public int atk;
        public int def;

        public int attackRange;
        public int pickRange;

        public int[] skillTypeIds;

        public Sprite spr;

    }

}