using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuantityMeasurementAppModels.Entities
{
    [Table("users_authenication_and_authorization")]
    public class PersonEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("entity_id")]
        public long EntityId { get; set; }

        [Required]
        [Column("entity_email")]
        public string EntityEmail { get; set; } = string.Empty;

        [Required]
        [Column("entity_name")]
        public string EntityName { get; set; } = string.Empty;

        // BCrypt hashed password - never store plain text passwords
        [Required]
        [Column("entity_hash_password")]
        public string EntityHashPassword { get; set; } = string.Empty;

        [Column("entity_created_at")]
        public DateTime EntityCreatedAt { get; set; } = DateTime.UtcNow;

        [Column("entity_last_active_at")]
        public DateTime EntityLastActiveAt { get; set; } = DateTime.UtcNow;
    }
}