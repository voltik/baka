namespace Tree2DB
{
    class Program
    {
        static void Main(string[] args)
        {
            switch (args[0])
            {
                case "1":
                    new Convert1().Main(args[1]);
                    break;
                case "2":
                    new Convert2().Main(args[1]);
                    break;
                case "3":
                    new Convert3().Main(args[1]);
                    break;
                case "4":
                    new Convert4().Main(args[1]);
                    break;
                case "5":
                    new Convert5().Main(args[1]);
                    break;
            }
        }
    }
}
