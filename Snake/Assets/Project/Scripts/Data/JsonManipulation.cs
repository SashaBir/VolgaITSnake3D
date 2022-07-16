using System.IO;
using UnityEngine;

namespace Snake.Data
{
    public class JsonManipulation
    {
        private const string _fileName = "Jsons";
        private static readonly string _path;

        static JsonManipulation() => _path = GetPathToDirectory;

        public static void Write<T>(T data, string name)
        {
            string json = JsonUtility.ToJson(data, true);
            string path = GetPath(name);

            File.WriteAllText(path, json);
        }

        public static T Read<T>(string name)
        {
            string path = GetPath(name);
            if (File.Exists(path) == false)
            {
                Write(default(T), name);
                Debug.LogWarning($"File { name } is not exist, but it is created.");
            }

            string json = File.ReadAllText(path);
            
            return JsonUtility.FromJson<T>(json);
        }
        
        private static string GetPathToDirectory => Path.Combine(Application.streamingAssetsPath, _fileName);

        private static string GetPath(string name) => Path.Combine(_path, name + ".json");
        //private static string GetPath(string name) => Path.Combine(_path, Path.ChangeExtension(name, "json"));
    }
}