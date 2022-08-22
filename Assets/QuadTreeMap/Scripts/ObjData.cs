using System.Collections.Generic;
using UnityEngine;

namespace WCC.QuadTree
{
    [System.Serializable]
    public class ObjData
    {
        public int uid; //独一无二的id Game用

        /// <summary>
        /// //prefab路径
        /// </summary>
        [SerializeField]
        public string resPath;
        /// <summary>
        /// //位置
        /// </summary>
        [SerializeField]
        public Vector3 pos;
        /// <summary>
        /// //旋转
        /// </summary>
        [SerializeField]
        public Quaternion rot;
        /// <summary>
        /// //缩放
        /// </summary>
        [SerializeField]
        public Vector3 scale;
        /// <summary>
        /// //模型尺寸
        /// </summary>
        [SerializeField]
        public Vector3 size;
        public ObjData(string resPath, Vector3 position, Quaternion rotation, Vector3 localScale, Vector3 modelSize)
        {
            this.resPath = resPath;
            this.pos = position;
            this.rot = rotation;
            this.scale = localScale;
            this.size = modelSize;
        }

        public Bounds GetObjBounds()
        {
            return new Bounds(pos, new Vector3(scale.x * size.x, scale.y * size.y, scale.z * size.z));
        }
    }

    [System.Serializable]
    public class ObjDataContainer
    {
        [SerializeField]
        public ObjData[] objDatas;
    }
}