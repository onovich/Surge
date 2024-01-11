using System;

namespace Surge {

    public class PoolService {

        Pool<RoleEntity> rolePool;
        Pool<BulletEntity> bulletPool;
        Pool<SkillSubEntity> skillSubPool;
        Pool<BuffSubEntity> buffPool;

        public PoolService(Func<RoleEntity> roleCtorHandle, Func<BulletEntity> bulletCtorHandle, Func<SkillSubEntity> skillSubCtorHandle, Func<BuffSubEntity> buffCtorHandle) {
            rolePool = new Pool<RoleEntity>(300, roleCtorHandle);
            bulletPool = new Pool<BulletEntity>(300, bulletCtorHandle);
            skillSubPool = new Pool<SkillSubEntity>(500, skillSubCtorHandle);
            buffPool = new Pool<BuffSubEntity>(500, buffCtorHandle);
        }

        public RoleEntity Role_Take() {
            return rolePool.Take();
        }

        public void Role_Return(RoleEntity item) {
            rolePool.Return(item);
        }

        public BulletEntity Bullet_Take() {
            return bulletPool.Take();
        }

        public void Bullet_Return(BulletEntity item) {
            bulletPool.Return(item);
        }

        public SkillSubEntity SkillSub_Take() {
            return skillSubPool.Take();
        }

        public void SkillSub_Return(SkillSubEntity item) {
            skillSubPool.Return(item);
        }

        public BuffSubEntity Buff_Take() {
            return buffPool.Take();
        }

        public void Buff_Return(BuffSubEntity item) {
            buffPool.Return(item);
        }

    }

}