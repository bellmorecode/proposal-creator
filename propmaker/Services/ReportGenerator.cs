namespace propmaker.Services {
    public sealed class ReportGenerator
    {
        private readonly string cs;
        public ReportGenerator(IConfiguration cfg)
        {
            cs = cfg.GetValue<string>("bcstore");
        }
    }
}