using propmaker.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text;
using System.Reflection.Metadata;

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

        internal async Task<bool> SaveProposal(ProposalDocument document)
        {
            var result = false;
            try
            {
                if (document != null)
                {
                    if (document.Id == Guid.Empty)
                    {
                        document.Id = Guid.NewGuid();
                    }
                    var list = await GetProposals();
                    ProposalDocument? target = list.FirstOrDefault(q => q.Id == document.Id);
                    if (target != null)
                    {
                        target.Header.Title = document.Header.Title;
                        target.Header.Overview = document.Header.Overview;
                        target.Header.CustomerName = document.Header.CustomerName;
                        target.Header.CustomerContactName = document.Header.CustomerContactName;
                        target.Header.CustomerEmail = document.Header.CustomerEmail;
                        target.Header.Filename = document.Header.Filename;
                    }
                    else
                    {
                        list.Add(document);
                    }
                    var blob = await GetBlob(container, filename);
                    using var ms = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(list)));
                    _ = await blob.UploadAsync(content: ms, overwrite: true);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error in Save Proposal: {ex}");
            }
            return result;
        }
    } 
}