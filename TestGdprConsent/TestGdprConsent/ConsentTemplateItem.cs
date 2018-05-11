using System;

namespace Bakalari.Data.Clients.GDPR.Model
{
    /// <summary>
    /// Šablona souhlasu - zkácené informace
    /// </summary>
    internal class ConsentTemplateItem 
    {
        /// <summary>
        /// Unikátní identifikátor
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Předmět
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Poznámka
        /// </summary>
        public string Note { get; set; }
    }
}
