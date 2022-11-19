using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperSellerModels
{
    public class SimulationSystem
    {
        public SimulationSystem()
        {
            DayTypeDistributions = new List<DayTypeDistribution>();
            DemandDistributions = new List<DemandDistribution>();
            SimulationTable = new List<SimulationCase>();
            PerformanceMeasures = new PerformanceMeasures();
        }
        ///////////// INPUTS /////////////
        public int NumOfNewspapers { get; set; }
        public int NumOfRecords { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal ScrapPrice { get; set; }
        public decimal UnitProfit { get; set; }
        public List<DayTypeDistribution> DayTypeDistributions { get; set; }
        public List<DemandDistribution> DemandDistributions { get; set; }
        
        ///////////// OUTPUTS /////////////
        public List<SimulationCase> SimulationTable { get; set; }
        public PerformanceMeasures PerformanceMeasures { get; set; }


        ///////////// MY FUNCTIONS /////////////
        void calcDayTypeRange(List<DayTypeDistribution> DayTypeDistribution)
        {
            decimal sum = 0;
            int minRnage, maxRange = 0;
            for (int i = 0; i < DayTypeDistribution.Count; i++)
            {
                sum += DayTypeDistribution[i].Probability;
                DayTypeDistribution[i].CummProbability = sum;
                minRnage = maxRange + 1;
                DayTypeDistribution[i].MinRange = minRnage;
                maxRange = (int)(DayTypeDistribution[i].CummProbability * 100);
                if(i == DayTypeDistribution.Count - 1)
                    maxRange = 0;
                DayTypeDistribution[i].MaxRange = maxRange;
            }
        }

        void getDemandDestributions()
        {
            for(int i = 0; i < DemandDistributions.Count; i++)
            {
                calcDayTypeRange(DemandDistributions[i].DayTypeDistributions);
            }
        }
    }
}
