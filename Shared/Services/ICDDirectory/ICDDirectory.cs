using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Common;
using Shared.Models.ICDModels;

namespace Shared.Services.ICDDirectory
{
    public sealed class ICDDirectory : IICDDirectory
    {
        private readonly List<ICD> _icds = new List<ICD>();
        private readonly string _directoryPath;
        private static ICDDirectory? _instance;

        private static readonly string DEFAULT_ICD_PATH = @"C:\Users\jonat\Desktop\IAF\projects\SMART\Shared\Shared\Files\ICD\";

        private ICDDirectory(string directoryPath)
        {
            _directoryPath = directoryPath;
        }

        public static ICDDirectory GetInstance()
        {
            if (_instance != null) return _instance;

            if (!Directory.Exists(DEFAULT_ICD_PATH))
            {
                throw new DirectoryNotFoundException($"ICD directory does not exist: {DEFAULT_ICD_PATH}");
            }

            _instance = new ICDDirectory(DEFAULT_ICD_PATH);
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
            return _icds.ToList();
        }
    }
}