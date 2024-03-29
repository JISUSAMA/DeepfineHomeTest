using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Test03
{
    public class AssetLoader : MonoBehaviour
    {
        [field: SerializeField]
        public LoaderModule LoaderModule { get; set; }
        [field: SerializeField] List<string> selectedAssetNames;
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
        public async void Load(List<string> assetNames)
        {
            foreach (string assetName in assetNames)
            {
                GameObject loadedAsset = await LoaderModule.LoadAssetAsync(assetName);
                loadedAsset.transform.SetParent(transform);
            };
        }
    }
}
