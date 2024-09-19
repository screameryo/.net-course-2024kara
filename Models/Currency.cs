namespace Models
{
    public class Currency
    {
        public Currency(string Name, string Code, string Symbol)
        {
            this.Name = Name;
            this.Code = Code;
            this.Symbol = Symbol;
        }

        /// <summary>
        /// Name of the currency
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Code of the currency
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Symbol of the currency
        /// </summary>
        public string Symbol { get; set; }
    }
}
