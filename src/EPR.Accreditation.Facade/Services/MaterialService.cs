using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.Enums;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services.Interfaces;

namespace EPR.Accreditation.Facade.Services
{
    public class MaterialService : IMaterialService
    {
        protected readonly IHttpMaterialService _materialService;

        public MaterialService(IHttpMaterialService materialService)
        {
            _materialService = materialService ?? throw new ArgumentNullException(nameof(materialService));
        }

        public async Task<IEnumerable<Material>> GetAll(Language language)
        {
            var allMaterials = await _materialService.GetAllMaterials();

            return allMaterials
                .Select(m => new Material
                {
                    Id = m.Id,
                    Name = language == Language.English ? m.English : m.Welsh
                });
        }

        public async Task<string> GetName(
            int id, 
            Language language)
        {
            var allMaterials = await _materialService.GetAllMaterials();

            return allMaterials
                .Where(m => m.Id == id)
                .Select(m => language == Language.English ? m.English : m.Welsh)
                .FirstOrDefault();
        }
    }
}
