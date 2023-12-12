using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EO1BOA_HFT_2023241.Client
{
    class CrudService
    {
        private RestService rest;

        public CrudService(RestService rest)
        {
            this.rest = rest;
        }
        public void Create<T>()
        {
            var properties = typeof(T).GetProperties().Where(x => x.GetAccessors().All(y => !y.IsVirtual) && x.Name != "Id");
            T instance = (T)Activator.CreateInstance(typeof(T));
            foreach (var property in properties)
            {
                Console.Write($"{property.Name} = ");
                string input = Console.ReadLine();
                if (property.PropertyType == typeof(int))
                {
                    property.SetValue(instance, int.Parse(input));
                }
                else if (property.PropertyType == typeof(double))
                {
                    property.SetValue(instance, double.Parse(input));
                }
                else if (property.PropertyType == typeof(bool))
                {
                    property.SetValue(instance, bool.Parse(input));
                }
                else
                {
                    property.SetValue(instance, input);
                }
            }
            rest.Post(instance, typeof(T).Name);
        }
        public void List<T>()//Read
        {
            var properties = typeof(T).GetProperties().Where(x => x.GetAccessors().All(y => !y.IsVirtual));
            var items = rest.Get<T>(typeof(T).Name);

            foreach (var property in properties)
            {
                Console.Write($"{property.Name}\t");
            }
            Console.Write("\n");

            foreach (var item in items)
            {
                foreach (var property in properties)
                {
                    Console.Write($"{property.GetValue(item)}\t");
                }
                Console.Write("\n");
            }

            Console.ReadLine();
        }
        public void Update<T>()
        {
            Console.WriteLine("Enter ID to update:");
            int id = int.Parse(Console.ReadLine());
            var instance = rest.Get<T>(id, typeof(T).Name);
            var properties = typeof(T).GetProperties().Where(p => p.GetAccessors().All(a => !a.IsVirtual) && p.Name != "Id");
            foreach (var property in properties)
            {
                Console.Write($"New {property.Name} [Old: {property.GetValue(instance)}]= ");
                string input = Console.ReadLine();
                if (property.PropertyType == typeof(int))
                {
                    property.SetValue(instance, int.Parse(input));
                }
                else
                {
                    property.SetValue(instance, bool.Parse(input));
                }
            }
            rest.Put(instance, typeof(T).Name);
        }
        public void Delete<T>()
        {
            Console.WriteLine("Enter ID to delete:");
            int id = int.Parse(Console.ReadLine());
            rest.Delete(id, typeof(T).Name);
        }
    }
}
