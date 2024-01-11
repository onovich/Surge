using UnityEngine;

namespace Surge {

    public class RoleAnimComponent {

        SpriteRenderer sr;

        public Sprite[] normal_sprites;
        public int index;

        public float timer;
        public float interval;

        public RoleAnimComponent() {
            interval = 1 / 12f;
        }

        public void Ctor(SpriteRenderer sr, Sprite[] normal_sprites) {
            this.sr = sr;
            this.normal_sprites = normal_sprites;
        }

        public void Tick(float dt) {
            if (normal_sprites == null || normal_sprites.Length == 0) {
                return;
            }
            timer -= dt;
            if (timer <= 0) {
                timer = interval;
                index++;
                index %= normal_sprites.Length;
                sr.sprite = normal_sprites[index];
            }
        }

    }

}