using System.ComponentModel.DataAnnotations;

namespace Fernando_Musto_LaboratorioPracticodeWebApi.Models
{
    public class roles
    {
        [Key]
        public int rolId { get; set; }
        public string rol { get; set; }
    }
}
