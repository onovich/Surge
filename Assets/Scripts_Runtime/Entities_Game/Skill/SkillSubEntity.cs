namespace Surge {

    public class SkillSubEntity {

        public int entityID;
        public int typeID;
        public string typeName;
        public string desc;

        public float cd;
        public float cdMax;

        // Stage
        public bool castByHold;
        public bool isAutoCast;
        public float preCastSecMax;
        public float castingMaintainSecMax;
        public float castingIntervalSecMax;
        public float endCastSecMax;

        // Bullet
        public bool hasCastBullet;
        public int castBulletTypeID;

        public bool hasDestroySelf;

        public SkillSubEntity() { }

        public void Reuse() {

        }

        public void Release() {

        }

        public void CD_ResetTimer() {
            cd = cdMax;
        }

    }

}