using CarPark.Cars;
using System;
using System.Collections.Generic;
using System.IO;

namespace CarPark
{
    class FileManager
    {
        private string Filename { get; set; } = @"..\..\..\..\Save.bin";

        public FileManager(string[] args)
        {
            if (args.Length > 0)
            {
                Filename = args[0];
            }
        }

        public List<Vehicle> ReadFromBinaryFile()
        {
            try
            {
                using (Stream stream = File.Open(Filename, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return (List<Vehicle>)binaryFormatter.Deserialize(stream);
                }
            }
            catch (Exception)
            {
                return new List<Vehicle>();
            }
        }

        public void WriteToBinaryFile<T>(T objectToWrite)
        {
            using (Stream stream = File.Open(Filename, FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }
    }
}
