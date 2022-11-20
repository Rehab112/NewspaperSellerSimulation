﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
//using NewspaperSellerTesting;
using NewspaperSellerModels;

namespace NewspaperSellerSimulation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ReadFiles readFiles = new ReadFiles();
            readFiles.ReadFromWordDocument("D:\\Study\\4-th year First Term\\Modeling\\Task2\\Re7ab\\NewspaperSellerSimulation\\NewspaperSellerSimulation\\TestCases\\TestCase1.txt");
            readFiles.fillSimulationSystem();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
        }
    }
}
