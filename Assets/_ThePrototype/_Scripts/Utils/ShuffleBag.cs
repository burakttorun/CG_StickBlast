using System;
using System.Collections.Generic;
using BasicArchitecturalStructure;

namespace ThePrototype.Scripts.Utils
{
    public class ShuffleBag<T>
    {
        private List<T> _items;
        private int _currentIndex;
        private Random _random;

        public ShuffleBag()
        {
            _items = new();
            _currentIndex = -1;
            _random = new Random();
        }

        public void AddRange(IEnumerable<T> newItems)
        {
            _items.AddRange(newItems);
            Shuffle();
            _currentIndex = _items.Count - 1;
        }

        public T GetNext()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("Shuffebag Empty!");
            }

            if (_currentIndex < 0)
            {
                _currentIndex = _items.Count - 1;
                Shuffle();
            }

            T item = _items[_currentIndex];
            _currentIndex--;
            return item;
        }

        private void Shuffle()
        {
            for (int i = _items.Count - 1; i > 0; i--)
            {
                int randomIndex = _random.Next(0, i + 1);
                (_items[i], _items[randomIndex]) = (_items[randomIndex], _items[i]);
            }
        }
    }
}