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
            

            string deltaSpeed = "13";
            string temp = "21";
            string humidity = "13";

            string ExperResults = $"EXPERIMENT_RESULTS;DeltaSpeed:{deltaSpeed};Temperature:{temp};Humidity:{humidity};Test #3";
            MicroController.SendToClient(ExperResults);




        }
    }
}
