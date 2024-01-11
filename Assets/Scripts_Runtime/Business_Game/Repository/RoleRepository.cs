using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surge.Business.Game {


    public class RoleRepository {
        Dictionary<int, RoleEntity> all;
        Dictionary<int, RoleEntity> monsters;
        RoleEntity owner;

        RoleEntity[] temp;

        public RoleRepository() {
            all = new Dictionary<int, RoleEntity>();
            monsters = new Dictionary<int, RoleEntity>(1000);
            temp = new RoleEntity[1000];
        }

        public void Role_Add(RoleEntity role) {
            all.Add(role.entityID, role);
            if (role.roleType == RoleType.Monster) {
                monsters.Add(role.entityID, role);
            }
            if (role.roleType == RoleType.Player) {
                owner = role;
            }
        }

        public void Role_ForEach(Action<RoleEntity> action) {
            all.Values.CopyTo(temp, 0);
            foreach (var role in temp) {
                if (role == null) {
                    break;
                }
                action(role);
            }
        }

        public bool Role_TryGet(int entityID, out RoleEntity role) {
            return all.TryGetValue(entityID, out role);
        }

        public RoleEntity Role_GetOwner() {
            return owner;
        }

        public RoleEntity Role_GetNeareast(AllyStatus allyStatus, Vector2 pos, float radius) {
            RoleEntity nearestRole = null;
            float nearestDist = float.MaxValue;
            float radiusSqr = radius * radius;
            foreach (var role in all.Values) {
                if (role.isDead) {
                    continue;
                }
                if (role.allyStatus != allyStatus) {
                    continue;
                }
                float dist = Vector2.SqrMagnitude(role.Pos_GetPos() - pos);
                if (dist <= radiusSqr && dist < nearestDist) {
                    nearestDist = dist;
                    nearestRole = role;
                }
            }
            return nearestRole;
        }

        public IEnumerable<RoleEntity> Monsters_Get() {
            return monsters.Values;
        }

        public bool Role_TryGetAlive(int entityID, out RoleEntity role) {
            bool has = all.TryGetValue(entityID, out role);
            return has && !role.isDead;
        }

        public void Role_Remove(RoleEntity role) {
            all.Remove(role.entityID);
            if (role.roleType == RoleType.Monster) {
                monsters.Remove(role.entityID);
            }
            if (role.roleType == RoleType.Player) {
                owner = null;
            }
        }

        public void Clear() {
            all.Clear();
            monsters.Clear();
            owner = null;
        }

    }

}