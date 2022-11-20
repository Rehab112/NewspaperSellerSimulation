using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace NewspaperSellerModels
{
    public class ReadFiles
    {
        string[] Lines { get; set; }
        string[] dayTypeDistributions { get; set; }
        string Line;
        public SimulationSystem simulationSystem = new SimulationSystem();

        public void ReadFromWordDocument(string path)
        {
            Line = File.ReadAllText(path);
            Lines = Line.Split('\n');
        }

        public SimulationSystem fillSimulationSystem()
        {
            simulationSystem.NumOfNewspapers = int.Parse(Lines[1]);
            simulationSystem.NumOfRecords = int.Parse(Lines[4]);
            simulationSystem.PurchasePrice = decimal.Parse(Lines[7]);
            simulationSystem.ScrapPrice = decimal.Parse(Lines[10]);
            simulationSystem.SellingPrice = decimal.Parse(Lines[13]);
            
            dayTypeDistributions = Lines[16].Split(',');
            decimal good = decimal.Parse( dayTypeDistributions[0]);
            decimal fair = decimal.Parse( dayTypeDistributions[1].Split(' ')[1]);
            decimal poor = decimal.Parse( dayTypeDistributions[2].Split(' ')[1]);

            simulationSystem.DayTypeDistributions = new List<DayTypeDistribution>();
            DayTypeDistribution dayTypeDistribution = new DayTypeDistribution();
            dayTypeDistribution.DayType = Enums.DayType.Good;
            dayTypeDistribution.Probability = good;
            simulationSystem.DayTypeDistributions.Add(dayTypeDistribution);

            dayTypeDistribution = new DayTypeDistribution();
            dayTypeDistribution.DayType = Enums.DayType.Fair;
            dayTypeDistribution.Probability = fair;
            simulationSystem.DayTypeDistributions.Add(dayTypeDistribution);

            dayTypeDistribution = new DayTypeDistribution();
            dayTypeDistribution.DayType = Enums.DayType.Poor;
            dayTypeDistribution.Probability = poor;
            simulationSystem.DayTypeDistributions.Add(dayTypeDistribution);
            
            simulationSystem.calcDayTypeRange(simulationSystem.DayTypeDistributions);
            //Todo :: cout 

            Enums.DayType type = simulationSystem.dayTypeMapping(100, simulationSystem.DayTypeDistributions);
            

            simulationSystem.DemandDistributions = new List<DemandDistribution>();
            DemandDistribution demandDistribution;
            for (int i = 19; i < 26; i++)
            {
                demandDistribution = new DemandDistribution();
                demandDistribution.DayTypeDistributions = new List<DayTypeDistribution>();
                dayTypeDistributions = Lines[i].Split(',');
                demandDistribution.Demand = int.Parse(dayTypeDistributions[0]);
                good = decimal.Parse(dayTypeDistributions[1].Split(' ')[1]);
                fair = decimal.Parse(dayTypeDistributions[2].Split(' ')[1]);
                poor = decimal.Parse(dayTypeDistributions[3].Split(' ')[1]);

                dayTypeDistribution = new DayTypeDistribution();
                dayTypeDistribution.DayType = Enums.DayType.Good;
                dayTypeDistribution.Probability = decimal.Parse(dayTypeDistributions[1].Split(' ')[1]);
                demandDistribution.DayTypeDistributions.Add(dayTypeDistribution);

                dayTypeDistribution = new DayTypeDistribution();
                dayTypeDistribution.DayType = Enums.DayType.Fair;
                dayTypeDistribution.Probability = fair;
                demandDistribution.DayTypeDistributions.Add(dayTypeDistribution);

                dayTypeDistribution = new DayTypeDistribution();
                dayTypeDistribution.DayType = Enums.DayType.Poor;
                dayTypeDistribution.Probability = poor;
                demandDistribution.DayTypeDistributions.Add(dayTypeDistribution);

                simulationSystem.DemandDistributions.Add(demandDistribution);
            }

            simulationSystem.getDemandDestributions(simulationSystem.DemandDistributions);


            for (int i = 0; i < Lines.Length; i++)
            {
                Console.WriteLine("Line " + i.ToString() + " : " + Lines[i]);
            }
            Console.WriteLine("Simulatiiiiion");
            Console.WriteLine( simulationSystem.NumOfNewspapers);
            Console.WriteLine(simulationSystem.NumOfRecords);
            Console.WriteLine(simulationSystem.PurchasePrice );
            Console.WriteLine(simulationSystem.ScrapPrice);
            Console.WriteLine(simulationSystem.SellingPrice);

            

            for (int i = 0; i < simulationSystem.DayTypeDistributions.Count; i++)
            {
                Console.WriteLine(simulationSystem.DayTypeDistributions[i].DayType+",  "+ simulationSystem.DayTypeDistributions[i].Probability+",  "+ 
                    simulationSystem.DayTypeDistributions[i].CummProbability+",  "+ simulationSystem.DayTypeDistributions[i].MinRange+",  "+ 
                    simulationSystem.DayTypeDistributions[i].MaxRange);
            }

            

            for (int i = 0; i < simulationSystem.DemandDistributions.Count; i++)
            {
                Console.Write(simulationSystem.DemandDistributions[i].Demand);
                for (int j = 0; j < simulationSystem.DemandDistributions[i].DayTypeDistributions.Count; j++)
                {
                    Console.Write(",  "+simulationSystem.DemandDistributions[i].DayTypeDistributions[j].DayType+",  "+ simulationSystem.DemandDistributions[i].DayTypeDistributions[j].Probability
                        + ",  " + simulationSystem.DemandDistributions[i].DayTypeDistributions[j].MinRange+ ",  " + simulationSystem.DemandDistributions[i].DayTypeDistributions[j].MaxRange);
                }
                Console.WriteLine(" ");
            }

            if (type == Enums.DayType.Good)
                Console.WriteLine("goooooodddddd");
            else if (type == Enums.DayType.Poor)
                Console.WriteLine("Poooooooooorrrrrr");
            else Console.WriteLine("faaaaaiiiiiiirrrrrrrrr");


            return simulationSystem;
        }
    }
}
