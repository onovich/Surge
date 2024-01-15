using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Surge {

    [Serializable]
    public class WaveTM {

        public int typeID;
        public float delayTimer;

        public int[] monsterTypeIDs;
        public float[] monsterSpawnDelayTimers;
        public Vector2[] monsterSpawnPositions;
        public float[] monsterSpawnDirections;

    }

}