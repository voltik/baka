using System.Collections.Generic;

namespace PersTableColsConversion
{
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
}
