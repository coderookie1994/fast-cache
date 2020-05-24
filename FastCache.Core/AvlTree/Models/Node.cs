using System;
using System.Collections.Generic;
using System.Text;

namespace FastCache.Core.AvlTree.Models
{
    internal class Node
    {
        internal int Key { get; set; }
        internal object Value { get; set; }
        internal int Height { get; set; }
        internal Node Left;
        internal Node Right;

        internal void UpdateHeight()
        {
            Height = 1 + Math.Max(GetHeight(Left), GetHeight(Right));
        }

        internal Node ReBalance()
        {
            var balanceFactor = GetBalance();

            if (balanceFactor < -1)
            {
                if (GetHeight(Left.Left) > GetHeight(Left.Right))
                {
                    return RotateRight(this);
                }
                else if (GetHeight(Left.Left) < GetHeight(Left.Right))
                {
                    Left = RotateLeft(Left);
                    return RotateRight(this);
                }
            }
            else if (balanceFactor > 1)
            {
                if (GetHeight(Right.Right) > GetHeight(Right.Left))
                {
                    return RotateLeft(this);
                }
                else if (GetHeight(Right.Right) < GetHeight(Right.Left))
                {
                    Right = RotateRight(Right);
                    return RotateLeft(this);
                }
            }

            return this;
        }

        private int GetHeight(Node node)
        {
            return node?.Height ?? -1;
        }

        private Node RotateRight(Node node)
        {
            var leftNode = new Node()
            {
                Key = node.Left.Key, Height = 0, Left = node.Left.Left, Right = node.Left.Right,
                Value = node.Left.Value
            };

            node.Left = leftNode.Right;
            leftNode.Right = node;

            node.UpdateHeight();
            leftNode.UpdateHeight();

            return leftNode;
        }

        private Node RotateLeft(Node node)
        {
            var rightNode = new Node()
            {
                Key = node.Right.Key, Height = 0, Left = node.Right.Left, Right = node.Right.Right,
                Value = node.Right.Value
            };

            node.Right = rightNode.Left;
            rightNode.Left = node;

            node.UpdateHeight();
            rightNode.UpdateHeight();

            return rightNode;
        }

        private int GetBalance() => GetHeight(Right) - GetHeight(Left);
    }
}
