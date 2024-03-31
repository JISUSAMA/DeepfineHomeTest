using System;
using System.IO;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Test02
{
    public class LoaderModule : MonoBehaviour
    {
        public Action<GameObject> OnLoadCompleted;
        [field: SerializeField] GameObject loadAsset;
        public void LoadAsset(string assetName)
        {
            string assetFile = Path.GetFileNameWithoutExtension(assetName);
            ResourceRequest request = Resources.LoadAsync<GameObject>("Models/" + assetFile);
            loadAsset = request.asset as GameObject;
        }
        public async Task<GameObject> LoadAssetAsync(string assetName)
        {
            LoadAsset(assetName);
            await Task.Yield();
            if (loadAsset != null)
            {
               GameObject result = Instantiate(loadAsset, transform.position, Quaternion.identity);
                return result;
            }
            else
            {
                Debug.LogError("Fail Load " + assetName);
                return null;
            }
        }
    }

}
