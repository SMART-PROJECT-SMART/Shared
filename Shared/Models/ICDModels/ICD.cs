using System.Collections;
using Newtonsoft.Json;

namespace Shared.Models.ICDModels
{
    public class ICD : IEnumerable<ICDItem>
    {
        [JsonProperty]
        public List<ICDItem> Document { get; set; } = new List<ICDItem>();
        public string FileName { get; set; }


        public ICD() { }

        public ICD(List<ICDItem> document,string fileName)
        {
            Document = document;
            FileName = fileName;
        }
        public ICD(List<ICDItem> document)
        {
            Document = document;
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