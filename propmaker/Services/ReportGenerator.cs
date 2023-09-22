namespace propmaker.Services {
    internal sealed class ReportGenerator
    {
        private readonly string cs;
        internal ReportGenerator(IConfiguration cfg)
        {
            cs = cfg.GetValue<string>("bcstore");
        }
    }
}