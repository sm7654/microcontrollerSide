using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.IO.Pipes;

namespace microcontrollerSide
{
    static class ExperimentController
    {
        private static CommunicaionForm form;
        public static void SetForm(CommunicaionForm f)
        {
            form = f;
        }
        public static void NewExperiment(string[] experimentString)
        {
            if (!(experimentString.Length > 0))
                MicroController.SendToClient("ERROR;406");
            try
            {
                string experName = experimentString[1]; // Fxperiment name
                string Frequency = experimentString[2]; // Frequency of engine


                form.GetCLientStatusLabel().Text = $"{experName}: {Frequency}";

            }
            catch (Exception ex) { return; }
        }

        public static void MicroChipCommunication()
        {

    







        // pipe communication

            DateTime curentDate = DateTime.Now;
            DateTime dateOnly = curentDate.Date;
            Random random = new Random();
            string curentTime = dateOnly.ToString("yyyy-MM-dd");
            string deltaSpeed = random.Next(1, 100).ToString();
            string temp = random.Next(-30, 50).ToString();
            string cameraSpeed = random.Next(1, 100).ToString();
            string innerPressure = random.Next(10, 100).ToString();
            string humidity = random.Next(0, 100).ToString();
            string time = random.Next(1, 60).ToString() + "sec";

            string[] firstNames = { "Aria", "Liam", "Olivia", "Noah", "Ava", "Ethan", "Sophia", "Mason", "Isabella", "Logan" };
            string[] lastNames = { "Silverwood", "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez" };

            string randomFirstName = firstNames[random.Next(firstNames.Length)];
            string randomLastName = lastNames[random.Next(lastNames.Length)];


            string ExperResults = $"EXPERIMENT_RESULTS;DeltaSpeed:{deltaSpeed}|m/s;Temperature:{temp}|°C;Camera Speed:{cameraSpeed}|fps;Pressure:{innerPressure}|kPa;Humidity:{humidity}|%;{curentTime};{time};{randomFirstName}";
            MicroController.SendToClient(ExperResults);




        }
    }
}
