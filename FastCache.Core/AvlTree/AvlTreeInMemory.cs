using System;
using FastCache.Core.AvlTree.Models;

namespace FastCache.Core.AvlTree
{
    internal class AvlTreeInMemory
    {
        internal Node Insert(int key, object value, Node node)
        {
            if (node is null)
            {
                return new Node()
                {
                    Key = key,
                    Value = value,
                    Height = 0,
                    Left = null,
                    Right = null
                };
            }

            if(key < node.Key)
                node.Left = Insert(key, value, node.Left);
            if (key > node.Key)
                node.Right = Insert(key, value, node.Right);
            else if(key.Equals(node.Key))
                node.Value = value;

            node.UpdateHeight();
            
            return node.ReBalance();
        }

        internal object Get(int key, Node node) => 
            node is null ? null :
            key < node.Key ? Get(key, node.Left) :
            key > node.Key ? Get(key, node.Right) : node.Value;
    }
}
