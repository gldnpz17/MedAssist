using MedAssistBusinessLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace InputMedicationConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MedAssistBLLibrary lib = new MedAssistBLLibrary();
            Console.WriteLine("MedAssist Medication Input Program");
            while (true)
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Description: ");
                string description = Console.ReadLine();
                Console.Write("Image Path(*.png): ");
                string imgPath = Console.ReadLine();
                Console.Write("Price: ");
                decimal price = Convert.ToDecimal(Console.ReadLine());

                bool success = true;
                try
                {
                    lib.Medication.AddMedication(name, description, ConvertPngImageToByteArray(imgPath), price);
                }
                catch(Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;

                    success = false;
                }

                if (success)
                {
                    Console.WriteLine("Entry Added");
                }
            }

            byte[] ConvertPngImageToByteArray(string path)
            {
                BitmapImage image = new BitmapImage();

                image.BeginInit();
                image.UriSource = new Uri(path);
                image.DecodePixelWidth = 300;
                image.EndInit();

                byte[] output;
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    output = ms.ToArray();
                }

                return output;
            }
        }
    }
}
