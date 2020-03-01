using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Desafio_Dominio
{
    public class VO_Enquete
    {
        public int poll_id { get; set; }

        [DisplayName("Pergunta da Enquete")]
        [Required(ErrorMessage = "Preencha a Pergunta da Enquete!")]
        public string poll_description { get; set; }
        public List<VO_Enquete_Opcao> options { get; set; }
    }
}