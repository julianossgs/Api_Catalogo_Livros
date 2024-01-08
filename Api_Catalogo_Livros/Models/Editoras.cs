using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Catalogo_Livros.Models
{
    [Table("Editoras")]
    [Index(nameof(Nome), Name = "Ix_Nome")]
    public class Editoras
    {
        public Editoras()
        {
            Livros = new Collection<Livros>();
        }

        [Key]
        public int EditoraId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Nome da Editora")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Máximo de 60 caracteres")]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um email válido!")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Informe um Telefone válido!")]
        public string Telefone { get; set; }

        //prop de navegação 1 editora pode conter 1 coleção de livros
        public ICollection<Livros> Livros { get; set; }
    }
}
