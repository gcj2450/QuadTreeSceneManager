using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneLoader : MonoBehaviour
{
    List<Vector3> mapDatas;
    public float xStart, yStart, xEnd, yEnd,step;
    public float loadTolerance;
    List<Vector3> loadedMaps;
    Dictionary<Vector3, AsyncOperationHandle<SceneInstance>> mapStreamers = new Dictionary<Vector3, AsyncOperationHandle<SceneInstance>>();


    // Start is called before the first frame update
    void Start()
    {
        mapDatas = new List<Vector3>();
        loadedMaps = new List<Vector3>();

        for (float x= xStart; x <= xEnd; x+=step)
        {
            for (float y = yStart; y <=yEnd; y+=step)
            {
                mapDatas.Add(new Vector3(x, 0, y));
            }
        }

        float[] v = Camera.main.layerCullDistances;
        v[0] = 1300;
        Camera.main.layerCullDistances = v;
        Camera.main.layerCullSpherical = true;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Vector3 a in mapDatas)
        {
            if (IsVector3InArea(transform.position,a,loadTolerance)&&!loadedMaps.Contains(a))
            {
                loadedMaps.Add(a);
                Addressables.LoadSceneAsync("Assets/Scenes/Map/MapX" + a.x + "Y" + a.z + ".unity", UnityEngine.SceneManagement.LoadSceneMode.Additive).Completed += (op => {
                    mapStreamers.Add(a, op);
                });
            }

            if (!IsVector3InArea(transform.position,a,loadTolerance)&&loadedMaps.Contains(a))
            {
                if (mapStreamers.ContainsKey(a)&&mapStreamers[a].PercentComplete==1)
                {
                    Addressables.UnloadSceneAsync(mapStreamers[a]);
                    Addressables.Release(mapStreamers[a]);
                    loadedMaps.Remove(a);
                    mapStreamers.Remove(a);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Resources.UnloadUnusedAssets();
        }
    }

    public bool IsVector3InArea(Vector3 refrence, Vector3 lowerBound, Vector3 upperBound)
    {
        return (refrence.x >= lowerBound.x && refrence.x <= upperBound.x && refrence.z >= lowerBound.z && refrence.z <= upperBound.z);
    }

    public bool IsVector3InArea(Vector3 refrence, Vector3 point, float range)
    {
        return Vector3.Distance(refrence, point) < range;
    }
}
