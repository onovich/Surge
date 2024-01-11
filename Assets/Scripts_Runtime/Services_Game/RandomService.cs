using System;
using UnityEngine;
using RD = System.Random;

namespace Surge {

    public class RandomService {

        RD random;
        public int seed;
        public int seedTimes;

        public RandomService(int seed, int seedTimes) {
            this.seed = seed;
            random = new RD(seed);
            for (int i = 0; i < seedTimes; i++) {
                random.Next();
            }
        }

        public int Next(int min, int max) {
            return random.Next(min, max);
        }

        public Quaternion Rotation() {
            return Quaternion.Euler(0, 0, (float)random.NextDouble() * 360);
        }

    }

}