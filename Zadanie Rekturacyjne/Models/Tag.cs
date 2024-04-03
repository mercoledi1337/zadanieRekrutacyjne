namespace Zadanie_Rekturacyjne.Models
{
    public class Tag
    {
    }
    public class Rootobject
    {
        public Item[] Items { get; set; }
        public bool Has_more { get; set; }
        public int Quota_max { get; set; }
        public int Quota_remaining { get; set; }
    }

    

   

}
