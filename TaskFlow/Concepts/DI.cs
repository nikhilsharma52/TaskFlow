namespace TaskFlow.Concepts
{
    public class Hammer
    {
        public void Use()
        {
            Console.WriteLine("Hammering Nails!");
        }
    }

    public class Saw
    {
        public void Use()
        {
            Console.WriteLine("Sawing Wood!");
        }
    }

    public class Builder
    {
        private Hammer _hammer;
        private Saw _saw;

        // Dependency Injection through constructor
        public Builder(Hammer hammer, Saw saw)
        {
            _hammer = hammer;
            _saw = saw;
        }

        public void BuildHosue()
        {
            _hammer.Use();
            _saw.Use();
            Console.WriteLine("House Built!");
        }
    }

    public class BI
    {
        static void Main(string[] args)
        {
            Hammer hammer = new Hammer();
            Saw saw = new Saw();
            Builder builder = new Builder(hammer, saw);

            builder.BuildHosue();

            Console.ReadLine();
        }
    }
}
