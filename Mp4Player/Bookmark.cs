using System;

namespace Mp4Player
{
    internal class Bookmark
    {
        public Bookmark(TimeSpan time, int count)
        {
            Time = time;
            Number = count + 1;
        }

        public TimeSpan Time { get; }

        public int Number { get; }
    }
}