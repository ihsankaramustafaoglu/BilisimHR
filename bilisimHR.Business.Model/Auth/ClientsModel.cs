using bilisimHR.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace bilisimHR.Business.Model.Auth
{
    public class ClientsModel : BaseModel<int>
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string Secret { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ApplicationTypes ApplicationType { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public int RefreshTokenLifeTime { get; set; }

        [MaxLength(100)]
        [Required]
        public string AllowedOrigin { get; set; }
    }
}
