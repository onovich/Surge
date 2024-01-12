using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surge.Business.Game {

    public class GameBulletFSMController : MonoBehaviour {
        public static void FixedTick(GameBusinessContext ctx, BulletEntity bullet, float fixdt) {
            GameBulletDomain.BulletFly(ctx, bullet, fixdt);
        }
    }

}