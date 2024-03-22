using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.Enums;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface IMaterialService
    {
        public Task<string> GetName(int id, Language language);

        public Task<IEnumerable<Material>> GetAll(Language language);
    }
}
