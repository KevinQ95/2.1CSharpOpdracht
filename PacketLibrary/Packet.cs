using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketLibrary
{
    [Serializable]
    public class Packet
    {
        public string PacketName { get; set; }

        public Packet()
        {
            PacketName = "";
        }
    }
}
