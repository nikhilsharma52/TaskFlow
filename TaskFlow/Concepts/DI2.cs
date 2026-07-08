namespace TaskFlow.Concepts
{

    public class Hammer2
    {
        public void Use()
        {
            Console.WriteLine("Hammering Nails!");
        }
    }

    public class Saw2
    {
        public void Use()
        {
            Console.WriteLine("Sawing Wood!");
        }
    }

    public class Builder2
    {
        public Hammer2 Hammer2 { get; set; }
        public Saw2 Saw2 { get; set; }



        public void BuildHosue()
        {
            Hammer2.Use();
            Saw2.Use();
            Console.WriteLine("House Built");
        }
    }

    public class DI2
    {
        static void Main(string[] args)
        {
            // Create depencencies outisde
            Hammer2 hammer = new Hammer2();
            Saw2 saw = new Saw2();
            Builder2 builder = new Builder2();

            // Inject dependencies via Setter
            builder.Hammer2 = hammer;
            builder.Saw2 = saw;

            builder.BuildHosue();
            Console.ReadLine();
        }
    }
}
