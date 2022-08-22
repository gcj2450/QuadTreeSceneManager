using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WCC.QuadTree
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class Node : INode
    {
        /// <summary>
        /// 节点边界
        /// </summary>
        public Bounds bound { get; set; }

        /// <summary>
        /// 节点当前深度
        /// </summary>
        private int depth;
        /// <summary>
        /// 所属树
        /// </summary>
        private Tree belongTree;
        /// <summary>
        /// 子节点列表
        /// </summary>
        private Node[] childList;
        /// <summary>
        /// 物体列表
        /// </summary>
        private List<ObjData> objDataList;

        private bool isInside = false;

        public Node(Bounds bound, int depth, Tree belongTree)
        {
            this.belongTree = belongTree;
            this.bound = bound;
            this.depth = depth;
            objDataList = new List<ObjData>();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="objData"></param>
        public void InsertObjData(ObjData objData)
        {
            Node node = null;
            bool bChild = false;

            if (depth < belongTree.maxDepth && childList == null)
            {
                CreateChild();
            }
            if (childList != null)
            {
                for (int i = 0; i < childList.Length; ++i)
                {
                    Node item = childList[i];
                    // if (item.bound.Contains(objData.pos))
                    if (item.bound.Intersects(objData.GetObjBounds()))
                    {
                        if (node != null)
                        {
                            bChild = false;
                            break;
                        }
                        node = item;
                        bChild = true;
                    }
                }
            }
            //只有一个子节点，则放入子节点里
            if (bChild)
            {
                node.InsertObjData(objData);
            }
            else
            {
                objDataList.Add(objData);
            }
        }

        //在该节点里
        public void Inside(Camera camera)
        {
            //刷新子节点
            if (childList != null)
            {
                for (int i = 0; i < childList.Length; ++i)
                {
                    if (childList[i].bound.CheckBoundIsInCamera(camera, belongTree.viewRatio))
                    {
                        childList[i].Inside(camera);
                    }
                    else
                    {
                        childList[i].Outside(camera);
                    }
                }
            }

            if (isInside)
                return;
            isInside = true;
            for (int i = 0; i < objDataList.Count; ++i)
            {
                ObjManager.Instance.LoadAsync(objDataList[i]);
            }

        }

        //不在该节点里
        public void Outside(Camera camera)
        {
            //刷新子节点
            if (childList != null)
            {
                for (int i = 0; i < childList.Length; ++i)
                {
                    childList[i].Outside(camera);
                }
            }
            if (isInside == false)
                return;
            isInside = false;
            for (int i = 0; i < objDataList.Count; i++)
            {
                ObjManager.Instance.Unload(objDataList[i].uid);
            }
        }

        /// <summary>
        /// 创建四个子节点
        /// </summary>
        private void CreateChild()
        {
            childList = new Node[belongTree.maxChildCount];
            int index = 0;
            for (int i = -1; i <= 1; i += 2)
            {
                for (int j = -1; j <= 1; j += 2)
                {
                    Vector3 centerOffset = new Vector3(bound.size.x / 4 * i, 0, bound.size.z / 4 * j);
                    Vector3 cSize = new Vector3(bound.size.x / 2, bound.size.y, bound.size.z / 2);
                    Bounds cBound = new Bounds(bound.center + centerOffset, cSize);
                    childList[index++] = new Node(cBound, depth + 1, belongTree);
                }
            }
        }

        //public void TriggerMove(Camera camera)
        //{
        //    //刷新当前节点
        //    for (int i = 0; i < objList.Count; ++i)
        //    {
        //        //进入该节点中意味着该节点在摄像机内，把该节点保存的物体全部创建出来
        //        ResourcesManager.Instance.LoadAsync(objList[i]);
        //    }

        //    if (depth == 0)
        //    {
        //        ResourcesManager.Instance.RefreshStatus();
        //    }

        //    //刷新子节点
        //    if (childList != null)
        //    {
        //        for (int i = 0; i < childList.Length; ++i)
        //        {
        //            if (childList[i].bound.CheckBoundIsInCamera(camera))
        //            {
        //                childList[i].TriggerMove(camera);
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 绘制边界
        /// </summary>
        public void DrawBound()
        {
            if (isInside)
            {
                ///相机在节点内，绘制为红色
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(bound.center, bound.size);
            }
            else if (objDataList.Count != 0)
            {
                ///节点内无物体，绘制为蓝色
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(bound.center, bound.size);
            }
            else
            {
                //节点内有物体，绘制为绿色
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(bound.center, bound.size);
            }

            if (childList != null)
            {
                for (int i = 0; i < childList.Length; ++i)
                {
                    childList[i].DrawBound();
                }
            }
        }
    }
}