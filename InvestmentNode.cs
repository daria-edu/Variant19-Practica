namespace InvestmentPortfolioApp
{
    public class InvestmentNode
    {
        public InvestmentPortfolio Data { get; set; }

        public InvestmentNode? Next { get; set; }

        public InvestmentNode? Prev { get; set; }

        public InvestmentNode(InvestmentPortfolio data)
        {
            Data = data;
        }
    }
}