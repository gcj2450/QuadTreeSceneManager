using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class MapDataStreamer : MonoBehaviour
{
    public string mapData;
    public GameObject environment;
    private List<GameObject> loadedGameObjects = new List<GameObject>();

    public MapDataStreamer(string mapDataAsset)
    {
        mapData = mapDataAsset;
        environment = new GameObject("environment");
        InstantiateAsync();
    }

    private void InstantiateAsync()
    {
        Addressables.LoadAssetAsync<MapData>(mapData).Completed += ((op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                foreach (MapObject a in op.Result.mapObjects)
                {
                    if (AddressableResourceExists(a.objectName))
                    {
                        Addressables.InstantiateAsync(a.objectName).Completed += ((opp) => 
                        {
                            if (opp.Status==AsyncOperationStatus.Succeeded)
                            {
                                opp.Result.transform.position = a.objectPosition;
                                opp.Result.transform.rotation = a.objectRotation;
                                opp.Result.name = a.objectName;
                                opp.Result.isStatic = true;

                                if (environment!=null)
                                {
                                    opp.Result.transform.parent = environment.transform;
                                    loadedGameObjects.Add(opp.Result);
                                }
                                else
                                {
                                    Addressables.ReleaseInstance(opp.Result);
                                }
                            }
                        });
                    }
                }
            }
        });
    }

    public static bool AddressableResourceExists(string key)
    {
        foreach (IResourceLocator item in Addressables.ResourceLocators)
        {
            IList<IResourceLocation> locs;
            if (item.Locate(key,typeof(GameObject),out locs))
            {
                return true;
            }
        }
        return false;
    }

    public void Destroy()
    {
        foreach (GameObject a in loadedGameObjects)
        {
            if (a!=environment)
                Addressables.ReleaseInstance(a);
        }
        GameObject.Destroy(environment);
    }

}
