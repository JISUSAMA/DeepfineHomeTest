using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Test03
{
    public class AssetLoader : MonoBehaviour
    {
        [field: SerializeField]
        public LoaderModule LoaderModule { get; set; }
        [field: SerializeField] List<string> selectedAssetNames;

        [Obsolete]
        private void Start()
        {
            selectedAssetNames = GetObjFiles("Models");
            Load(selectedAssetNames);
        }

        private List<string> GetObjFiles(string directory)
        {
            UnityEngine.Object[] objectsInFolder = Resources.LoadAll(directory, typeof(GameObject));
            foreach (UnityEngine.Object obj in objectsInFolder)
            {
                string objectName = obj.name;
                selectedAssetNames.Add(objectName);
            }
            return selectedAssetNames;
        }

        [Obsolete]
        public async void Load(List<string> assetNames)
        {

            List<Task<GameObject>> AddTasks = new List<Task<GameObject>>();
            foreach (string assetName in assetNames)
            {
                AddTasks.Add(LoaderModule.LoadAssetAsync(assetName));
            }
            List<Task<GameObject>> TaskList = AddTasks.ToList();
            while (TaskList.Any())
            {
                Task<GameObject> finishedTask = await Task.WhenAny(TaskList);
                TaskList.Remove(finishedTask);
            }

        }
    }
}
