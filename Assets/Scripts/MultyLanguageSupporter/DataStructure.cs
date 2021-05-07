using CsvHelper.Configuration;

namespace MultyLanguageSupporter
{
    public class DataStructure
    {
        public string Key;
        public string Value;
    }

    public class DataStructureMap : ClassMap<DataStructure>
    {
        public string Key;
        public string Value;

        public DataStructureMap()
        {
            Map(m => m.Key).Index(0);
            Map(m => m.Value).Index(1);
        }
    }
}