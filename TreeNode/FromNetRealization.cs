using System;
using System.Collections.Generic;
using System.Text;

namespace TreeNode
{
    class FromNetRealization
    {
        public class MyObject
        {   // The actual object
            public int ParentId { get; set; }
            public int Id { get; set; }
        }

        public class Node
        {
            public List<Node> Children = new List<Node>();
            public Node Parent { get; set; }
            public MyObject Source { get; set; }
        }

        public List<Node> BuildTreeAndGetRoots(List<MyObject> actualObjects)
        {
            var lookup = new Dictionary<int, Node>();
            var rootNodes = new List<Node>();

            foreach (var item in actualObjects)
            {
                // add us to lookup
                Node ourNode;
                if (lookup.TryGetValue(item.Id, out ourNode))
                {   // was already found as a parent - register the actual object
                    ourNode.Source = item;
                }
                else
                {
                    ourNode = new Node() { Source = item };
                    lookup.Add(item.Id, ourNode);
                }

                // hook into parent
                if (item.ParentId == 0)
                {   // is a root node
                    rootNodes.Add(ourNode);
                }
                else
                {   // is a child row - so we have a parent
                    Node parentNode;
                    if (!lookup.TryGetValue(item.ParentId, out parentNode))
                    {   // unknown parent, construct preliminary parent
                        parentNode = new Node();
                        lookup.Add(item.ParentId, parentNode);
                    }
                    parentNode.Children.Add(ourNode);
                    ourNode.Parent = parentNode;
                }
            }

            return rootNodes;
        }

        public List<MyObject> SetMyObjects()
        {
            var items2 = new List<MyObject>()
            {
                new MyObject()
                {
                    Id=1,
                    ParentId=0
                },
                new MyObject()
                {
                    Id=2,
                    ParentId=1
                },
                new MyObject()
                {
                    Id=3,
                    ParentId=2
                },
                new MyObject()
                {
                    Id=4,
                    ParentId=1
                },
            };

            return items2;
        }
    }
}
