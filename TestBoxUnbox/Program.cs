using System.Diagnostics;

namespace TestBoxUnbox
{
    public class Program
    {
        static void Main(string[] args)
        {
            int iterations = 1000000;
            List<int> ints = new List<int>();
            List<object> objects = new List<object>();
            Random random = new Random();

            for (int i = 0; i < iterations; i++)
            {
                ints.Add(random.Next());
            }

            // Boxing
            Stopwatch watch = new Stopwatch();
            watch.Start();
            foreach (var i in ints)
            {
                objects.Add(i); //Box int in object
            }
            watch.Stop();
            Console.WriteLine($"Boxing {iterations} times took: {watch.ElapsedMilliseconds} ms ({watch.ElapsedTicks} ticks)");

            // Unboxing
            watch.Restart();
            foreach (var obj in objects)
            {
                int i = (int)obj; //Unbox object to int
            }
            watch.Stop();
            Console.WriteLine($"Unboxing {iterations} times took: {watch.ElapsedMilliseconds} ms ({watch.ElapsedTicks} ticks)");
                           
            watch.Reset();
            object boxed;

            // Boxing
            watch.Start();
            for (int i = 0; i < iterations; i++)
            {
                boxed = i;
            }
            watch.Stop();
            Console.WriteLine($"Boxing {iterations} times took: {watch.ElapsedMilliseconds} ms ({watch.ElapsedTicks} ticks)");

            boxed = 0;

            // Unboxing
            watch.Restart();
            for (int i = 0; i < iterations; i++)
            {
                int unboxed = (int)boxed; // Unboxing
            }
            watch.Stop();
            Console.WriteLine($"Unboxing {iterations} times took: {watch.ElapsedMilliseconds} ms ({watch.ElapsedTicks} ticks)");
        }
    }
}
