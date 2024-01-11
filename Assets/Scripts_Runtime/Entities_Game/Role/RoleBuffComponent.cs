using System;
using System.Collections.Generic;

namespace Surge {

    public class RoleBuffComponent {

        List<BuffSubEntity> all;
        Dictionary<int, BuffSubEntity> dict;

        public float moveSpeedRate = 1f;
        public float moveSpeedAdd = 0f;

        BuffSubEntity[] temp;

        public RoleBuffComponent() {
            all = new List<BuffSubEntity>();
            dict = new Dictionary<int, BuffSubEntity>();
            temp = new BuffSubEntity[200];
        }

        public void Clear() {
            all.Clear();
        }

        public void TryAddOrUpdate(BuffSubEntity buff) {
            var key = buff.typeID;
            if (dict.TryGetValue(key, out var exist)) {
                exist.lifeTimer = buff.lifeTimer;
            } else {
                dict.Add(key, buff);
                all.Add(buff);
                if (buff.hasIce) {
                    moveSpeedRate *= buff.iceSlowRate;
                }
            }
        }

        public void Remove(BuffSubEntity buff) {
            all.Remove(buff);
            if (buff.hasIce) {
                moveSpeedRate /= buff.iceSlowRate;
            }
        }

        public BuffSubEntity Get(int typeID) {
            return all.Find(buff => buff.typeID == typeID);
        }

        public void Foreach(Action<BuffSubEntity> action) {
            all.ForEach(action);
        }

        public int TakeAll(out BuffSubEntity[] buffs) {
            int count = all.Count;
            if (temp.Length < count) {
                temp = new BuffSubEntity[count];
            }
            all.CopyTo(temp);
            buffs = temp;
            return count;
        }

    }

}