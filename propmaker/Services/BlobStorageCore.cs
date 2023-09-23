using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;

namespace propmaker.Services
{
    internal class BlobStorageCore
    {
        protected async Task<BlobContainerClient> GetContainer(string name)
        {
            var client = new BlobContainerClient(ConnectionString, name);
            await client.CreateIfNotExistsAsync();
            return client;
        }
        protected async Task<BlobClient> GetBlob(string container, string name)
        {
            var blob = new BlobClient(ConnectionString, container, name);
            return await Task.FromResult(blob);
        }
        protected IEnumerable<Azure.Page<BlobItem>> GetFindBlobsInContainer(string container, string prefix)
        {
            var con = new BlobContainerClient(ConnectionString, container);
            var pageable = con.GetBlobs(BlobTraits.None, BlobStates.None, prefix);
            return pageable.AsPages();
        }

        protected string ConnectionString { get; set; } = "";
        protected void ExtractAccountNameAndKey()
        {
            var props = ConnectionString
                        .Split(';')
                        .Select(part => {
                            var eq = part.IndexOf('=');
                            if (eq > -1) { return new string[] { part.Substring(0, eq), part.Substring(eq + 1) }; }
                            return new string[] { part };
                        }).Select(arr => new { Name = arr[0], Value = arr[1] });

            if (props != null) {
                AccountName = props.FirstOrDefault(q => q.Name == "AccountName")?.Value ?? "";
                AccountKey = props.FirstOrDefault(q => q.Name == "AccountKey")?.Value ?? "";
            } 
        }
        protected string AccountName { get; set; } = "";
        protected string AccountKey { get; set; } = "";
    }
}