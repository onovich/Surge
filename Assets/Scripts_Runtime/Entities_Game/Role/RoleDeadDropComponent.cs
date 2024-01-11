using System;
using System.Collections.Generic;

namespace Surge {

    public class RoleDeadDropComponent {

        public int dropExp;
        public int dropGold;
        public int[] dropStuffTypeIDs;

        public RoleDeadDropComponent() { }

        public void Clear() {
            dropExp = 0;
        }

        public int GetRandomDropStuff(RandomService rd) {
            if (dropStuffTypeIDs == null || dropStuffTypeIDs.Length == 0) {
                return 0;
            }
            int index = rd.Next(0, dropStuffTypeIDs.Length);
            return dropStuffTypeIDs[index];
        }

        public int[] GetDropStuffs() {
            return dropStuffTypeIDs;
        }

    }

}