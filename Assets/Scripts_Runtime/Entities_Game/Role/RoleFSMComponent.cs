using UnityEngine;

namespace Surge {

    public class RoleFSMComponent {

        public RoleFSMStatus status;

        public bool normal_isEntering;

        public bool casting_isEntering;
        public SkillSubEntity casting_skill;
        public SkillCastStage casting_stage;
        public float casting_preCastTimer;
        public float casting_castingTimer;
        public float casting_castingIntervalTimer;
        public float casting_endCastTimer;

        public bool fakeDead_isEntering;
        public float fakeDead_timer;
        public float fakeDead_duration;

        public bool reborn_isEntering;
        public float reborn_timer;
        public float reborn_duration;

        public RoleFSMComponent() { }

        public void EnterNormal() {
            status = RoleFSMStatus.Normal;
            normal_isEntering = true;
        }

        public void EnterCasting(SkillSubEntity skill) {
            status = RoleFSMStatus.Casting;
            casting_isEntering = true;
            casting_skill = skill;
            casting_stage = SkillCastStage.PreCast;
            casting_preCastTimer = skill.preCastSecMax;
            casting_castingTimer = skill.castingMaintainSecMax;
            casting_castingIntervalTimer = skill.castingIntervalSecMax;
            casting_endCastTimer = skill.endCastSecMax;
        }

        public void EnterFakeDead(float duration) {
            status = RoleFSMStatus.FakeDead;
            fakeDead_isEntering = true;
            fakeDead_timer = duration;
            fakeDead_duration = duration;
        }

        public void EnterReborn(float duration) {
            status = RoleFSMStatus.Reborn;
            reborn_isEntering = true;
            reborn_timer = duration;
            reborn_duration = duration;
        }

    }

}