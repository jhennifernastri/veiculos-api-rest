using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web_api.Models
{
    public class Veiculo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A marca do veiculo é obrigatória.")]
        [StringLength(50, ErrorMessage = "A Marca pode ter no máximo 50 caracteres.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O nome do veiculo é obrigatório.")]
        [StringLength(100, ErrorMessage = "O Nome pode ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O ano do modelo do veiculo é obrigatório.")]
        public int AnoModelo { get; set; }

        [Required(ErrorMessage = "A data de fabricação do veiculo é obrigatório.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataFabricacao { get; set; }

        [Required(ErrorMessage = "O valor do veiculo é obrigatório.")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Valor { get; set; }

        public string Opcionais { get; set; }

    }
}