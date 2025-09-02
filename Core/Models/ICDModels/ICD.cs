using System.Collections;
using Newtonsoft.Json;
using Shared.Common.Enums;
using System.Text.RegularExpressions;
using Shared.Common;

namespace Shared.Models.ICDModels
{
    public class ICD : IEnumerable<ICDItem>
    {
        [JsonProperty]
        public List<ICDItem> Document { get; set; } = new List<ICDItem>();
        public string FileName { get; set; }
        public CommunicationDataType DataType { get; set; }

        public ICD() { }

        public ICD(List<ICDItem> document, string fileName)
        {
            Document = document;
            FileName = fileName;
            DataType = ParseDataTypeFromFileName(fileName);
        }
        public ICD(List<ICDItem> document)
        {
            Document = document;
        }

        private static CommunicationDataType ParseDataTypeFromFileName(string fileName)
        {
            var match = Regex.Match(fileName, SharedConstants.Config.ICD_TYPE_PATTERN, RegexOptions.IgnoreCase);
            if (match.Success && Enum.TryParse<CommunicationDataType>(match.Groups[1].Value, true, out var type))
            {
                return type;
            }
            throw new ArgumentException($"Could not determine CommunicationDataType from file name: {fileName}");
        }

        public IEnumerator<ICDItem> GetEnumerator()
        {
            return Document.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int GetSizeInBites()
        {
            var lastICDItem = Document[^1];
            return lastICDItem.StartBitArrayIndex + lastICDItem.BitLength;
        }
    }
}