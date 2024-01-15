namespace Surge.Business.Game {

    public static class GameStageDomain {

        // Game
        public static GameEntity Game_Spawn(GameBusinessContext ctx, int typeID) {
            var game = GameFactory.Game_Spawn(ctx.templateInfraContext, typeID);
            Game_SpawnFinish(ctx, game);
            return game;
        }

        public static void Game_SpawnFinish(GameBusinessContext ctx, GameEntity game) {
            ctx.Game_Set(game);
        }

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