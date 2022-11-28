using KM.Startup.GameData;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace KM.Startup
{
    public static class SaveGameDataUtils
    {
        public static async Task SaveData()
        {
            var data = new SavedGameData()
            {

            };
            var json = JsonUtility.ToJson(data);
            //File.WriteAllText("", json);
        }

        public static async Task LoadData()
        {
            var json = string.Empty;
            var data = JsonUtility.FromJson<SavedGameData>(json);


        }
    }
}