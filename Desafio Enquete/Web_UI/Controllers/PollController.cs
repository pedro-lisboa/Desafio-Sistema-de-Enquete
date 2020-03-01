//using Newtonsoft.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Windows.Documents;
using Web_UI.Models;


namespace Web_UI.Controllers
{
    public class PollController : Controller
    {

        string Baseurl = "https://localhost:44371/api/poll/";

        //********************Index********************\\
        // GET: Poll
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            List<TB_Enquete> ListarTodos = null;
            List<VO_View> ListarTodosView = new List<VO_View> { };
            try
            {
                var handler = new WebRequestHandler();
                handler.ServerCertificateValidationCallback = delegate { return true; };
                using (HttpClient votoUi = new HttpClient(handler))
                {                    
                    votoUi.BaseAddress = new Uri(Baseurl);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    var resposta = await votoUi.GetAsync("");
                    string dados = await resposta.Content.ReadAsStringAsync();
                    ListarTodos = new JavaScriptSerializer().Deserialize<List<TB_Enquete>>(dados);
                }
                VO_View viewModel = new VO_View();
                foreach (TB_Enquete enquete in ListarTodos)
                {
                    viewModel.id = enquete.poll_id;
                    viewModel.description = enquete.poll_description;
                    viewModel.option_1 = enquete.options[0].option_description;
                    viewModel.option_2 = enquete.options[1].option_description;
                    viewModel.option_3 = enquete.options[2].option_description;
                    viewModel.views = enquete.views;
                    ListarTodosView.Add(viewModel);
                    viewModel = new VO_View();
                }
                return View(ListarTodosView);
            }
            catch (Exception)
            {
                return View();
            }

        }

