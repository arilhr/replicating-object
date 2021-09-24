using System;

namespace SharedData
{
    public class Player
    {
        public int xPos;
        public int yPos;

        public Player() { }
        public Player(int _xPos, int _yPos)
        {
            xPos = _xPos;
            yPos = _yPos;
        }

        public void Move(int _x, int _y)
        {
            xPos += _x;
            yPos += _y;
        }
    }
}
