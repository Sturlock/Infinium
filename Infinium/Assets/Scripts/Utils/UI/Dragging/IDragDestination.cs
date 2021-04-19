using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infinium.Core.UI.Dragging
{
    public interface IDragDestination<T> where T : class
    {
        int MaxAcceptable(T item);

        void AddItems(T item, int number);
    }
}
