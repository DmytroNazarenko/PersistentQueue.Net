using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistentDataStructures
{
    public class PersistentStack<T> : IEnumerable<T>
    {
        private T _Value;
        private PersistentStack<T> _Previous;

        public Int32 Count { get; private set; }

        private PersistentStack(PersistentStack<T> prev, T newElement) : this()
        {
            _Value = newElement;
            Count = prev.Count + 1;
            _Previous = prev;
        }

        public PersistentStack()
        {
            Count = 0;
        }

        public PersistentStack<T> Push(T element)
        {
            return new PersistentStack<T>(this, element);
        }

        public PersistentStack<T> Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this._Previous;
        }

        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this._Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            PersistentStack<T> current = this;
            while (this != null)
            {
                yield return current._Value;

                current = current._Previous;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
