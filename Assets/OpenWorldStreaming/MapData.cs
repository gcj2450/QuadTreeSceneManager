using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[CreateAssetMenu(fileName ="New Map Data",menuName ="Map/Map Data"),Serializable]
public class MapData : ScriptableObject
{
    public List<MapObject> mapObjects;

    private void Awake()
    {
        if (mapObjects==null)
        {
            mapObjects = new List<MapObject>();
        }
    }

    public void AddObject(MapObject _mapObject)
    {
        mapObjects.Add(_mapObject);
    }

    public void InstantiateMapObjects()
    {
        foreach (MapObject item in mapObjects)
        {
            InstantiateMapObject(item);
        }
    }

    public GameObject InstantiateMapObject(MapObject mapObject)
    {
#if UNITY_EDITOR
        GameObject temp = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Map/" + mapObject.objectName + ".prefab", typeof(GameObject));
        if (temp == null)
        {
            Debug.Log("temp==null");
            return null;
        }
        temp = Instantiate(temp);
        temp.transform.position = mapObject.objectPosition;
        temp.transform.rotation = mapObject.objectRotation;
        temp.name = mapObject.objectName;

        return temp;
#endif
        return null;
    }

    public static void RecordObjects(MapData mapData, GameObject parentMap)
    {
        Transform[] objects = parentMap.GetComponentsInChildren<Transform>();
        mapData.mapObjects.Clear();
        foreach (Transform item in objects)
        {
            if (item.name == parentMap.name)
                continue;
            MapObject temp = new MapObject(item);
            mapData.AddObject(temp);
        }
    }

}
