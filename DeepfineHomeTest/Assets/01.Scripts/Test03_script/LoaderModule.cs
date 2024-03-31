using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
namespace Test03
{
    public class LoaderModule : MonoBehaviour
    {
        private GameObject loadAsset;

        public void LoadAsset(string assetName)
        {
            string assetFile = Path.GetFileNameWithoutExtension(assetName);
            ResourceRequest request = Resources.LoadAsync<GameObject>("Models/" + assetFile);
            loadAsset = request.asset as GameObject;
        }
        public async Task<GameObject> LoadAssetAsync(string assetName)
        {
            LoadAsset(assetName);
            GameObject asset = loadAsset;
            await Task.Yield();

            // int RandomT = UnityEngine.Random.RandomRange(0, 3) * 1000;
            // await Task.Delay(RandomT);
            // Debug.Log($"랜덤 시간 : {RandomT}  에셋 이름 {assetName} 불러오는 asset에셋 {asset}");

            if (loadAsset != null)
            {
                GameObject result = Instantiate(asset, transform.position, Quaternion.identity);
                result.transform.SetParent(transform);
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
