using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace web_api.Controllers
{
    public class VeiculosController : ApiController
    {
        private readonly Repositories.SQLServer.Veiculo repositorioVeiculo;

        public VeiculosController()
        {
            this.repositorioVeiculo = new Repositories.SQLServer.Veiculo(Configurations.Database.getConnectionString());
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(this.repositorioVeiculo.Select());
            }
            catch (Exception ex)
            {

                Utils.Logger.WriteException(Configurations.Logger.getFullPath(), ex);

                return InternalServerError();
            }
        }

        [HttpGet]
        public IHttpActionResult Get(int Id)
        {
            try
            {
                Models.Veiculo veiculo = this.repositorioVeiculo.Select(Id);
                if (veiculo == null)
                    return NotFound();

                return Ok(veiculo);
            }
            catch (Exception ex)
            {
                Utils.Logger.WriteException(Configurations.Logger.getFullPath(), ex);

                return InternalServerError();
            }
        }

        [HttpGet]
        public IHttpActionResult Get(string Nome)
        {
            try
            {
                List<Models.Veiculo> veiculo = new List<Models.Veiculo>(this.repositorioVeiculo.Select(Nome));
                if (veiculo == null)
                    return NotFound();

                return Ok(veiculo);
            }
            catch (Exception ex)
            {
                Utils.Logger.WriteException(Configurations.Logger.getFullPath(), ex);

                return InternalServerError();
            }
        }

        [HttpPost]
        public IHttpActionResult Post(Models.Veiculo veiculo)
        {
            try
            {
                if (!this.repositorioVeiculo.Insert(veiculo))
                    return InternalServerError();

                return Ok("Veiculo cadastrado com sucesso!");

            }
            catch (Exception ex)
            {
                Utils.Logger.WriteException(Configurations.Logger.getFullPath(), ex);

                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IHttpActionResult Put(int id, Models.Veiculo veiculo)
        {
            try
            {
                if (id != veiculo.Id)
                    return BadRequest("O id da requisição não coincide com o Id do veiculo.");

                if (!this.repositorioVeiculo.Update(veiculo))
                    return InternalServerError();

                return Ok(veiculo);
            }
            catch (Exception ex)
            {
                Utils.Logger.WriteException(Configurations.Logger.getFullPath(), ex);

                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int Id)
        {
            try
            {
                if (!this.repositorioVeiculo.Delete(Id))
                    return NotFound();

                return Ok("Registro excluido com sucesso!");
            }
            catch (Exception ex)
            {
                Utils.Logger.WriteException(Configurations.Logger.getFullPath(), ex);

                return InternalServerError();
            }
        }
    }
}
