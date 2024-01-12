using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surge {

    public class BulletEntity : MonoBehaviour {

        public readonly EntityType entityType = EntityType.Bullet;

        public int entityID;
        public int typeID;
        public string typeName;
        public AllyStatus allyStatus;
        public BulletFlyType flyType;

        public bool IsDead => crossTimes <= 0 || lifeSec <= 0;

        public bool hasTarget;
        public EntityType casterEntityType;
        public int casterEntityID;
        public EntityType targetEntityType;
        public int targetEntityID;

        public float flySpeed;
        public float radius;
        public int atk;
        public int crossTimes;
        public float lifeSec;
        public float searchRangeIfTrack;
        public float trackFlyPreSec;

        public Vector2 fromPos;
        public Vector2 pos;
        public Vector2 targetPos;
        public Vector2 dir;

        public EffectorModel hitEffector;

        [SerializeField] SpriteRenderer sr;

        public void Ctor() {
            gameObject.SetActive(false);
        }

        public void Reuse() {
            gameObject.SetActive(true);
        }

        public void Release() {
            gameObject.SetActive(false);
        }

        public void TearDown() {
            Release();
            Destroy(gameObject);
        }

        public void Pos_UpdatePos() {
            transform.position = new Vector2(pos.x, pos.y);
            transform.up = dir;
        }

        public Vector2Int Pos_GetPosInt() {
            return new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
        }

        public void Show() {
            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }

        public void SR_SetSprite(Sprite sprite) {
            sr.sprite = sprite;
        }

    }

}