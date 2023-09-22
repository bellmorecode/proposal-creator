namespace propmaker.Models 
{ 
    public sealed class DocumentSection 
    {
        public DocumentSection() { }

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public int SortOrder { get; set; } = 1;
    } 
}