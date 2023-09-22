using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using propmaker.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text;

namespace propmaker.Services 
{
    internal sealed class DataService : BlobStorageCore
    {
        private readonly string container = "propmaker";
        private readonly string filename = "all_proposals.json";

        internal DataService(IConfiguration cfg) 
        {
            ConnectionString = cfg.GetValue<string>("bcstore");
            ExtractAccountNameAndKey();
        }

        internal async Task<List<ProposalDocument>> GetProposals()
        {
            var list = new List<ProposalDocument>();

            try
            {
                var blob = await GetBlob(container, filename);
                if (await blob.ExistsAsync() == false)
                {
                    using var ms = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(list)));
                    _ = await blob.UploadAsync(content: ms, overwrite: true);
                } 
                else
                {
                    var downloadStream = new MemoryStream();
                    _ = await blob.DownloadToAsync(downloadStream);
                    downloadStream.Position = 0;
                    var json = new String(Encoding.UTF8.GetChars(downloadStream.ToArray()));
                    list = JsonConvert.DeserializeObject<List<ProposalDocument>>(json);
                }
                
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error in Get Proposals: {ex}");
            }

            return list;
        }

        internal bool SaveProposal(ProposalDocument document)
        {
            var result = false;
            try
            {
                if (document != null)
                {

                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error in Save Proposal: {ex}");
            }
            return result;
        }
    } 

    internal class BlobStorageCore
    {
        public async Task<BlobContainerClient> GetContainer(string name)
        {
            var client = new BlobContainerClient(ConnectionString, name);
            await client.CreateIfNotExistsAsync();
            return client;
        }
        public async Task<BlobClient> GetBlob(string container, string name)
        {
            var blob = new BlobClient(ConnectionString, container, name);
            return await Task.FromResult(blob);
        }
        public IEnumerable<Azure.Page<BlobItem>> GetFindBlobsInContainer(string container, string prefix)
        {
            var con = new BlobContainerClient(ConnectionString, container);
            var pageable = con.GetBlobs(BlobTraits.None, BlobStates.None, prefix);
            return pageable.AsPages();
        }
        protected string ConnectionString { get; set; }
        protected void ExtractAccountNameAndKey()
        {
            var props = ConnectionString
                        .Split(';')
                        .Select(part => {
                            var eq = part.IndexOf('=');
                            if (eq > -1) { return new string[] { part.Substring(0, eq), part.Substring(eq + 1) }; }
                            return new string[] { part };
                        }).Select(arr => new { Name = arr[0], Value = arr[1] });

            AccountName = props.FirstOrDefault(q => q.Name == "AccountName")?.Value;
            AccountKey = props.FirstOrDefault(q => q.Name == "AccountKey")?.Value;
        }
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
    }
}