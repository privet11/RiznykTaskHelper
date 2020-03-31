using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;

namespace RiznykTaskHelper
{
    public static class Serialisation
    {
        public static void Serialise<T>(T obj, string address)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<T>));
            using (FileStream fs = new FileStream(address, FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    List<T> objects = (List<T>)jsonFormatter.ReadObject(fs);
                    objects.Add(obj);
                    fs.SetLength(0);
                    jsonFormatter.WriteObject(fs, objects);
                }
                else
                {
                    List<T> object1 = new List<T>() { obj };
                    jsonFormatter.WriteObject(fs, object1);
                }
            }
        }

        public static void Serialise<T>(List<T> objList, string address)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<T>));
            using (FileStream fs = new FileStream(address, FileMode.OpenOrCreate))
            {
                List<T> objects = (List<T>)jsonFormatter.ReadObject(fs);
                objects = objList;
                fs.SetLength(0);
                jsonFormatter.WriteObject(fs, objects);
            }
        }
        public static List<T> GetList<T>(string address) //Отримання списку об'єктів з файлу
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<T>));
            List<T> objects = new List<T>() { };
            using (FileStream fs = new FileStream(address, FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    objects = (List<T>)jsonFormatter.ReadObject(fs);
                }

            }
            return objects;
        }
      
    }
}
