namespace BloomFilter
{
    class Program
    {
        private static void Main()
        {
            // Bloom filter declration and initialization
            BFilter<string> filter = new BFilter<string>(50000);

            // string array to add to filter
            string[] items = {
                "C#", "Ruby", "Scala", "Python", "JavaScript", "Node", "HTML5"
            };

            // add each item in above array
            foreach (var item in items)
            {
                filter.Add(item);
            }

            // test
            filter.Contains("C#");  // True
            filter.Contains("Node");    // True
            filter.Contains("left-pad");    // Most likely False
        }
    }
}
