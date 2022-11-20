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

        //int demandMapping(int rand, Enums.DayType dayType)
        //{
        //    for(int i = 0; i < DemandDistributions.Count; i++)
        //    {
        //        for (int j = 0; j < DemandDistributions[i].DayTypeDistributions.Count; j++)
        //        {
        //            if(dayType == DemandDistributions[i].DayTypeDistributions[j].DayType)
        //            {
        //                if (rand >= DemandDistributions[i].DayTypeDistributions[j].MinRange && rand <= DemandDistributions[i].DayTypeDistributions[j].MaxRange)
        //                {

        //                    return DemandDistributions[i].Demand;
        //                }
        //            }
        //        }
        //    }
        //}



        public Enums.DayType dayTypeMapping(int rand, List<DayTypeDistribution> DayTypeDistribution)
        {
            for (int i = 0; i < DayTypeDistribution.Count - 1; i++)
            {
                if (rand >= DayTypeDistribution[i].MinRange && rand <= DayTypeDistribution[i].MaxRange)
                {

                    return DayTypeDistribution[i].DayType;
                }
            }

            return DayTypeDistribution[DayTypeDistribution.Count - 1].DayType;
        }

        //public void calcDayTypeRange(List<DayTypeDistribution> DayTypeDistribution)
        //{
        //    decimal sum = 0;
        //    int minRnage, maxRange = 0;
        //    for (int i = 0; i < DayTypeDistribution.Count; i++)
        //    {
        //        sum += DayTypeDistribution[i].Probability;
        //        DayTypeDistribution[i].CummProbability = sum;
        //        minRnage = maxRange + 1;
        //        DayTypeDistribution[i].MinRange = minRnage;
        //        maxRange = (int)(DayTypeDistribution[i].CummProbability * 100);
        //        if(i == DayTypeDistribution.Count - 1)
        //            maxRange = 0;
        //        DayTypeDistribution[i].MaxRange = maxRange;
        //    }
        //}

        public void calcDayTypeRange(List<DayTypeDistribution> DayTypeDistribution)
        {
            decimal sum = 0;
            int minRnage, maxRange = 0;
            bool oneCumm = false;
            for (int i = 0; i < DayTypeDistribution.Count; i++)
            {
                sum += DayTypeDistribution[i].Probability;
                DayTypeDistribution[i].CummProbability = sum;
                minRnage = maxRange + 1;
                DayTypeDistribution[i].MinRange = minRnage;
                if(!oneCumm)
                    maxRange = (int)(DayTypeDistribution[i].CummProbability * 100);
                if (sum >= 1)
                {
                    maxRange = 0;
                    oneCumm = true;
                }
                DayTypeDistribution[i].MaxRange = maxRange;
            }
        }

        // not complete 
        public void getDemandDestributions(List<DemandDistribution> DemandDistribution)
        {
            int minRnage, maxRange = 0;

            for (int i = 0; i < DemandDistribution.Count; i++)
            {
                DemandDistribution[i].DayTypeDistributions[0].MinRange = 1;
                for (int j = 1; j < DemandDistribution[i].DayTypeDistributions.Count; j++)
                {
                    minRnage = DemandDistribution[i].DayTypeDistributions[j - 1].MaxRange + 1;
                    DemandDistribution[i].DayTypeDistributions[j].MinRange = minRnage;
                    maxRange = (int)(DemandDistribution[i].DayTypeDistributions[j].Probability * 100);

                    DemandDistribution[i].DayTypeDistributions[j].MaxRange = maxRange;
                }
            }
        }
    }
}
