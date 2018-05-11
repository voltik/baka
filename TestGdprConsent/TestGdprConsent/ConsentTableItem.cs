namespace Bakalari.Data.Clients.GDPR.Model
{
    /// <summary>
    /// Souhlas s položkou tabulky
    /// </summary>
    internal class ConsentTableItem 
    {
        /// <summary>
        /// Tabulka
        /// </summary>
        public string Table { get; set; }
        /// <summary>
        /// Sloupec v tabulce
        /// </summary>
        public string Column { get; set; }
    }
}
