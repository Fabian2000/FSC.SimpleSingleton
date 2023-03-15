using System.Text;

using FSC.Singleton;

namespace Test
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            // Create instances of StringBuilder and TestClass
            StringBuilder sbInstance = new StringBuilder();
            TestClass tcInstance = new TestClass();

            // Register the instances
            bool sbRegistered = SimpleSingleton.RegisterInstance(sbInstance);
            bool tcRegistered = SimpleSingleton.RegisterInstance(tcInstance);

            // Add the text "Hello World" to the StringBuilder instance
            StringBuilder? sb = SimpleSingleton.InstanceOf<StringBuilder>();
            bool sbAppended = sb?.Append("Hello World ") != null;

            // Get the content of the StringBuilder and the number of TestClass
            string sbContent = sb?.ToString() ?? string.Empty;
            int tcNumber = SimpleSingleton.InstanceOf<TestClass>()?.Number ?? 0;

            // Output the content and number
            string output = $"{sbContent}{tcNumber}";
            Console.WriteLine(output);

            // Unregister the instances
            bool sbUnregistered = SimpleSingleton.UnregisterInstanceOf<StringBuilder>();
            bool tcUnregistered = SimpleSingleton.UnregisterInstanceOf<TestClass>();

            // Output success or failure messages
            Console.WriteLine(sbRegistered && tcRegistered ? "Instances successfully registered" : "Error registering instances");
            Console.WriteLine(sbAppended ? "Text successfully appended" : "Error appending text");
            Console.WriteLine(sbUnregistered && tcUnregistered ? "Instances successfully unregistered" : "Error unregistering instances");
        }
    }

    internal class TestClass
    {
        internal int Number => 24;
    }
}
