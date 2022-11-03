using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

namespace WCC.QuadTree
{
    public class Demo : MonoBehaviour
    {
        public Bounds bounds;
        private Tree tree;
        public GameObject root;
        [SerializeField] float viewRatio = 1;

        // Start is called before the first frame update
        void Start()
        {
            tree = new Tree(bounds);

            Transform[] trans = GetSonChildren(root);

            for (int i = 0; i < trans.Length; i++)
            {
                string path = "Assets/Prefabs/Map/" + trans[i].name + ".prefab";
                PrefabUtility.SaveAsPrefabAsset(trans[i].gameObject, path);

                ObjData objData = new ObjData(path, trans[i].transform.localPosition, trans[i].transform.localRotation, trans[i].transform.localScale, Vector3.one);
                objData.uid = i;
                tree.InsertObjData(objData);
            }
        }

        // Update is called once per frame
        void Update()
        {
            tree.viewRatio = viewRatio;
            tree.Inside(Camera.main);
        }

        //public void RecordObjects(MapData mapData, GameObject parentMap)
        //{
        //    Transform[] objects = root.transform.GetComponentsInChildren<Transform>();
        //    mapData.mapObjects.Clear();
        //    foreach (Transform item in objects)
        //    {
        //        if (item.name == parentMap.name)
        //            continue;
        //        //只添加第一级子物体
        //        if (parentMap.transform == item.transform.parent)
        //        {
        //            MapObject temp = new MapObject(item);
        //            mapData.AddObject(temp);
                    
        //        }

        //    }

        //}


        public Transform[] GetSonChildren(GameObject go)
        {
            Transform[] childs = go.transform.GetComponentsInChildren<Transform>();
            List<Transform> realChildren = new List<Transform>();
            for (int i = 0; i < childs.Length; i++)
            {
                if (childs[i].transform.parent == go.transform)
                {
                    realChildren.Add(childs[i]);
                }
            }
            return realChildren.ToArray();
        }


        private void OnDrawGizmos()
        {
            if (tree != null)
            {
                tree.DrawBound();
            }
            else
            {
                Gizmos.DrawWireCube(bounds.center, bounds.size);
            }
        }
    }

}