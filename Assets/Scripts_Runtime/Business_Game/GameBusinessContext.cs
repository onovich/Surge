using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surge.Business.Game {

    public class GameBusinessContext {

        // Internal
        public GameEntity gameEntity;
        public InputEntity inputEntity;

        public PlayerEntity playerEntity;
        RoleRepository roleRepository;
        BulletRepository bulletRepository;

        public IDRecordService idRecordService;
        public PoolService poolService;

        public HashSet<I32I32I32I32_U128> damageArbitSet;

        // Extra
        public UIAppContext uiAppContext;
        public AssetsInfraContext assetsInfraContext;
        public TemplateInfraContext templateInfraContext;
        public CameraCoreContext cameraCoreContext;

        // Temp
        public Collider2D[] overlapTemp;

        public GameBusinessContext() {
            gameEntity = new GameEntity();
            inputEntity = new InputEntity();
            playerEntity = new PlayerEntity();

            roleRepository = new RoleRepository();
            bulletRepository = new BulletRepository();

            idRecordService = new IDRecordService();
            overlapTemp = new Collider2D[1000];
            damageArbitSet = new HashSet<I32I32I32I32_U128>(1000);
        }

        public void Reset() {

            roleRepository.Clear();
            bulletRepository.Clear();

            idRecordService.Reset();
            damageArbitSet.Clear();
        }

        // Player
        public void Player_Set(PlayerEntity playerEntity) {
            this.playerEntity = playerEntity;
        }

        public void Player_TearDown() {
            playerEntity = null;
        }

        // Role
        public RoleEntity Role_GetOwner() {
            return roleRepository.Role_GetOwner();
        }

        public void Role_Add(RoleEntity roleEntity) {
            roleRepository.Role_Add(roleEntity);
        }

        public void Role_Remove(RoleEntity roleEntity) {
            roleRepository.Role_Remove(roleEntity);
        }

        public bool Role_TryGet(int entityID, out RoleEntity roleEntity) {
            return roleRepository.Role_TryGet(entityID, out roleEntity);
        }

        public RoleEntity Role_GetNeareast(AllyStatus allyStatus, Vector2 curPos, float range) {
            return roleRepository.Role_GetNeareast(allyStatus, curPos, range);
        }

        public void Role_ForEach(Action<RoleEntity> action) {
            roleRepository.Role_ForEach(action);
        }

        // Bullet
        public void Bullet_Add(BulletEntity bulletEntity) {
            bulletRepository.Bullet_Add(bulletEntity);
        }

        public void Bullet_Remove(BulletEntity bulletEntity) {
            bulletRepository.Bullet_Remove(bulletEntity);
        }

        public bool Bullet_TryGet(int entityID, out BulletEntity bulletEntity) {
            return bulletRepository.Bullet_TryGet(entityID, out bulletEntity);
        }

        // Damage Arbit
        public void DamageArbit_Add(EntityType casterType, int casterEntityID, EntityType victimType, int victimEntityID) {
            var key = new I32I32I32I32_U128((int)casterType, casterEntityID, (int)victimType, victimEntityID);
            damageArbitSet.Add(key);
        }

        public void DamageArbit_Remove(EntityType casterType, int casterEntityID) {
            damageArbitSet.RemoveWhere((key) => {
                return (key.i32_1 == (int)casterType) && (key.i32_2 == casterEntityID);
            });
        }

        public bool DamageArbit_Has(EntityType casterType, int casterEntityID, EntityType victimType, int victimEntityID) {
            var key = new I32I32I32I32_U128((int)casterType, casterEntityID, (int)victimType, victimEntityID);
            return damageArbitSet.Contains(key);
        }

        public void DamageArbit_Clear() {
            damageArbitSet.Clear();
        }

    }

}