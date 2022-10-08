namespace dgPadCms.Models
{
    public class TaxonomyPostType
    {
        public int TaxonomyId { get; set; }
        public int PostTypeId { get; set; }

        public PostType PostType { get; set; }
        public Taxonomy Taxonomy { get; set; }
    }
}
