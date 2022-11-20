using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperSellerModels
{
    public class MainClass
    {
        SimulationSystem simulationSystem = new SimulationSystem();
        SimulationCase simulationCase;
        


        public SimulationSystem simulationTableGenerator(SimulationSystem simulationSystem)
        {
            Random rnd = new Random();
            int numberOfPapers = simulationSystem.NumOfNewspapers;
            decimal salesPrice = simulationSystem.SellingPrice;
            decimal scrapPrice = simulationSystem.ScrapPrice;
            decimal PurchasePrice = simulationSystem.PurchasePrice;
            decimal dailycost = numberOfPapers * simulationSystem.PurchasePrice;

            decimal totalSalesProfit = 0;
            decimal totalLostProfit = 0;
            decimal totalScrapProfit = 0;
            decimal totalUnitProfit = 0;

            int NumOfDaysWithExcessDemand = 0;
            int NumOfDaysWithUnSoldPaper = 0;


            for (int i = 0; i < simulationSystem.NumOfRecords; i++)
            {
                simulationCase = new SimulationCase();
                
                simulationCase.DayNo = i+1;

                int number = rnd.Next(1, 101);
                simulationCase.RandomNewsDayType = number;

                /// MApping to Type of news paper : 
                /// Rehab CODE ::::
                //simulationCase.NewsDayType = ??

                int number2 = rnd.Next(1, 101);
                simulationCase.RandomDemand = number2;

                /// MApping to number of demanf : 
                /// Rehab CODE ::::
                //simulationCase.Demand = ??

                if (numberOfPapers > simulationCase.Demand)
                {
                    simulationCase.SalesProfit = salesPrice * simulationCase.Demand;
                    simulationCase.LostProfit = 0;
                    simulationCase.ScrapProfit = scrapPrice * (numberOfPapers - simulationCase.Demand);

                    NumOfDaysWithUnSoldPaper++;

                }
                else if (numberOfPapers < simulationCase.Demand)
                {
                    int numOfLost = simulationCase.Demand - numberOfPapers;
                    simulationCase.SalesProfit = salesPrice * simulationCase.Demand;
                    simulationCase.LostProfit = (salesPrice * numOfLost) + (PurchasePrice * numOfLost);
                    simulationCase.ScrapProfit = 0;
                    NumOfDaysWithExcessDemand++;

                }
                else
                {
                    simulationCase.SalesProfit = salesPrice * simulationCase.Demand;
                    simulationCase.LostProfit = 0;
                    simulationCase.ScrapProfit = 0;


                }

                simulationCase.DailyCost = dailycost;
                simulationCase.DailyNetProfit = simulationCase.SalesProfit - simulationCase.DailyCost
                    - simulationCase.LostProfit + simulationCase.ScrapProfit;


                totalSalesProfit += simulationCase.SalesProfit;
                totalLostProfit += simulationCase.LostProfit;
                totalScrapProfit += simulationCase.ScrapProfit;
                totalUnitProfit += simulationCase.DailyNetProfit;

                this.simulationSystem.SimulationTable.Add(simulationCase);
                

            }
            this.simulationSystem.PerformanceMeasures.DaysWithMoreDemand = NumOfDaysWithExcessDemand;
            this.simulationSystem.PerformanceMeasures.DaysWithUnsoldPapers = NumOfDaysWithUnSoldPaper;
            this.simulationSystem.PerformanceMeasures.TotalCost = dailycost * simulationSystem.NumOfRecords;
            this.simulationSystem.PerformanceMeasures.TotalLostProfit =totalLostProfit;
            this.simulationSystem.PerformanceMeasures.TotalSalesProfit =totalSalesProfit;
            this.simulationSystem.PerformanceMeasures.TotalScrapProfit =totalScrapProfit;
            this.simulationSystem.PerformanceMeasures.TotalNetProfit =totalUnitProfit;
            return this.simulationSystem;
        }
    }
}
