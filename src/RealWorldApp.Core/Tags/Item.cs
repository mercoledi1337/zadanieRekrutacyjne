namespace RealWorldApp.Core.Tags
{
    public class Item
    {
        public bool has_synonyms { get; set; }
        public bool is_moderator_only { get; set; }
        public bool is_required { get; set; }
        public int count { get; set; }
        public string name { get; set; }
        public Collective[] collectives { get; set; }
    }

}
