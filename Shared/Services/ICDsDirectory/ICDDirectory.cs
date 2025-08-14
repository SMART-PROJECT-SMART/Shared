using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shared.Common;
using Shared.Configuration;
using Shared.Models.ICDModels;

namespace Shared.Services.ICDsDirectory
{
    public class ICDDirectory : IICDDirectory
    {
        private readonly List<ICD> _icds = new List<ICD>();
        private readonly string _directoryPath;

        public ICDDirectory(IOptions<ICDSettings> opts)
        {
            _directoryPath = !string.IsNullOrWhiteSpace(opts.Value.ICDFilePath)
                ? opts.Value.ICDFilePath
                : throw new DirectoryNotFoundException();
            LoadAllICDs();
        }

        public void LoadAllICDs()
        {
            var jsonFiles = Directory.GetFiles(
                _directoryPath,
                SharedConstants.Config.JSON_SEARCH_PATTERN,
                SearchOption.TopDirectoryOnly
            );
            foreach (var filePath in jsonFiles)
            {
                LoadICD(filePath);
            }
        }

        public ICD GetPortsICD(int portNumber)
        {
            int index = (portNumber - SharedConstants.Ports.BASE_PORT) % _icds.Count;
            return _icds[index];
        }

        private void LoadICD(string fullFilePath)
        {
            var fileJson = File.ReadAllText(fullFilePath);
            var icdFields = JsonConvert.DeserializeObject<List<ICDItem>>(fileJson);
            var icd = new ICD(icdFields);
            _icds.Add(icd);
        }

        public List<ICD> GetAllICDs()
        {
            return _icds;
        }
    }
}