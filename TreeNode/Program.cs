using System;
using System.Collections.Generic;

namespace TreeNode
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = new List<Item>()
            {
                new Item()
                {
                    Id=1,
                    ParentId=4
                },
                new Item()
                {
                    Id=2,
                    ParentId=5
                },
                new Item()
                {
                    Id=3,
                    ParentId=1
                },
                new Item()
                {
                    Id=4,
                    ParentId=null
                },
                new Item()
                {
                    Id=5,
                    ParentId=4
                },

                new Item()
                {
                    Id=6,
                    ParentId=4
                },
                new Item()
                {
                    Id=7,
                    ParentId=null
                },
                new Item()
                {
                    Id=8,
                    ParentId=7
                },
                new Item()
                {
                    Id=9,
                    ParentId=8
                }
            };



            SortedDictionary<int, Leaf> leafCollection = new SortedDictionary<int, Leaf>();
            List<Leaf> root = new List<Leaf>();
            bool isRootExist = false;

            foreach (var item in items)
            {
                Leaf parentNode;
                Leaf currentLeaf = new Leaf
                {
                    Id = item.Id,
                    ParentId = item.ParentId
                };
                if (item.ParentId.HasValue)
                {
                    if (!leafCollection.TryGetValue(item.Id, out Leaf existedLeaf))
                    {
                        leafCollection.Add(item.Id, currentLeaf);
                    }
                    else
                    {
                        currentLeaf = existedLeaf;
                        currentLeaf.ParentId = item.ParentId;
                    }

                    if (!leafCollection.TryGetValue(item.ParentId.Value, out parentNode))
                    {
                        parentNode = new Leaf();
                        parentNode.Id = item.ParentId.Value;
                        leafCollection.Add(item.ParentId.Value, parentNode);
                    }
                    parentNode.Children.Add(currentLeaf);

                }
                else
                {
                    if (leafCollection.TryGetValue(item.Id, out parentNode))
                    {

                        root.Add(parentNode);
                    }
                    else
                    {
                        leafCollection.Add(item.Id, currentLeaf);
                        root.Add(currentLeaf);
                    }
                    isRootExist = true;
                }
            }

            if (!isRootExist)
            {
                var rootNode = leafCollection.Where(x => x.Value.ParentId == leafCollection.First().Key);
                foreach (var item in rootNode)
                {
                    leafCollection.TryGetValue(item.Key, out Leaf parentNode);
                    root.Add(parentNode);
                }

            }



            Console.ReadLine();
        }
    }

    public class Item
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
    }

    public class Leaf
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public List<Leaf> Children { get; set; }

        public Leaf()
        {
            Children = new List<Leaf>();
        }
    }
}
