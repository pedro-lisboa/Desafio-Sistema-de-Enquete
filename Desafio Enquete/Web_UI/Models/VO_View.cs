using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web_UI.Models
{
    public class VO_View
    {
        public int id { get; set; }

        [DisplayName("Pergunta da Enquete:")]
        [Required(ErrorMessage = "Preencha a Pergunta da Enquete!")]
        public string description { get; set; }

        [DisplayName("1ª Opção:")]
        [Required(ErrorMessage = "Preencha a 1ª Opção!")]
        public string option_1 { get; set; }
        [DisplayName("2ª Opção:")]
        [Required(ErrorMessage = "Preencha a 1ª Opção!")]
        public string option_2 { get; set; }
        [DisplayName("3ª Opção:")]
        [Required(ErrorMessage = "Preencha a 1ª Opção!")]
        public string option_3 { get; set; }

        public int views { get; set; }
        public int qty_1 { get; set; }
        public int qty_2 { get; set; }
        public int qty_3 { get; set; }
        public int option_vote { get; set; }
        

    }
}