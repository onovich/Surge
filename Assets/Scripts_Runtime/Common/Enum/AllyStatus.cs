namespace Surge {

    public enum AllyStatus {

        None = 0,
        Justice = 1,
        Evil = 2,
        Nuetral = 3,

    }

    public static class AllyStatusExtension {
        public static AllyStatus GetOpposite(this AllyStatus status) {
            if (status == AllyStatus.Justice) {
                return AllyStatus.Evil;
            } else if (status == AllyStatus.Evil) {
                return AllyStatus.Justice;
            } else {
                return AllyStatus.None;
            }
        }
    }

}