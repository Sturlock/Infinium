using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infinium.Core.UI.Dragging
{
        public interface IDragContainer<T> : IDragContainer<T>, IDragSource<T> where T : class
        {

        }
}
