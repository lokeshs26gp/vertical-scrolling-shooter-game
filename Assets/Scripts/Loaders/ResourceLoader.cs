using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoadAllAssetsEvent = System.Action<int,int, bool>;
using LoadAssetEvent = System.Action<object, bool>;

    public class ResourceLoader : MonoBehaviour
    {
       
        private Dictionary<string, Object> loadedResourceList;

        public Dictionary<string, Object> LoadedResourceList
        {
            get
            {
                return loadedResourceList;
            }
        }
        public T GetLoadedAsset<T>(string path)where T:Object
        {
            if (loadedResourceList.ContainsKey(path))
                return (T)loadedResourceList[path];

            return null;
        }
        public void LoadAllResources(string[] allPaths,LoadAllAssetsEvent allloadedEvent)
        {
            loadedResourceList = new Dictionary<string, Object>();
            
            StartCoroutine(LoadAllAsync(allPaths, allloadedEvent));

        }

        private IEnumerator LoadAllAsync(string[] allpaths, LoadAllAssetsEvent inEvent)
        {
            int completed = 0;
            WaitForSeconds waitforSecond = new WaitForSeconds(0.1f);
            for (int i = 0; i < allpaths.Length; i++)
            {
                ResourceRequest request = Resources.LoadAsync<Object>(allpaths[i]);
                while(!request.isDone)
                {
                    yield return 0;

                }
                loadedResourceList.Add(allpaths[i], request.asset);
                yield return waitforSecond;

                ++completed;
                
                inEvent(completed, allpaths.Length, false);
            }
            inEvent(completed, allpaths.Length, true);
        }

        private void LoadAsset(string path, LoadAssetEvent inEvent)
        {
        
            if(loadedResourceList.ContainsKey(path))
            {
                inEvent(loadedResourceList[path], true);
            }
            else
                StartCoroutine(LoadAsync(path, inEvent));
        }

        private IEnumerator LoadAsync(string path , LoadAssetEvent inEvent)
        {
            ResourceRequest request = Resources.LoadAsync<Object>(path);
            while(!request.isDone)
            {
                yield return 0;
            }
            
            inEvent(request.asset, true);
        }

        public void UnLoadResources()
        {
            foreach (KeyValuePair<string, Object> resource in loadedResourceList)
                Resources.UnloadAsset(resource.Value);

        }
    }

