using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shared.Common;
using Shared.Configuration;
using Shared.Models.ICDModels;

namespace Shared.Services.ICDDirectory
{
    public sealed class ICDDirectory : IICDDirectory
    {
        private readonly List<ICD> _icds = new List<ICD>();
        private readonly string _directoryPath;
        private static ICDDirectory? _instance;
        private static readonly object _lock = new object();

        private ICDDirectory(IOptions<ICDSettings> opts)
        {
            _directoryPath = !string.IsNullOrWhiteSpace(opts.Value.ICDFilePath)
                ? opts.Value.ICDFilePath
                : throw new DirectoryNotFoundException();
        }

        public static ICDDirectory GetInstance(IOptions<ICDSettings> opts)
        {
            if (_instance is not (null and null)) return _instance;
            _instance = new ICDDirectory(opts);
                _instance.LoadAllICDs();
                return _instance;
        }

        public void LoadAllICDs()
        {
            if (_icds.Count > 0) return;

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
            var icd = new ICD(icdFields);
            _icds.Add(icd);
        }

        public List<ICD> GetAllICDs()
        {
            return _icds;
        }
    }
}
