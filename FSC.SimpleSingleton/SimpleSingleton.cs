using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace FSC.Singleton
{
    /// <summary>
    /// Provides a simple thread-safe implementation of a singleton pattern.
    /// </summary>
    public static class SimpleSingleton
    {
        /// <summary>
        /// A thread-safe collection to store lazy-initialized instances of objects.
        /// </summary>
        private static ConcurrentBag<Lazy<object>> _instances = new ConcurrentBag<Lazy<object>>();

        /// <summary>
        /// A lock object to synchronize access to the collection of instances.
        /// </summary>
        private static readonly object _lockObject = new object();

        /// <summary>
        /// Gets the singleton instance of a type, if it exists.
        /// </summary>
        /// <typeparam name="T">The type of the object to get.</typeparam>
        /// <returns>The singleton instance of the specified type, or <c>null</c> if it does not exist.</returns>
        public static T? InstanceOf<T>()
        {
            lock (_lockObject)
            {
                var instance = _instances.FirstOrDefault(i => i.Value.GetType() == typeof(T));
                return instance is null ? default(T) : (T)instance.Value;
            }
        }

        /// <summary>
        /// Determines whether a singleton instance of a type exists.
        /// </summary>
        /// <typeparam name="T">The type of the object to check.</typeparam>
        /// <returns><c>true</c> if a singleton instance of the specified type exists; otherwise, <c>false</c>.</returns>
        public static bool HasInstanceOf<T>()
        {
            T? instance = InstanceOf<T>();
            return instance is not null;
        }

        /// <summary>
        /// Registers a singleton instance of a type.
        /// </summary>
        /// <typeparam name="T">The type of the object to register.</typeparam>
        /// <param name="instance">The singleton instance to register.</param>
        /// <returns><c>true</c> if the instance was successfully registered; otherwise, <c>false</c>.</returns>
        public static bool RegisterInstance<T>(T instance)
        {
            lock (_lockObject)
            {
                if (instance is null || HasInstanceOf<T>())
                {
                    return false;
                }

                _instances.Add(new Lazy<object>(() => instance));
                return true;
            }
        }

        /// <summary>
        /// Unregisters a singleton instance of a type.
        /// </summary>
        /// <typeparam name="T">The type of the object to unregister.</typeparam>
        /// <returns><c>true</c> if the instance was successfully unregistered; otherwise, <c>false</c>.</returns>
        public static bool UnregisterInstanceOf<T>()
        {
            int initialCount;
            lock (_lockObject)
            {
                initialCount = _instances.Count;
                _instances = new ConcurrentBag<Lazy<object>>(_instances.Where(x => x.Value.GetType() != typeof(T)));
            }
            return initialCount != _instances.Count;
        }

        /// <summary>
        /// Returns a list of all instances registered in the SimpleSingleton.
        /// </summary>
        /// <returns>A list of all instances.</returns>
        public static List<object> GetAllInstances()
        {
            return _instances.Select(x => x.Value).ToList();
        }
    }
}