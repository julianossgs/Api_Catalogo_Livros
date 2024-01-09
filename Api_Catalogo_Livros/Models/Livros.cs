using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Api_Catalogo_Livros.Models
{
    [Table("Livros")]
    [Index(nameof(Nome), Name = "Ix_Nome")]
    public class Livros
    {
        [Key]
        public int LivroId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Nome do Livro")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Máximo de 60 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        [DisplayFormat(DataFormatString = "yyyy")]
        public string Ano_Publicacao { get; set; }

        public int AutorId { get; set; }//fk

        [JsonIgnore]
        public Autores Autores { get; set; }

        public int EditoraId { get; set; }//fk

        [JsonIgnore]
        public Editoras Editoras { get; set; }
    }
}
