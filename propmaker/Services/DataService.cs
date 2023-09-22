namespace propmaker.Services 
{ 
    public sealed class DataService 
    {
        private readonly string cs;
        public DataService(IConfiguration cfg) 
        {
            cs = cfg.GetValue<string>("bcstore");
        } 
    } 
}