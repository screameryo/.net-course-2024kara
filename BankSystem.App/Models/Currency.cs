namespace Models
{
    public class Currency
    {
        public Currency(string Name, string Code, string Symbol, int NumCode)
        {
            this.Name = Name;
            this.Code = Code;
            this.Symbol = Symbol;
            this.NumCode = NumCode;
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
        /// Number code of the currency
        /// </summary>
        public int NumCode { get; set; }

        /// <summary>
        /// Symbol of the currency
        /// </summary>
        public string Symbol { get; set; }
    }
}
