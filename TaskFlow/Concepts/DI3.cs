//namespace TaskFlow.Concepts
//{

//    public interface ITool
//    {
//        void SetHammer(Hammer3 hammer);
//        void SetSaw(Saw3 saw);
//    }

//    public class Hammer3
//    {
//        public void Use()
//        {
//            Console.WriteLine("Hammering Nails!");
//        }
//    }

//    public class Saw3
//    {
//        public void Use()
//        {
//            Console.WriteLine("Sawing Wood!");
//        }
//    }

//    public class Builder3 : ITool
//    {
//        private Hammer3 _hammer;
//        private Saw3  _saw;



//        public void BuildHosue()
//        {
//            _hammer.Use();
//            _saw.Use();
//            Console.WriteLine("House Built");
//        }

//        public void SetHammer(Hammer3 hammer)
//        {
//            _hammer = hammer;
//        }

//        public void SetSaw(Saw3 saw)
//        {
//            _saw = saw;
//        }
//    }

//    public class DI3
//    {
//        static void Main(string[] args)
//        {
//            // Create depencencies outisde
//            Hammer3 hammer = new Hammer3();
//            Saw3 saw = new Saw3();
//            Builder3 builder = new Builder3();

//            // Inject dependencies via Setter
//            builder.SetHammer(hammer);
//            builder.SetSaw(saw);

//            builder.BuildHosue();
//            Console.ReadLine();
//        }
//    }
//}
