using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Test01
{
    public class LoaderModule : MonoBehaviour
    {
        public Action<GameObject> OnLoadCompleted;
        public void LoadAsset(string assetName)
        {
            string assetFile = Path.GetFileNameWithoutExtension(assetName);
            GameObject modelLoad = Resources.Load<GameObject>("Models/" + assetFile);
            Instantiate(modelLoad, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

}
