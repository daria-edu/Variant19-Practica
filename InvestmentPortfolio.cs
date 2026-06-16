namespace InvestmentPortfolioApp
{
    public enum InvestmentType
    {
        Stocks,
        Bonds,
        RealEstate,
        Cryptocurrency
    }

    public class InvestmentPortfolio
    {
        public InvestmentType InvestmentType { get; set; }

        public double ReturnRate { get; set; }

        public bool HighRisk { get; set; }

        public InvestmentPortfolio()
        {
        }

        public InvestmentPortfolio(
            InvestmentType investmentType,
            double returnRate,
            bool highRisk)
        {
            InvestmentType = investmentType;
            ReturnRate = returnRate;
            HighRisk = highRisk;
        }

        public override string ToString()
        {
            return $"{InvestmentType,-15} {ReturnRate,-10} {HighRisk}";
        }
    }
}