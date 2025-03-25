using System;
using System.IO;
using System.Threading;

namespace microcontrollerSide
{
    static class PipeStream
    {
        private static string ReaderPipeLocation = "/tmp/ProgramFolder/Rpipe";
        private static string WriterPipeLocation = "/tmp/ProgramFolder/Wpipe";
        private static string pipeFolder = "/tmp/ProgramFolder";
        private static bool IsConnected = false;
        private static StreamWriter pipeWriter = null;
        private static StreamReader pipeReader = null;
        
        
        public static bool InitionlisePipe()
        {
            if (IsConnected)
                return true;
                       
            try
            {
                

                FileStream Rpipe = new FileStream(ReaderPipeLocation, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamReader streamReader = new StreamReader(Rpipe);
                FileStream Wpipe = new FileStream(WriterPipeLocation, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamWriter streamWriter = new StreamWriter(Wpipe);

                pipeWriter = streamWriter;
                IsConnected = true;
                pipeWriter.AutoFlush = true;
                pipeReader = streamReader;

                new Thread(() => ReadFromPipe()).Start();

                Console.WriteLine("connected");
                return true;


            }

            catch (Exception e) {
                Console.WriteLine($"{e.Message}"); return false; }
        }

        public static void WriteToPipe(string data)
        {
            if (!IsConnected && pipeWriter == null)
                return;
            
            try
            {
                pipeWriter.WriteLine($"{data}\n");
                Console.WriteLine("Sent The message");

            }
            catch (Exception e){ Console.WriteLine("error"); }

        }

        private static void ReadFromPipe()
        {
            if (pipeReader == null)
                return;

            try
            {
                while (true)
                {
                    Thread.Sleep(20);
                    string Message = pipeReader.ReadLine();
                    if (Message != null && Message != "" && Message != "\n")
                        new Thread(() => MessageSelector(Message)).Start();
                }
            }
            catch (Exception e) { }
        }

        private static void MessageSelector(string message)
        {
            try
            {
                if (message.Split(';')[0] == "EXPERIMENT_RESULTS")
                {
                    Console.WriteLine($"got from pipe: {message}");
                    MicroController.MicroChipCommunication(message); // delete and send diractly to SendToClient
                }


            } catch (Exception e) { }
        }
        
        public static bool IsPipeConnected()
        {
            return IsConnected;
        }

    }
}




