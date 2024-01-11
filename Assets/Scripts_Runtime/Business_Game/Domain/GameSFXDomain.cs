using UnityEngine;

namespace Surge.Business.Game {

    public static class GameSFXDomain {

        public static void Bullet_Hit(GameBusinessContext ctx, Vector2 hitPos, AudioClip clip) {
            // TODO
            // var owner = ctx.Role_GetOwner();
            // var setting = ctx.settingEntity;
            // SoundCore.PlayBulletHit(ctx.soundCoreContext, clip, owner.Pos_GetPos(), hitPos, SoundConst.THRESHOLD_DIS, setting.sfxVolume);
        }

    }

}