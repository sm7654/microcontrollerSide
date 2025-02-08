using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

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
            string curentTime = dateOnly.ToString("yyyy-MM-dd");
            string deltaSpeed = "5";
            string temp = "21";
            string CameraSpeed = "14";
            string innerPressure = "31";
            string humidity = "40";
            string Time = "47sec";



            string ExperResults = $"EXPERIMENT_RESULTS;DeltaSpeed:{deltaSpeed}m/s;Temperature:{temp}deg;Camera Speed:{CameraSpeed}m/s;Inner Pressure:{innerPressure}psi;Humidity:{humidity}deg;{curentTime};{Time};energy effieceint #5";
            MicroController.SendToClient(ExperResults);




        }
    }
}
