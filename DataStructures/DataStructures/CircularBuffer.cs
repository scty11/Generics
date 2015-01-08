using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataStructures
{
    public class Buffer<T> : IBuffer<T>
    {
        protected Queue<T> _queue;

        public Buffer()
        {
            _queue = new Queue<T>();
        }

        public virtual bool IsEmpty
        {
            get { return _queue.Count == 0; }
        }

        public virtual T Read()
        {
            return _queue.Dequeue();
        }

        public virtual void Write(T value)
        {
            _queue.Enqueue(value);
        }

        //these allow us to use a foreach on our buffer. 
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _queue)
            {
                yield return item;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        
    }

    public class CircularBuffer<T> : Buffer<T>
    {
        public event EventHandler<ItemDiscardedEventArgs<T>> ItemDiscarded;
        int _capacity;
        public CircularBuffer(int capacity = 10)
        {
            _capacity = capacity;
        }

        public override void Write(T value)
        {
            base.Write(value);
            if (_queue.Count > _capacity)
            {
                var d = _queue.Dequeue();
                OnItemDiscarded(d, value);
            }
        }

        private void OnItemDiscarded(T d, T value)
        {
            if (ItemDiscarded != null)
            {
                ItemDiscarded(this, new ItemDiscardedEventArgs<T>(d, value));
            }
        }

       
        public bool IsFull
        {
            get { return _queue.Count == _capacity; }
        }
    }

    public class ItemDiscardedEventArgs<T> : EventArgs
    {
        public ItemDiscardedEventArgs(T discard, T newItem)
        {
            ItemDiscarded = discard;
            NewItem = newItem;
        }

        public T ItemDiscarded { get; set; }
        public T NewItem { get; set; }
    }
}
