using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using PacketLibrary;


namespace SharedProjectServerClient
{
    public class SharedMethods
    {
        private static BinaryFormatter serializer = new BinaryFormatter();

        public static void WriteTextMessage(TcpClient client, string v)
        {
            StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.ASCII);
            writer.WriteLine(v);
            writer.Flush();
        }

        public static string ReadTextMessage(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream(), Encoding.ASCII);
            string message = reader.ReadLine();
            return message;
        }

        public static string ReadMessage(TcpClient client)
        {

            byte[] buffer = new byte[1024];
            int totalRead = 0;

            do
            {
                int read = client.GetStream().Read(buffer, totalRead, buffer.Length - totalRead);
                totalRead += read;
                Console.WriteLine("ReadMessage: " + read);

            } while (client.GetStream().DataAvailable);

            return Encoding.Unicode.GetString(buffer, 0, totalRead);
        }

        public static void SendMessage(TcpClient client, string message)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(message);
            client.GetStream().Write(bytes, 0, bytes.Length);
        }

        public static void SendPacket(TcpClient client, Packet packet)
        {
            try
            {
                serializer.Serialize(client.GetStream(), packet);

            }
            catch (IOException)
            {
                Console.WriteLine("Packet not send");
            }
        }
    }
}
