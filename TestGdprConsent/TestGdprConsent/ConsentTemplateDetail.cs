using System;
using System.Collections.Generic;

namespace Bakalari.Data.Clients.GDPR.Model
{
    public enum BakaUserType
    {
        none = -1,
        student = 0,
        parents = 1,
        mother = 2,
        father = 3,
        controller = 4,
        teacher = 5,
        /*director    = 6 (manažer - nepoužívá se) */
        supervisor = 7,
        founder = 8,
        administrator = 9,
        undefined = 10  // specialito pro odeslání všem
        /* controller = dohled */
    }

    public enum ConsentValidity
    {
        /// <summary>
        /// Do konce studia
        /// </summary>
        EndOfStudy = 1,
        /// <summary>
        /// Platnost od-do
        /// </summary>
        Validity = 2
    }


    /// <summary>
    /// Detail vzoru souhlasu
    /// </summary>
    internal class ConsentTemplateDetail 
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
        /// Osobní údaje
        /// </summary>
        public string PersonalData { get; set; }

        /// <summary>
        /// Účel použití
        /// </summary>
        public string UsagePurpose { get; set; }

        /// <summary>
        /// Tělo souhlasu - poučení
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Typ validity (od-do, do konce studia)
        /// </summary>
        public ConsentValidity Validity { get; set; }

        /// <summary>
        /// Poznámka
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Udává, zda jsou osobní údaje a účel použití jen pro čtení.
        /// </summary>
        public bool IsPersonalDataReadOnly { get; set; }

        /// <summary>
        /// Defaultní šablona pro nové souhlasy
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Typ příjemce
        /// </summary>
        public BakaUserType RecipientType { get; set; }

        /// <summary>
        /// Seznam položek souhlasu
        /// </summary>
        public List<ConsentTableItem> ConsentTableItemSet { get; set; }
    }
}
