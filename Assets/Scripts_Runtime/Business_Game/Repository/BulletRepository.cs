using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surge.Business.Game {

    public class BulletRepository {

        Dictionary<int, BulletEntity> all;

        BulletEntity[] temp;

        public BulletRepository() {
            all = new Dictionary<int, BulletEntity>();
            temp = new BulletEntity[1000];
        }

        public void Bullet_Add(BulletEntity bullet) {
            all.Add(bullet.entityID, bullet);
        }

        public void Bullet_ForEach(Action<BulletEntity> action) {
            foreach (var bullet in all.Values) {
                action(bullet);
            }
        }

        public int Bullet_TakeAll(out BulletEntity[] bullets) {
            int count = all.Count;
            if (count > temp.Length) {
                temp = new BulletEntity[(int)(count * 1.5f)];
            }
            all.Values.CopyTo(temp, 0);
            bullets = temp;
            return count;
        }

        public void Bullet_Remove(BulletEntity bullet) {
            all.Remove(bullet.entityID);
        }

        public void Clear() {
            all.Clear();
        }

    }

}