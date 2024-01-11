namespace Surge.UI {

    public static class PanelLoginDomain {

        public static void Open(UIAppContext ctx) {

            Panel_Login panel = UIFactory.UniquePanel_Open<Panel_Login>(ctx);
            panel.Ctor();

            panel.OnClickStartGameHandle = () => {
                ctx.evt.Login_OnStartGameClick();
            };

            panel.OnClickExitGameHandle = () => {
                ctx.evt.Login_OnExitGameClick();
            };

        }

        public static void Close(UIAppContext ctx) {
            UIFactory.UniquePanel_Close<Panel_Login>(ctx);
        }

    }

}