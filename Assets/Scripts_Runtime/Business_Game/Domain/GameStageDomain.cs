namespace Surge.Business.Game {

    public static class GameStageDomain {

        public static void FocusBoss(GameBusinessContext ctx, RoleEntity boss) {
            var game = ctx.gameEntity;
            game.Stage_EnterFocusBoss(boss.Pos_GetPos());
        }

        public static void ApplyResult(GameBusinessContext ctx) {

            var game = ctx.gameEntity;

            // TODO
            // 失败: 玩家生命用尽
            // 胜利: Boss 全灭

        }

        static void UI_ShowResultWin(GameBusinessContext ctx) {
        }

        static void UI_ShowResultLose(GameBusinessContext ctx) {
        }

    }

}