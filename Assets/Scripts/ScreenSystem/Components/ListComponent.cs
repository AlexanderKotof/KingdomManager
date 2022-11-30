using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScreenSystem.Components
{
    public class ListComponent : WindowComponent
    {
        public struct ListParameters
        {
            public int index;

            public ListParameters(int index)
            {
                this.index = index;
            }
        }
        public WindowComponent resource;
        public Transform container;

        public List<WindowComponent> items = new List<WindowComponent>();
        public int ItemsCount => items.Count;

        public void SetItems(int count, Action<WindowComponent, ListParameters> onComplite)
        {
            if (count <= 0)
                return;

            if (count < items.Count)
            {
                for (int i = 0; count < items.Count; i++)
                {
                    RemoveAt(0);
                }
            }

            for (int i = 0; i < count; i++)
            {
                if (items.Count > i)
                {
                    onComplite?.Invoke(items[i], new ListParameters(i));
                    continue;
                }

                var item = CreateItem(i);
                onComplite?.Invoke(item, new ListParameters(i));
            }
        }

        public void SetItems<T>(int count, Action<T, ListParameters> onComplite) where T : WindowComponent
        {
            if (count < 0)
                return;

            if (count < items.Count)
            {
                for (int i = 0; count < items.Count; i++)
                {
                    RemoveAt(0);
                }
            }


            for (int i = 0; i < count; i++)
            {
                if (items.Count > i)
                {
                    onComplite?.Invoke((T)items[i], new ListParameters(i));
                    continue;
                }
                var item = CreateItem(i);
                onComplite?.Invoke((T)item, new ListParameters(i));
            }
        }

        private WindowComponent CreateItem(int index)
        {
            var parent = container ? container : transform;
            var item = Instantiate(resource, parent);
            item.name += $"({index})";
            items.Add(item);
            return item;
        }

        public void AddItem<T>(Action<T, ListParameters> onComplite) where T : WindowComponent
        {
            var parent = container ? container : transform;
            var index = items.Count;
            var item = CreateItem(index);
            onComplite?.Invoke((T)item, new ListParameters(index));
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= items.Count)
                return;

            var item = items[index];
            items.RemoveAt(index);
            Destroy(item.gameObject);
        }

        public T GetItem<T>(int index) where T : WindowComponent
        {
            if (items.Count > index)
                return (T)items[index];

            return null;
        }
    }
}
