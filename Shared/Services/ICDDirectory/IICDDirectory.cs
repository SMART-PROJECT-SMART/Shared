using Shared.Models.ICDModels;

namespace Shared.Services.ICDDirectory
{
    public interface IICDDirectory
    {
        public List<ICD> GetAllICDs();
        public void LoadAllICDs();
    }
}
