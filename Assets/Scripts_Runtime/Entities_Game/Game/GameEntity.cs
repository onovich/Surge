using UnityEngine;

namespace Surge {

    public class GameEntity {

        public int gameTypeID;
        public RandomService random;

        public float intervalTime;
        public float gameTimeSpeedRate;
        public float[] gameTimeSpeedRates;
        public int gameTimeSpeedRateIndex;

        // Stage
        public BattleStage battleStage;
        public BattleStage lastBattleStage;
        public float focusBoss_timer;
        public Vector2 focusBoss_pos;
        public bool focusBoss_isEntering;

        public GameEntity() {

            intervalTime = 0.01f;
            gameTimeSpeedRate = 1f;
            gameTimeSpeedRates = new float[] { 0.6f, 1f, 1.5f, 2f, 10f };

        }

        public void Stage_EnterInBattle() {
            lastBattleStage = battleStage;
            battleStage = BattleStage.InBattle;
            Time.timeScale = 1;
        }

        public void Stage_EnterFocusBoss(Vector2 pos) {
            lastBattleStage = battleStage;
            battleStage = BattleStage.FocusBoss;
            focusBoss_isEntering = true;
            focusBoss_timer = 1.5f;
            focusBoss_pos = pos;
        }

        public void Stage_EnterEnd() {
            lastBattleStage = battleStage;
            battleStage = BattleStage.End;
        }

        public void Stage_EnterPause() {
            lastBattleStage = battleStage;
            battleStage = BattleStage.Pause;
            Time.timeScale = 0;
        }

        public void Stage_ResumeLastStage() {
            if (lastBattleStage == BattleStage.None) {
                lastBattleStage = battleStage;
            }
            if (lastBattleStage == BattleStage.InBattle) {
                Stage_EnterInBattle();
            } else if (lastBattleStage == BattleStage.End) {
                Stage_EnterEnd();
            }
        }

    }

}