namespace propmaker.Models 
{ 
    public sealed class DocumentHeader 
    {
        public DocumentHeader()
        {

        }

        public string Filename { get; set; } = "proposal.docx";

        public string Title { get; set; } = string.Empty;

        public string Overview { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        public string CustomerEmail { get; set; } = string.Empty;

        public string CustomerContactName { get; set; } = string.Empty;
    } 
}