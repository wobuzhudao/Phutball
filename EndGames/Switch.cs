﻿namespace EndGames
{
    public class Switch<T>
    {
        private readonly T _first;
        private readonly T _second;

        public Switch(T first, T second)
        {
            _first = first;
            _second = second;
        }

        public T Value {get { return _first; }}

        public Switch<T> Swap()
        {
            return new Switch<T>(_second, _first);
        }

        public bool Is(T value)
        {
            return _first.Equals(value);
        }
    }
}