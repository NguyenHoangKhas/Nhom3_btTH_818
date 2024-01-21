using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class PhieuNH
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Ngaynhap { get; set; }
        public int Soluong { get; set; }    
        public int Dongia { get; set; }
        [ForeignKey("User")]
        public int userName { get; set; }
        public virtual User? User { get; set; }
        [NotMapped]
        public int TongTien
        {
            get { return Dongia * Soluong; }
        }
    }
}
