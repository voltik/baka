namespace Tree2DB
{
    class Program
    {
        static void Main(string[] args)
        {
            switch(args[0])
            {
                case "1":
                    new Convert1().Main(args[1]);
                    break;
                case "2":
                    new Convert2().Main(args[1]);
                    break;
            }
        }
    }
}
