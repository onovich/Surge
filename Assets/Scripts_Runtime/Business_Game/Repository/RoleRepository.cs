using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surge.Business.Game {


    public class RoleRepository {
        Dictionary<int, RoleEntity> all;
        Dictionary<int, RoleEntity> monsters;
        Dictionary<I32I32_U64, List<RoleEntity>> posIntDict;
        RoleEntity owner;

        RoleEntity[] temp;

        public RoleRepository() {
            all = new Dictionary<int, RoleEntity>();
            monsters = new Dictionary<int, RoleEntity>(1000);
            temp = new RoleEntity[1000];
            posIntDict = new Dictionary<I32I32_U64, List<RoleEntity>>();
        }

        public void Role_Add(RoleEntity role) {
            all.Add(role.entityID, role);
            if (role.roleType == RoleType.Monster) {
                monsters.Add(role.entityID, role);
            }
            if (role.roleType == RoleType.Player) {
                owner = role;
            }
            var posInt = role.Pos_GetPosInt();
            var posKey = new I32I32_U64(posInt.x, posInt.y);
            posIntDict.GetOrAdd(posKey, () => new List<RoleEntity>()).Add(role);
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

        public void UpdatePosDict(RoleEntity role) {
            if (role.IsDead()) {
                return;
            }
            var lastPos = role.Pos_GetLastPosInt();
            var newPos = role.Pos_GetPosInt();
            var oldKey = new I32I32_U64(lastPos.x, lastPos.y);
            var newKey = new I32I32_U64(newPos.x, newPos.y);
            if (posIntDict.ContainsKey(oldKey)) {
                posIntDict[oldKey].Remove(role);
            } else {
                SLog.LogError("Dont's has old key in PosDict, role entityID = " + role.entityID + "; oldKey = " + oldKey);
            }
            if (posIntDict.ContainsKey(newKey)) {
                posIntDict[newKey].Add(role);
            } else {
                var list = new List<RoleEntity>();
                list.Add(role);
                posIntDict.Add(newKey, list);
            }
        }

        public bool TryGetByPosInt(Vector2Int pos, out List<RoleEntity> roles) {
            if (!posIntDict.TryGetValue(new I32I32_U64(pos.x, pos.y), out roles)) {
                return false;
            }
            return true;
        }

        public bool IsInRange(int entityID, in Vector2 pos, float range) {
            bool has = Role_TryGetAlive(entityID, out var role);
            if (!has) {
                return false;
            }
            return Vector2.SqrMagnitude(role.Pos_GetPos() - pos) <= range * range;
        }


        public int TakeAll(out RoleEntity[] roles) {
            int count = all.Count;
            if (count > temp.Length) {
                temp = new RoleEntity[(int)(count * 1.5f)];
            }
            all.Values.CopyTo(temp, 0);
            roles = temp;
            return count;
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
                if (role.IsDead()) {
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

        public int TryGetAround(int entityID, AllyStatus allyStatus, Vector2Int centerPos, int radius, int maxCount, out RoleEntity[] roles) {
            int gridCount = PFGrid.RectCycle_Grids(centerPos, radius, out var cells);
            int roleCount = 0;
            for (int i = 0; i < gridCount; i++) {
                var key = new I32I32_U64(cells[i].x, cells[i].y);
                if (!posIntDict.TryGetValue(key, out var list)) {
                    continue;
                }
                list.ForEach((role) => {
                    if (role.IsDead()) {
                        return;
                    }
                    if (role.allyStatus != allyStatus) {
                        return;
                    }
                    if (role.entityID == entityID) {
                        return;
                    }
                    temp[roleCount++] = role;
                    if (roleCount >= maxCount) {
                        return;
                    }
                });
                if (roleCount >= maxCount) {
                    break;
                }
            }
            roles = temp;
            return roleCount;
        }

        public IEnumerable<RoleEntity> Monsters_Get() {
            return monsters.Values;
        }

        public bool Role_TryGetAlive(int entityID, out RoleEntity role) {
            bool has = all.TryGetValue(entityID, out role);
            return has && !role.IsDead();
        }

        public void Role_Remove(RoleEntity role) {
            all.Remove(role.entityID);
            if (role.roleType == RoleType.Monster) {
                monsters.Remove(role.entityID);
            }
            if (role.roleType == RoleType.Player) {
                owner = null;
            }
            var posKey = new I32I32_U64(role.Pos_GetPosInt().x, role.Pos_GetPosInt().y);
            if (posIntDict.ContainsKey(posKey)) {
                posIntDict[posKey].Remove(role);
            }
        }

        public void Clear() {
            all.Clear();
            monsters.Clear();
            owner = null;
            posIntDict.Clear();
        }

    }

}