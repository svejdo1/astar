using System.Collections;
using System.Collections.Generic;

namespace Barbar.Framework.Collections {
  public class OrderedList<T> {
    private LinkedList<T> m_List = new LinkedList<T>();

    public void Add(T item) {
      if (m_List.Count == 0) {
        m_List.AddFirst(item);
      } else {
        var iterator = m_List.First;
        while ((iterator.Next != null) && (Comparer.Default.Compare(iterator.Value, item) <= 0)) {
          iterator = iterator.Next;
        }
        if (Comparer.Default.Compare(iterator.Value, item) <= 0) {
          m_List.AddAfter(iterator, item);
        } else {
          m_List.AddBefore(iterator, item);
        }
      }
    }

    public void Clear() {
      m_List.Clear();
    }

    public bool Contains(T item) {
      return m_List.Contains(item);
    }

    public T First() {
      return m_List.First.Value;
    }

    public void Remove(T item) {
      this.m_List.Remove(item);
    }

    public int Count {
      get { return m_List.Count; }
    }
  }
}
