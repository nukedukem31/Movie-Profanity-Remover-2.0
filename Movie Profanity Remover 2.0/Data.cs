using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Movie_Profanity_Remover_2
{
    public static class Data
    {
        public static void Save(string path, object obj)
        {
            string data = "";

            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, obj);
                data = Convert.ToBase64String(ms.ToArray());
            }

            using (FileStream fs = new FileStream(path, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fs))
                sw.WriteLine(data);
        }

        public static object Load(string path)
        {
            if (File.Exists(path))
            {
                string data = "";

                using (FileStream fs = new FileStream(path, FileMode.Open))
                using (StreamReader sr = new StreamReader(fs))
                    data = sr.ReadToEnd();

                byte[] bytes = Convert.FromBase64String(data);

                using (MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length))
                {
                    ms.Write(bytes, 0, bytes.Length);
                    ms.Position = 0;
                    return new BinaryFormatter().Deserialize(ms);
                }
            }

            return null;
        }
    }
}
