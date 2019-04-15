namespace TareasMirandaXavier
{
    class LogLine
    {
        int counter;
        public LogLine(int counter)
        {
            this.counter = counter;
        }

        public string GetIP()
        {
            return "ip-" + counter;
        }
    }
}
