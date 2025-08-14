using Shared.Models.ICDModels;

namespace Shared.Services.ICDsDirectory
{
    public interface IICDDirectory
    {
        public List<ICD> GetAllICDs();
        public void LoadAllICDs();
        public ICD GetPortsICD(int portNumber);
    }
}
