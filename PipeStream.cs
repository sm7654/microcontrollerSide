using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace microcontrollerSide
{
    static class PipeStream
    {
        private static string pipeLocation = "/tmp/ProgramFolder/pipe";
        private static string pipeFolder = "/tmp/ProgramFolder";
        private static bool IsConnected = false;
        private static StreamWriter pipeWriter = null;
        private static StreamReader pipeReader = null;
        
        public static bool InitionlisePipe()
        {

            return InitionlisePipe(pipeLocation);
        }
        public static bool InitionlisePipe(string path)
        {
            if (IsConnected)
                return true;
                       
            try
            {
                pipeLocation = path;
                FileStream pipe = new FileStream(pipeLocation, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamReader streamReader = new StreamReader(pipe);
                StreamWriter streamWriter = new StreamWriter(pipe);
                pipeWriter = streamWriter;
                IsConnected = true;
                pipeWriter.AutoFlush = true;
                pipeReader = streamReader;

                new Thread(() => ReadFromPipe()).Start();

                Console.WriteLine("connected");
                return true;


            }

            catch (Exception e) {
                Console.WriteLine("Not connected"); return false; }
        }

        public static void WriteToPipe(string data)
        {
            if (!IsConnected && pipeWriter == null)
                return;
            
            try
            {
                pipeWriter.WriteLine($"{data}\n");    


            }
            catch (Exception e){ MicroController.PipeMessageRec("at WriteToPipe"); }

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
                    if (Message != null && Message != "" && Message != "\n")
                        new Thread(() => MessageSelector(Message)).Start();
                }
            }
            catch (Exception e) { MicroController.PipeMessageRec("at ReadFromPipe"); }
        }

        private static void MessageSelector(string message)
        {
            try
            {
                string DecryptedData = message;
                Console.WriteLine($"got from pipe: {DecryptedData}");
                ExperimentController.MicroChipCommunication(DecryptedData);


            } catch (Exception e) { MicroController.PipeMessageRec("At messege selector"); }
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




