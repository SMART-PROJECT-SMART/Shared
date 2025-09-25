using Core.Models.ICDModels;

namespace Core.Services.ICDsDirectory
{
    public interface IICDDirectory
    {
        public List<ICD> GetAllICDs();
        public void LoadAllICDs();
        public int GetICDCount();
    }
}
