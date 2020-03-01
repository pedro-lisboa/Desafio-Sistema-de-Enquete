using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Desafio_Dominio
{
    public class TB_Enquete
    {
        public int poll_id { get; set; }

        [DisplayName("Pergunta da Enquete")]
        [Required(ErrorMessage = "Preencha a Pergunta da Enquete!")]
        public string poll_description { get; set; }

        public int views { get; set; }

        public List<TB_Opcao> options { get; set; }
    }
}