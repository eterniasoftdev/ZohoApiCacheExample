namespace ApiCacheExample
{
    public class ResultTemplate
    {

        public IEnumerable<DataTemplate> Oppurtunities { get; }
        public ResultTemplate(IEnumerable<DataTemplate> oppurtunities)
        {
            Oppurtunities = oppurtunities;
        }
    }
}