        //********************Estatística********************\\
        // GET: Poll/5/Stats
        public async System.Threading.Tasks.Task<ActionResult> Stats(int id)
        {
            Baseurl = Baseurl + id + "/Stats";
            VO_Parcial obj = null;
            try
            { 

                var handler = new WebRequestHandler();
                handler.ServerCertificateValidationCallback = delegate { return true; };
                using (HttpClient votoUi = new HttpClient(handler))
                {
                    votoUi.BaseAddress = new Uri(Baseurl);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    votoUi.BaseAddress = new Uri(Baseurl);
                    var resposta = await votoUi.GetAsync("");
                    string dados = await resposta.Content.ReadAsStringAsync();

                    obj = JsonConvert.DeserializeObject<VO_Parcial>(dados.ToString());
                }
                VO_View objReturn = new VO_View();
                objReturn.qty_1 = obj.votes[0].qty;
                objReturn.qty_2 = obj.votes[1].qty;
                objReturn.qty_3 = obj.votes[2].qty;
                objReturn.views = obj.views;
                return View(objReturn);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //********************Cadastro********************\\
        // GET: Poll
        public async System.Threading.Tasks.Task<ActionResult> Cadastrar()
        {
            var model = new VO_View();
            var handler = new WebRequestHandler();
            handler.ServerCertificateValidationCallback = delegate { return true; };
            using (HttpClient tipovotoUi = new HttpClient(handler))
            {
                tipovotoUi.BaseAddress = new Uri(Baseurl);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                var resposta = await tipovotoUi.GetAsync("");
            }
            return View(model);
        }

        // POST: Poll
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Cadastrar(VO_View viewModel)
        {
            try
            {
                viewModel.id = 0;
                //// TODO: Add insert logic here
                var handler = new WebRequestHandler();
                handler.ServerCertificateValidationCallback = delegate { return true; };
                using (HttpClient enqueteUi = new HttpClient(handler))
                {
                    enqueteUi.BaseAddress = new Uri(Baseurl);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    var resposta = await enqueteUi.GetAsync("");
                    VO_Enquete_Post enquete_post = new VO_Enquete_Post();
                    enquete_post.poll_description = viewModel.description;
                    enquete_post.options = new string[3];
                    enquete_post.options[0] = viewModel.option_1;
                    enquete_post.options[1] = viewModel.option_2;
                    enquete_post.options[2] = viewModel.option_3;
                    var serializedEnquete = JsonConvert.SerializeObject(enquete_post);
                    var content = new StringContent(serializedEnquete, Encoding.UTF8, "application/json");

                    var result = await enqueteUi.PostAsync(Baseurl, content);
                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //********************Edição********************\\
        // GET: Poll/Edit/5
        public async System.Threading.Tasks.Task<ActionResult> Editar(int id)
        {

            TB_Enquete Listar = null;
            List<VO_View> ListarTodosView = new List<VO_View> { };
            try
            {
                var handler = new WebRequestHandler();
                handler.ServerCertificateValidationCallback = delegate { return true; };
                using (HttpClient votoUi = new HttpClient(handler))
                {
                    votoUi.BaseAddress = new Uri(Baseurl+id+"/edit");
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    var resposta = await votoUi.GetAsync("");
                    string dados = await resposta.Content.ReadAsStringAsync();
                    Listar = new JavaScriptSerializer().Deserialize<TB_Enquete>(dados);
                }
                VO_View viewModel = new VO_View();
                    viewModel.id = Listar.poll_id;
                    viewModel.description = Listar.poll_description;
                    viewModel.option_1 = Listar.options[0].option_description;
                    viewModel.option_2 = Listar.options[1].option_description;
                    viewModel.option_3 = Listar.options[2].option_description;
                    viewModel.views = Listar.views;
                
                return View(viewModel);
            }
            catch (Exception)
            {
                return View();
            }

        }

        // POST: Poll/Edit/5
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Editar(VO_View viewModel)
        {
            Baseurl = Baseurl + viewModel.id ;
            TB_Enquete enquete = new TB_Enquete();
            enquete.poll_id = viewModel.id;
            enquete.poll_description = Request["description"];
            enquete.options = new List<TB_Opcao> {
                new TB_Opcao
                {
                    poll_id = viewModel.id,
                    option_description = Request["option_1"],
                    option_id = 1
                },
                new TB_Opcao
                {
                    poll_id = viewModel.id,
                    option_description = Request["option_2"],
                    option_id = 2
                },
                new TB_Opcao
                {
                    poll_id = viewModel.id,
                    option_description = Request["option_3"],
                    option_id = 3
                }
            };
            try
            {
                // TODO: Add update logic here
                var handler = new WebRequestHandler();
                handler.ServerCertificateValidationCallback = delegate { return true; };
                using (HttpClient votoUi = new HttpClient(handler))
                {
                    votoUi.BaseAddress = new Uri(Baseurl);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    //var resposta = await votoUi.GetAsync("");

                    var serializedEnquete = JsonConvert.SerializeObject(enquete);
                    var content = new StringContent(serializedEnquete, Encoding.UTF8, "application/json");

                    var result = await votoUi.PutAsync(Baseurl, content);
                    
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //********************Exclusão********************\\
        // GET: Poll/Delete/5
        public async System.Threading.Tasks.Task<ActionResult> Excluir(int id)
        {
            Baseurl = Baseurl + id + "/Delete";
            TB_Enquete obj = null;
            try
            {
                var handler = new WebRequestHandler();
                handler.ServerCertificateValidationCallback = delegate { return true; };
                using (HttpClient votoUi = new HttpClient(handler))
                {
                    votoUi.BaseAddress = new Uri(Baseurl);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    HttpResponseMessage resposta = await votoUi.DeleteAsync("");
                    string dados = await resposta.Content.ReadAsStringAsync();

                    obj = JsonConvert.DeserializeObject<TB_Enquete>(dados.ToString());
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View();
            }

        }

        // POST: Poll/Delete/5
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Excluir(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var handler = new WebRequestHandler();
                handler.ServerCertificateValidationCallback = delegate { return true; };
                using (HttpClient votoUi = new HttpClient(handler))
                {
                    votoUi.BaseAddress = new Uri(Baseurl);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    HttpResponseMessage resposta = await votoUi.GetAsync("");

                    var serializedEnquete = JsonConvert.SerializeObject(id);
                    var Content = new StringContent(serializedEnquete, Encoding.UTF8, "application/json");

                    var result = await votoUi.DeleteAsync(string.Format("{0}/{1}", Baseurl, id));

                }
                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //********************Voto********************\\
        // GET: Poll/Edit/5
        public async System.Threading.Tasks.Task<ActionResult> Vote(int id)
        {

            //TB_Enquete Listar = null;
            VO_Enquete Listar2 = null;
            List<VO_View> ListarTodosView = new List<VO_View> { };
            try
            {
                var handler = new WebRequestHandler();
                handler.ServerCertificateValidationCallback = delegate { return true; };
                using (HttpClient votoUi = new HttpClient(handler))
                {
                    votoUi.BaseAddress = new Uri(Baseurl + id);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    var resposta = await votoUi.GetAsync("");
                    string dados = await resposta.Content.ReadAsStringAsync();
                    Listar2 = new JavaScriptSerializer().Deserialize<VO_Enquete>(dados);
                }
                VO_View viewModel = new VO_View();
                viewModel.id = Listar2.poll_id;
                viewModel.description = Listar2.poll_description;
                viewModel.option_1 = Listar2.options[0].option_description;
                viewModel.option_2 = Listar2.options[1].option_description;
                viewModel.option_3 = Listar2.options[2].option_description;

                return View(viewModel);
            }
            catch (Exception)
            {
                return View();
            }

        }

        // PUT: Poll/Edit/5
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Vote(int id, VO_View viewModel)
        {
            Baseurl = Baseurl + viewModel.id + "/vote";
            try
            {
                // TODO: Add update logic here
                var handler = new WebRequestHandler();
                handler.ServerCertificateValidationCallback = delegate { return true; };
                using (HttpClient votoUi = new HttpClient(handler))
                {
                    votoUi.BaseAddress = new Uri(Baseurl);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    var resposta = await votoUi.GetAsync("");
                    VO_Voto voto = new VO_Voto();
                    voto.option_id = viewModel.option_vote;
                    var serializedVoto = JsonConvert.SerializeObject(voto);
                    var content = new StringContent(serializedVoto, Encoding.UTF8, "application/json");

                    var result = await votoUi.PostAsync(Baseurl, content);

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}
