using System.Collections.Generic;

namespace Web_UI.Models
{
    public class VO_ParcialModal
    {
        public int poll_id { get; set; }
        public int views { get; set; }

        public string poll_description { get; set; }

        public List<TB_Opcao> options { get; set; }
    }
}