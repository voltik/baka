using Bakalari.Data.Clients.GDPR.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TestGdprConsent
{
    class Program
    {
        static void Main(string[] args)
        {
            var cd = new ConsentTemplateDetail()
            {
                Guid = Guid.NewGuid(),
                Title = "Testovaci vzor souhlasu",
                PersonalData = "Osobni data",
                UsagePurpose = "Duvod pouziti",
                Body = "Telo souhlasu",
                Validity = ConsentValidity.Validity,
                Note = "Poznamka",
                IsPersonalDataReadOnly = true,
                IsDefault = false,
                ConsentTableItemSet = new List<ConsentTableItem>()
            };
            cd.ConsentTableItemSet.Add(new ConsentTableItem() { Table = "tabulka1", Column = "sloupec1" });
            cd.ConsentTableItemSet.Add(new ConsentTableItem() { Table = "tabulka2", Column = "sloupec2" });
            var cdStr = JsonConvert.SerializeObject(cd);

            cd = new ConsentTemplateDetail()
            {
                Guid = Guid.NewGuid(),
                Title = "Obecný vzor pro založení nového souhlasu",
                //PersonalData = "Osobni data",
                //UsagePurpose = "Duvod pouziti",
                Body = "Tento souhlas můžete kdykoliv odvolat a my Vaše osobní údaje smažeme, pokud to bude možné a výmaz nebude v rozporu s našimi jinými povinnostmi či oprávněnými zájmy. Při splnění požadavků dle čl. 15 až 18 GDPR máte právo na přístup, opravu nebo výmaz Vašich osobních údajů, a dále právo na to, abychom omezili zpracování osobních údajů týkajících se Vaší osoby. Dále máte právo podat stížnost u našeho pověřence nebo u Úřadu pro ochranu osobních údajů, pokud se domníváte, že zpracování Vašich osobních údajů je prováděno v rozporu s GDPR.",
                Validity = ConsentValidity.EndOfStudy,
                Note = "Slouží pro zakládání nového souhlasu pro předvyplnění základních informací.",
                //IsPersonalDataReadOnly = true,
                IsDefault = true,
                RecipientType = BakaUserType.undefined,
                ConsentTableItemSet = new List<ConsentTableItem>()
            };
            cdStr = JsonConvert.SerializeObject(cd);

            cd = new ConsentTemplateDetail()
            {
                Guid = Guid.NewGuid(),
                Title = "Zobrazení fotografií na webu školy",
                PersonalData = "jméno, příjmení, fotografie ze školních akcí",
                UsagePurpose = "propagace školy na webových stránkách školy",
                Body = "Tento souhlas můžete kdykoliv odvolat a my Vaše osobní údaje smažeme, pokud to bude možné a výmaz nebude v rozporu s našimi jinými povinnostmi či oprávněnými zájmy. Při splnění požadavků dle čl. 15 až 18 GDPR máte právo na přístup, opravu nebo výmaz Vašich osobních údajů, a dále právo na to, abychom omezili zpracování osobních údajů týkajících se Vaší osoby. Dále máte právo podat stížnost u našeho pověřence nebo u Úřadu pro ochranu osobních údajů, pokud se domníváte, že zpracování Vašich osobních údajů je prováděno v rozporu s GDPR.",
                Validity = ConsentValidity.EndOfStudy,
                Note = "Souhlas se zveřejněním fotografií na webových stránkách školy.",
                //IsPersonalDataReadOnly = true,
                IsDefault = true,
                RecipientType = BakaUserType.student,
                ConsentTableItemSet = new List<ConsentTableItem>()
            };
            cd.ConsentTableItemSet.Add(new ConsentTableItem() { Table = "zaci", Column = "Jmeno" });
            cd.ConsentTableItemSet.Add(new ConsentTableItem() { Table = "zaci", Column = "Prijmeni" });
            cdStr = JsonConvert.SerializeObject(cd);
        }
    }
}
