using System;
using System.Collections.Generic;
using System.Text;

namespace SharedData
{
    public enum Packet
    {
        PLAYER_SPAWN = 1,
        PLAYER_MOVE,
        PLAYER_ATTACK
    }

    class PacketData
    {
        private List<byte> buffer;
        private byte[] readableBuffer;
        private int readPos;
    }
}
