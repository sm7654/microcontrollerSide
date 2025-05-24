using System;
using System.IO;
using System.Text;
using System.Threading;

namespace microcontrollerSide
{
    static class PipeStream
    {



        private static string ReaderPipeLocation = "/tmp/ProgramFolder/ListenForCWriteForPython";
        private static string WriterPipeLocation = "/tmp/ProgramFolder/WriteForCListenForPython";



        private static bool IsConnected = false;
        private static StreamWriter pipeWriter = null;
        private static StreamReader pipeReader = null;
        
        
        public static bool InitionlisePipe()
        {
            if (IsConnected)
                return true;
                       
            try
            {
                

                FileStream Rpipe = new FileStream(ReaderPipeLocation, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamReader streamReader = new StreamReader(Rpipe);
                FileStream Wpipe = new FileStream(WriterPipeLocation, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamWriter streamWriter = new StreamWriter(Wpipe);

                pipeWriter = streamWriter;
                IsConnected = true;
                pipeWriter.AutoFlush = true;
                pipeReader = streamReader;

                pipeWriter.Write("ReadyToStart");



                new Thread(() => ReadFromPipe()).Start();

                return true;


            }

            catch (Exception e) {
                Console.WriteLine($"{e.Message}"); return false; 
             }
        }

        public static void WriteToPipe(string data)
        {
            if (!IsConnected || pipeWriter == null)
            {
                if (!IsConnected && pipeWriter == null)
                {
                    Console.WriteLine("Cannot write to pipe: Not connected AND pipeWriter is null.");
                }
                else if (!IsConnected)
                {
                    Console.WriteLine("Cannot write to pipe: Not connected to the pipe server.");
                }
                else if (pipeWriter == null)
                {
                    Console.WriteLine("Cannot write to pipe: pipeWriter is null.");
                }
                return;
            }


            try
            {
                pipeWriter.WriteLine($"{data}");

            }
            catch (Exception e){ Console.WriteLine("error"); }

        }

        private static void ReadFromPipe()
        {
            if (pipeReader == null)
            {
                Console.WriteLine("the pipe is not connected");
                return;
            }
            else
            {
                Console.WriteLine("Good Job");
            }



            try
            {
                while (true)
                {
                    string Message = pipeReader.ReadLine();
                    Console.WriteLine($"THE MESSEAGE GOT FROM PIPE IS {Message}");
                    if (Message != null && Message != "" && Message != "\n")

                    { new Thread(() => MessageSelector(Message)).Start(); }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERRRRRRRROOOOOORRRRR");
            }
        }

        private static void MessageSelector(string message)
        {
            try
            {
                Console.WriteLine($"______________________________\nIn the MessageSelector and got a new message\n______________________________");
                if (message.Split(';')[0] == "EXPERIMENT_RESULTS")
                {
                    Console.WriteLine($"got from pipe: {message}");
                    MicroController.MicroChipCommunication(message); // delete and send diractly to SendToClient
                } else if (message.Split(';')[0] == "GotExper")
                {
                    MicroController.SendToClient($"GotExper;{message.Split(';')[1]}");
                }

            } catch (Exception e) {
            
            }
        }
        

        public static void DisconnetFromPipe()
        {
            IsConnected = false;
            pipeWriter.Close();
            pipeReader.Close();
            pipeWriter = null;
            pipeReader = null;

        
        }
        public static bool IsPipeConnected()
        {
            return IsConnected;
        }

    }
}




