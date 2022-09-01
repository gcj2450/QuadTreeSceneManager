using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ��װAddressables �������window->Address Management->Addressabels->groups�����ʲ�
/// Ȼ��ѡ��Ԥ���壬��ѡAddressableѡ��
/// </summary>

[CustomEditor(typeof(MapData)),CanEditMultipleObjects]
public class MapDataInspector : Editor
{
    public GameObject parentMap;
    MapData mapData;

    //private void OnEnable()
    //{
    //    mapData = (MapData)target;

    //}

    public override void OnInspectorGUI()
    {
        parentMap = (GameObject)EditorGUILayout.ObjectField(parentMap, typeof(GameObject), true);

        EditorUtility.SetDirty(target);
        EditorGUILayout.LabelField("Number of object: " + mapData.mapObjects.Count.ToString());

        if (GUILayout.Button("Record Map Objects"))
        {
            foreach (Object item in targets)
            {
                mapData = (MapData)item;
                MapData.RecordObjects(mapData, parentMap);
            }

            //Transform[] objects = parentMap.GetComponentsInChildren<Transform>();
            //mapData.mapObjects.Clear();

            //foreach (Transform item in objects)
            //{
            //    if (item.name == parentMap.name)
            //        continue;

            //    MapObject temp = new MapObject(item);
            //    mapData.AddObject(temp);
            //}
        }

        if (GUILayout.Button("Instantiate Map Objects"))
        {
            foreach (Object a in targets)
            {
                mapData = (MapData)a;

                foreach (MapObject item in mapData.mapObjects)
                {
                    mapData.InstantiateMapObject(item);
                }
            }
            //foreach (MapObject item in mapData.mapObjects)
            //{
            //    mapData.InstantiateMapObject(item);
            //}
        }
    }
}
