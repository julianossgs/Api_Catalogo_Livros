using Api_Catalogo_Livros.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Catalogo_Livros.Models
{
    [Table("Autores")]
    [Index(nameof(Nome), Name = "Ix_Nome")]
    public class Autores
    {
        public Autores()
        {
            Livros = new List<Livros>();
        }

        [Key]
        public int AutorId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Nome do Autor")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Máximo de 60 caracteres")]
        [UpperCase]
        public string Nome { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um email válido!")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Informe um Telefone válido!")]
        public string Telefone { get; set; }

        //prop de navegação 1 autor pode ter 1 coleção de livros
        public ICollection<Livros> Livros { get; set; }
    }
}
