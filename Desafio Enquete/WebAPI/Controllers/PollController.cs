using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Desafio_BLL;
using Desafio_Dominio;
using System;

namespace WebAPI.Controllers
{
    [Route("api/poll")]
    [ApiController]
    public class PollController : ControllerBase
    {
        //********************Listar Todas Enquetes (Com toda informações) ********************\\
        //Informações:
        //    poll_id
        //    poll_description
        //    views
        //    options
        //        poll_id
        //        option_id
        //        option_description
        //        votes
        // GET: api/Poll
        [HttpGet]
        public ActionResult<List<TB_Enquete>> GetAll()
        {

            return new Enquete().ListarTodos();
        }

        //********************Listar Enquete por ID (Com toda informações) ********************\\
        //Informações:
        //    poll_id
        //    poll_description
        //    views
        //    options
        //        poll_id
        //        option_id
        //        option_description
        //        votes
        // GET: api/Poll/:id
        [HttpGet("{id}/edit", Name = "poll edit")]
        public ActionResult<TB_Enquete> GetById(int id)
        {
            return new Enquete().ListarPorID(id) ?? throw new Exception("404 Not Found.");
        }

        //********************Listar Enquete por ID (Com informações objetivas) ********************\\
        //Informações:
        //    poll_description
        //    options
        //        option_id
        //        option_description
        // GET: api/Poll/:id
        [HttpGet("{id}", Name = "poll")]
        public ActionResult<VO_Enquete> GetPollById(int id)
        {
            return new Enquete().ListPollById(id) ?? throw new Exception("404 Not Found.");
        }

        //********************Estatística********************\\
        //Informações:
        //    views
        //    votes
        //        option_id
        //        qty
        // GET: api/Poll/:id/Stats
        [HttpGet("{id}/Stats", Name = "Stats")]
        public VO_Parcial Stats(int id)
        {
            Enquete enquete = new Enquete();
            VO_Parcial parcial = new VO_Parcial();
            VO_Opcao objO = new VO_Opcao();
            List<VO_Opcao> objOList = new List<VO_Opcao> { };
            var obj = enquete.ListarStats(id);
            parcial.views = obj.views;
            for (int i = 0; i < 3; i++)
            {
                objO.option_id = obj.options[i].option_id;
                objO.qty = obj.options[i].votes;
                objOList.Add(objO);
                objO = new VO_Opcao();
            }
            parcial.votes = objOList;

            return parcial;
        }

        //********************Cadastrando********************\\
        // POST: api/Poll
        //[HttpPost]
        //public void Cadastrar([FromBody]TB_Enquete enquete)
        //{
        //    Enquete cadEnquete = new Enquete();
        //    cadEnquete.Inserir(enquete);
        //}
        [HttpPost]
        public VO_Enquete_Retorno Cadastrar([FromBody]VO_Enquete_Post enquete_post)
        {
            TB_Enquete enquete = new TB_Enquete();
            VO_Enquete_Retorno retorno = new VO_Enquete_Retorno();
            enquete.poll_description = enquete_post.poll_description;
            TB_Opcao objO = new TB_Opcao();
            List<TB_Opcao> objOList = new List<TB_Opcao> { };
            for (int i = 0; i < 3; i++)
            {
                objO.option_description = enquete_post.options[i];
                objO.option_id = i + 1;
                objOList.Add(objO);
                objO = new TB_Opcao();
            }
            enquete.options = objOList;
            Enquete cadEnquete = new Enquete();
            retorno.poll_id = cadEnquete.Inserir(enquete);
            return retorno;
        }

        //********************Votando********************\\
        // POST: api/poll/:id/vote
        [HttpPost("{id}/vote/", Name = "vote")]
        public void Vote(int id, [FromBody]VO_Voto voto)
        {
            Enquete cadEnquete = new Enquete();
            TB_Opcao opcao = new TB_Opcao();
            opcao.poll_id = id;
            if (voto.option_id > 3 || voto.option_id < 1)
            {
                voto.option_id = 1;
            }
            opcao.option_id = voto.option_id;
            cadEnquete.Votar(opcao);
        }

        //********************Edição********************\\
        // PUT: api/Poll/5
        [HttpPut("{id}")]
        public void Put([FromBody] TB_Enquete enquete)
        {
            var appEnquete = new Enquete();
            appEnquete.Alterar(enquete);
        }

        //********************Exclusão********************\\
        // DELETE: api/poll/5/delete
        [HttpDelete("{id}/delete")]
        public void Excluir(int id)
        {
            var excEnquete = new Enquete();
            excEnquete.Excluir(id);
        }
    }
}
