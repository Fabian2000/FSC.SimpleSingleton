# FSC.SimpleSingleton
SimpleSingleton is a thread-safe singleton pattern implementation in C# that allows registration and retrieval of singleton instances of any type. It ensures that only one instance of a particular type can be created and accessed at a time, providing a globally accessible point of access for these instances.

## Explanations:
Gets the singleton instance of a type, if it exists.

- `SimpleSingleton.InstanceOf<T>() : T (Instance)`
  
Determines whether a singleton instance of a type exists.

- `SimpleSingleton.HasInstanceOf<T>() : bool`
  
Registers a singleton instance of a type.

- `SimpleSingleton.RegisterInstance<T>(T instance) : bool`
  
Unregisters a singleton instance of a type.

- `SimpleSingleton.UnregisterInstanceOf<T>() : bool`
  
Returns a list of all instances registered in the SimpleSingleton.

- `SimpleSingleton.GetAllInstances() : List<object>`

## Code example:
```cs
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
```
