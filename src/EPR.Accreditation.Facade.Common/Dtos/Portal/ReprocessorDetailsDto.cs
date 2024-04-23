using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Facade.Common.Dtos.Portal
{
    public class ReprocessorDetailsDto
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public int? CountryId { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        public IEnumerable<Country> CountryList { get; set; }
    }
}
