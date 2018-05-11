using Bakalari.Interfaces.Gdpr;
using System;
using System.Collections.Generic;

namespace Bakalari.Data.Clients.GDPR.Model
{
    internal enum DocType
    {
        Folder = 0, SystemToDoList = 1, ReportDef = 2, ReportModel = 3, /* AdminToDoList = 4,*/ Questionnaire = 5,
        PersonalInfoTableDefinition = 6, ContentTemplate = 7
    };

    internal class DocDesc
    {
        public int docId { get; set; }
        public int type { get; set; }
        public bool paid { get; set; }
        public int? folderId { get; set; }
        public int? mainDocId { get; set; }
        public string name { get; set; }
        public string docSpec { get; set; }
    }

    internal class DocDescription : IDocDescription
    {
        public int DocId { get; set; }
        public bool IsPaid { get; set; }
        public string FolderName { get; set; }
        public string Name { get; set; }
        public string DocSpecific { get; set; }
    }

    internal class ReportDescription : IReportDescription
    {
        public int ReportDefinitionId { get; set; }
        public int ReportModelId { get; set; }
        public bool IsPaid { get; set; }
        public string FolderName { get; set; }
        public string Name { get; set; }
    }

    internal class GdprToDoList
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public List<GdprToDoSection> SectionSet { get; set; }
    }

    internal class GdprToDoSection
    {
        public string Name { get; set; }
        public Guid Guid { get; set; }
        public int Priority { get; set; }
        public string Number { get; set; }
        public List<GdprToDoCheck> CheckSet { get; set; }
    }

    internal class GdprToDoCheck
    {
        public string Name { get; set; }
        public Guid Guid { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
        public bool IsObsolete { get; set; }
        public string Action { get; set; }
        public string Number { get; set; }
    }

    internal class GdprTables
    {
        public List<GdprTableField> Records { get; set; }
    }

    internal class GdprTableField
    {
        public string Table { get; set; }
        public string Column { get; set; }
        public int CollectionPermision { get; set; }
        public string PermisionDescription { get; set; }
        public string Description { get; set; }
    }

    internal class GdprConsent
    {
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public string PersonalData { get; set; }
        public string UsagePurpose { get; set; }
        public string Body { get; set; }
        public ConsentValidity Validity { get; set; }
        public string Note { get; set; }
        public bool IsPersonalDataReadOnly { get; set; }
        public bool IsDefault { get; set; }
        public List<GdprConsentTableItem> ConsentTableItemSet { get; set; }
    }

    internal class GdprConsentTableItem
    {
        public string Table { get; set; }
        public string Column { get; set; }
    }
}
