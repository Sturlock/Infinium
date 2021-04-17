using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Infinium.Core.UI.Dragging
{
    public class DragItem<T> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler where T : class
    {
        Vector3 startPos;
        Transform originalParent;
        IDragSource<T> source;

        Canvas parentCanvas;

        private void Awake()
        {
            parentCanvas = GetComponentInParent<Canvas>();
            source = GetComponentInParent<IDragSource<T>>();
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            startPos = transform.position;
            originalParent = transform.parent;

            GetComponent<CanvasGroup>().blocksRaycasts = false;
            transform.SetParent(parentCanvas.transform, true);
        }
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            transform.position = startPos;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            transform.SetParent(originalParent, true);

            IDragDestination<T> container;
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                container = parentCanvas.GetComponent<IDragDestination<T>>();
            }
            else container = GetContainer(eventData);
        }

        private IDragDestination<T> GetContainer(PointerEventData eventData)
        {
            if (eventData.pointerEnter)
            {
                var container = eventData.pointerEnter.GetComponent<IDragDestination<T>>();

                if (container != null) return container;
            }
            return null;
        }

        private void DropItemIntoContainer(IDragDestination<T> destination)
        {
            if (object.ReferenceEquals(destination, source)) return;

            var destinationContainer = destination as IDragContainer<T>;
            var sourceContainer = source as IDragContainer<T>;

            if (destinationContainer == null || sourceContainer == null || destinationContainer.GetItem() == null ||
                object.ReferenceEquals(destinationContainer.GetItem(), sourceContainer.GetItem()))
            {
                AttemptSimpleTransfer(destination);
                return;
            }

            AttemptSwap(destinationContainer, sourceContainer);
        }

        private void AttemptSwap(IDragContainer<T> destination, IDragContainer<T> source)
        {
            var removedSourceNumber = source.GetNumber();
            var removedSourceItem = source.GetItem();
            var removedDestinationNumber = destination.GetNumber();
            var removedDestinationItem = destination.GetItem();

            source.RemoveItem(removedSourceNumber);
            destination.RemoveItem(removedDestinationNumber);

            var sourceTakeBackNumber = CalculateTakeBack(removedSourceItem, removedSourceNumber, source, destination);
            var destinationTakeBackNumber = CalculateTakeBack(removedDestinationItem, removedDestinationNumber, destination, source);

            if (sourceTakeBackNumber > 0)
            {
                source.AddItems(removedDestinationItem, sourceTakeBackNumber);
                removedSourceNumber -= sourceTakeBackNumber;
            }
            if (destinationTakeBackNumber > 0)
            {
                destination.AddItems(removedDestinationItem, destinationTakeBackNumber);
                removedDestinationNumber -= destinationTakeBackNumber;
            }
        }

        private void AttemptSimpleTransfer(IDragDestination<T> destination)
        {
            throw new NotImplementedException();
        }
        private int CalculateTakeBack(T removedItem, int removedNumber, IDragContainer<T> removeSource, IDragContainer<T> destination)
        {
            throw new NotImplementedException();
        }
    }
}
