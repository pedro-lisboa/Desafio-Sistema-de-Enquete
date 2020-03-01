using System;
using System.Collections.Generic;
using System.Linq;
using Desafio_Dominio;

namespace Desafio_BLL
{
    public class Enquete
    {
        public int Inserir(TB_Enquete enquete)
        {
            try
            {
                var objEnquete = new Desafio_DAL.Enquete();
                return objEnquete.Inserir(enquete);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Votar(TB_Opcao opcao)
        {
            try
            {
                var objEnquete = new Desafio_DAL.Enquete();
                objEnquete.Votar(opcao);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Alterar(TB_Enquete enquete)
        {
            try
            {
                var objEnquete = new Desafio_DAL.Enquete();
                objEnquete.Alterar(enquete);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public List<TB_Enquete> ListarTodos()
        //{
        //    var retorno = new DAL.Enquete();
        //    List<TB_Enquete> obj = retorno.ListarTodos();
        //    return obj;
        //}
        public List<TB_Enquete> ListarTodos()
        {
            try
            {
                var retorno = new Desafio_DAL.Enquete();
                var obj = retorno.ListarTodos();
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TB_Enquete ListarPorID(int id)
        {
            try
            {
                var retorno = new Desafio_DAL.Enquete();
                var obj = retorno.ListarPorId(id);
                return obj;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public VO_Enquete ListPollById(int id)
        {
            try
            {
                var retorno = new Desafio_DAL.Enquete();
                var obj = retorno.ListPollById(id);
                return obj;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public TB_Enquete ListarStats(int id)
        {
            try
            {
                var retorno = new Desafio_DAL.Enquete();
                var obj = retorno.ListarStats(id);
                return obj;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void Excluir(int id)
        {
            try
            {
                var excDados = new Desafio_DAL.Enquete();
                excDados.Excluir(id);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
