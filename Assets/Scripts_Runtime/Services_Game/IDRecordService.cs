namespace Surge {

    public class IDRecordService {

        public int role;
        public int bullet;
        public int skill;
        public int buff;

        public IDRecordService() { }

        public void Reset() {
            role = 0;
            bullet = 0;
            skill = 0;
            buff = 0;
        }
    }

}