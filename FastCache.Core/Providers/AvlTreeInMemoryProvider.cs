using System;
using System.Collections;
using System.Collections.Generic;
using FastCache.Abstractions;
using FastCache.Core.AvlTree;
using FastCache.Core.AvlTree.Models;

namespace FastCache.Core.Providers
{
    public class AvlTreeInMemoryProvider : ICacheProvider, IEnumerable
    {
        private Node _root;
        private readonly AvlTreeInMemory _tree;

        public AvlTreeInMemoryProvider()
        {
            _tree = new AvlTreeInMemory();
        }

        public void Insert(string key, object value)
        {
            _root = _tree.Insert(GetHashCode(key), value, _root);
        }

        public object Get(string key) => _tree.Get(GetHashCode(key), _root);

        protected virtual int GetHashCode(string key) => key.GetDeterministicHashCode();

        public IEnumerator GetEnumerator()
        {
            return new TreeEnumerator(_root);
        }

        private sealed class TreeEnumerator : IEnumerator
        {
            private readonly Stack<Node> _stack = new Stack<Node>();
            private Node _current;
            private Node _traversePointer;
            private readonly Node _startPointer;
            public TreeEnumerator(Node root)
            {
                _traversePointer = root;
                _startPointer = root;
            }
            public bool MoveNext()
            {
                while (_stack.Count > 0 || _traversePointer != null)
                {
                    if (_traversePointer != null)
                    {
                        _stack.Push(_traversePointer);
                        _traversePointer = _traversePointer.Left;
                    }
                    else
                    {
                        var node = _stack.Pop();
                        _current = node;
                        _traversePointer = node.Right;
                        return true;
                    }
                }

                return false;
            }

            public void Reset()
            {
                _traversePointer = _startPointer;
            }

            public object? Current
            {
                get
                {
                    try
                    {
                        return _current.Value;
                    }
                    catch (NullReferenceException)
                    {
                        throw new NullReferenceException();
                    }
                }
            }
        }
    }
}
