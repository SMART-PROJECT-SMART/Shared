using Core.Common;
using Core.Configuration;
using Core.Models.ICDModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Core.Services.ICDsDirectory
{
    public class ICDDirectory : IICDDirectory
    {
        private readonly List<ICD> _icds = new List<ICD>();
        private readonly string _directoryPath;
        private int _nextId = 1;

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


        private void LoadICD(string fullFilePath)
        {
            var fileJson = File.ReadAllText(fullFilePath);
            var icdFields = JsonConvert.DeserializeObject<List<ICDItem>>(fileJson);

            var fileName = Path.GetFileName(fullFilePath);
            var icd = new ICD(icdFields, fileName, _nextId++);

            _icds.Add(icd);
        }

        public List<ICD> GetAllICDs()
        {
            return _icds;
        }

        public int GetICDCount()
        {
            return _icds.Count;
        }
    }
}