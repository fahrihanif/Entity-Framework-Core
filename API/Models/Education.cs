using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_education")]
    public class Education
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public string GPA { get; set; }
        [Required]
        public int UniversityId { get; set; }

        //Relation
        public virtual University University { get; set; }
        [JsonIgnore]
        public virtual Profiling Profiling { get; set; }
    }
}
