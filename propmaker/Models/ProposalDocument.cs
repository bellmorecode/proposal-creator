namespace propmaker.Models 
{ 
    public sealed class ProposalDocument 
    {
        public ProposalDocument() 
        { 
        
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public DocumentHeader Header { get; set; } = new DocumentHeader();

        public List<DocumentSection> Sections { get; set; } = new List<DocumentSection>();

    } 
}