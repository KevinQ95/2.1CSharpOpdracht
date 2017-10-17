using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketLibrary
{
    [Serializable]
    public class DrawCardPacket : Packet
    {
        public DrawCardPacket()
        {
            PacketName = "DrawCardPacket";
        }
    }
}
