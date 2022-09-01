using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    List<Vector3> mapDatas;
    public float xStart, yStart, xEnd, yEnd, step;
    public float loadTolerance;
    List<Vector3> loadedMapDatas;
    Dictionary<Vector3, MapDataStreamer> mapDataStreamers = new Dictionary<Vector3, MapDataStreamer>();

    // Start is called before the first frame update
    void Start()
    {
        mapDatas = new List<Vector3>();
        loadedMapDatas = new List<Vector3>();

        for (float x = xStart; x <=xEnd ; x+=step)
        {
            for (float y = yStart; y <= yEnd; y++)
            {
                mapDatas.Add(new Vector3(x, 0, y));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Vector3 a in mapDatas)
        {
            if (IsVector3InArea(transform.position,a,loadTolerance)&&!loadedMapDatas.Contains(a))
            {
                loadedMapDatas.Add(a);
                mapDataStreamers.Add(a, new MapDataStreamer("Asset/Map Data/MapX" + a.x + "Y" + a.z + ".asset"));
            }

            if (!IsVector3InArea(transform.position, a, loadTolerance) && loadedMapDatas.Contains(a))
            {
                mapDataStreamers[a].Destroy();
                loadedMapDatas.Remove(a);
                mapDataStreamers.Remove(a);
            }
        }
    }

    public bool IsVector3InArea(Vector3 refrence, Vector3 lowerBound,Vector3 upperBound)
    {
        return (refrence.x >= lowerBound.x && refrence.x <= upperBound.x && refrence.z >= lowerBound.z && refrence.z <= upperBound.z);
    }

    public bool IsVector3InArea(Vector3 refrence, Vector3 point, float range)
    {
        return Vector3.Distance(refrence,point)<range;
    }
}
