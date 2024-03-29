using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
namespace Test02
{
    public class AssetLoader : MonoBehaviour
    {
        [field: SerializeField]
        public LoaderModule LoaderModule { get; set; }

        private void Start()
        {
            string selectedAssetName = EditorUtility.OpenFilePanel("Select obj model", "", "obj");
            Load(selectedAssetName);
        }

        public async void Load(string assetName)
        {
            GameObject loadedAsset = await LoaderModule.LoadAssetAsync(assetName);
            loadedAsset.transform.SetParent(transform);
        }

    }
}