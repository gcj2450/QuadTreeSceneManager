//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace YouTubeQuad
//{
//    //https://www.youtube.com/watch?v=R2CPNuP8eiU
//    public class QuadTreeYouTube
//    {
//        static int childCount = 4;
//        static int maxObjectCount = 100;
//        static int maxDepth;

//        private bool searched = false;

//        private QuadTreeYouTube nodeParent;
//        private QuadTreeYouTube[] childNodes;

//        private List<GameObject> objects = new List<GameObject>();

//        private int currentDeoth = 0;

//        private Vector2 nodeCenter;
//        private Rect nodeBounds = new Rect();

//        private float nodeSize = 0;

//        public QuadTreeYouTube(float worldSize,int maxNodeDepth,int maxNodeObjects,Vector2 center):this(worldSize,0,center,null)
//            {
//            maxDepth = maxNodeDepth;
//            maxObjectCount = maxNodeObjects;
//        }

//        private QuadTreeYouTube(float size,int depth,Vector2 center,QuadTreeYouTube parent)
//        {
//            this.nodeSize = size;
//            this.currentDeoth = depth;
//            this.nodeCenter = center;
//            this.nodeParent = parent;

//            if (this.currentDeoth == 0)
//                this.nodeBounds = new Rect(center.x - size, center.y - size, size * 2, size * 2);
//            else
//                this.nodeBounds = new Rect(center.x - (size / 2f), center.y - (size / 2f), size, size);
//        }

//        public bool Add(GameObject go)
//        {
//            if (this.nodeBounds.Contains(go.transform.position))
//            {
//                return this.Add(go, new Vector2(go.transform.position.x, go.transform.position.y)) != null;
//            }
//            return false;
//        }

//        private QuadTreeYouTube Add(GameObject obj,Vector2 objCenter)
//        {
//            if (this.childNodes!=null)
//            {
//                //^z plus
//                //2 | 3
//                //--|--> xplus
//                //0 | 1

//                int index = (objCenter.x < this.nodeCenter.x ? 0 : 1) + (objCenter.y < this.nodeCenter.y ? 0 : 2);
//                return this.childNodes[index].Add(obj, objCenter);
//            }

//            if (this.currentDeoth<maxDepth&&this.objects.Count+1>maxObjectCount)
//            {
//                Split(nodeSize);
//                foreach (GameObject nodeObject in objects)
//                {
//                    Add(nodeObject);
//                }
//                this.objects.Clear();
//                return Add(obj, objCenter);
//            }
//            else
//            {
//                this.objects.Add(obj);
//            }
//            return this;
//        }



//    }
//}