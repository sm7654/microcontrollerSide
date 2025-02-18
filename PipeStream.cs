using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace microcontrollerSide
{
    static class PipeStream
    {
        private static string pipeLocation = "/tmp/ProgramFolder/pipe";
        private static string pipeFolder = "/tmp/ProgramFolder";
        private static bool IsConnected = false;
        private static StreamWriter pipeWriter = null;
        private static StreamReader pipeReader = null;


        public static string InitionlisePipe()
        {
            if (IsConnected)
                return "connected";
            
            try
            {
                FileStream pipe = new FileStream(pipeLocation, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamReader streamReader = new StreamReader(pipe);
                StreamWriter streamWriter = new StreamWriter(pipe);
                pipeWriter = streamWriter;
                IsConnected = true;
                pipeWriter.AutoFlush = true;
                pipeReader = streamReader;

                new Thread(() => ReadFromPipe()).Start();

                return "connected";


            }

            catch (Exception e) {
                return e.Message; }
        }

        public static void WriteToPipe(string data)
        {
            if (!IsConnected && pipeWriter == null)
                return;
            
            try
            {
                pipeWriter.WriteLine($"{data}");    
            }
            catch (Exception e){ }

        }

        private static void ReadFromPipe()
        {
            if (pipeReader == null)
                return;

            try
            {
                while (true)
                {
                    string Message = pipeReader.ReadLine();
                    new Thread(() => MessageSelector(Message)).Start();
                }
            }
            catch (Exception e) { }
        }

        private static void MessageSelector(string message)
        {
            try
            {
                string DecryptedData = Encoding.UTF8.GetString(AesEncryption.DecryptData(Encoding.UTF8.GetBytes(message)));


                ExperimentController.MicroChipCommunication(DecryptedData);


            } catch (Exception e) { }
        }





        public static string GetPipeLocation()
        {
            return pipeLocation;
        }
        public static bool IsPipeConnected()
        {
            return IsConnected;
        }

    }
}




