using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WCC.QuadTree
{
    /// <summary>
    /// 树根节点
    /// </summary>
    public class Tree : INode
    {
        /// <summary>
        /// 边界
        /// </summary>
        public Bounds bound { get; set; }
        /// <summary>
        /// 根节点
        /// </summary>
        private Node root;
        /// <summary>
        /// 最大深度
        /// </summary>
        public int maxDepth { get; }
        /// <summary>
        /// 最大子节点数（默认4个）
        /// </summary>
        public int maxChildCount { get; }
        /// <summary>
        /// 视图宽高比
        /// </summary>
        public float viewRatio = 1;

        public Tree(Bounds bound)
        {
            this.bound = bound;
            this.maxDepth = 5;
            this.maxChildCount = 4;

            root = new Node(bound, 0, this);
        }

        /// <summary>
        /// 插入模型数据
        /// </summary>
        /// <param name="obj"></param>
        public void InsertObjData(ObjData obj)
        {
            root.InsertObjData(obj);
        }

        //public void TriggerMove(Camera camera)
        //{
        //    root.TriggerMove(camera);
        //}

        /// <summary>
        /// 是否在视角内
        /// </summary>
        /// <param name="camera"></param>
        public void Inside(Camera camera)
        {
            root.Inside(camera);
        }

        /// <summary>
        /// 视角外
        /// </summary>
        /// <param name="camera"></param>
        public void Outside(Camera camera)
        {
            root.Outside(camera);
        }

        /// <summary>
        /// 绘制边界
        /// </summary>
        public void DrawBound()
        {
            root.DrawBound();
        }
    }
}