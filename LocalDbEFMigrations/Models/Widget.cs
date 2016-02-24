using System.ComponentModel.DataAnnotations.Schema;

namespace LocalDbEFMigrations.Models
{
    public class Widget
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Sum { get; set; }
    }
}