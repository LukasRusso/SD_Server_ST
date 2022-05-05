using Server.Models;
using Server_ST.Utilities;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Server.Controllers
{
    [EnableCors(origins:"*",headers:"*",methods:"*")]
    [RoutePrefix("History")]
    public class HistoryController : ApiController
    {
        private static List<HistoryModel> m_lsHistoryModels = new List<HistoryModel>();

        [SwaggerConsumes("application/json")]
        [SwaggerProduces("application/json")]
        [SwaggerResponse(HttpStatusCode.Created, "History saved successfuly")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Machine can't be empty \nWorker can't be empty \nDateTime is out of range (2000 - Now)")]
        [HttpPost]
        public HttpResponseMessage SaveHistory ([FromBody] HistoryModel history)
        {
            string strMessage = "";

            if(!history.Validate(ref strMessage))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, strMessage);
            }

            int nCount = m_lsHistoryModels.Count;
            history.Id = nCount != 0 ? m_lsHistoryModels[nCount - 1].Id + 1 : 1;

            m_lsHistoryModels.Add(history);
            return Request.CreateResponse(HttpStatusCode.OK, "History saved successfuly");
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [SwaggerConsumes("application/json")]
        [SwaggerProduces("application/json")]
        [SwaggerResponse(HttpStatusCode.OK, "List of History Models", typeof(HistoryModel))]
        [HttpGet]
        public HttpResponseMessage ListHistory()
        {
            return Request.CreateResponse(HttpStatusCode.OK, m_lsHistoryModels);
        }

        [SwaggerConsumes("application/json")]
        [SwaggerProduces("application/json")]
        [SwaggerResponse(HttpStatusCode.OK, "History object", typeof(HistoryModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "History not found")]
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetHistory([FromUri] int id)
        {
            HistoryModel history = null;

            m_lsHistoryModels.ForEach(h =>
            {
                if(h.Id == id)
                {
                    history = h;
                    return;
                }
            });

            if (history != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, history);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "History not found");
            }           
        }

        [SwaggerConsumes("application/json")]
        [SwaggerProduces("application/json")]
        [SwaggerResponse(HttpStatusCode.OK, "History successfully updated")]
        [SwaggerResponse(HttpStatusCode.NotFound, "History not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Machine can't be empty \nWorker can't be empty \nDateTime is out of range (2000 - Now)")]
        [HttpPut]
        [Route("{id}")]
        public HttpResponseMessage UpdateHistory([FromUri] int id, [FromBody] HistoryModel history)
        {
            bool bUpdated = false;
            string strMessage = "";
            if (!history.Validate(ref strMessage))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, strMessage);
            }

            for(int nIndex = 0; nIndex < m_lsHistoryModels.Count; nIndex++)
            {
                if (m_lsHistoryModels[nIndex].Id == id)
                {
                    bUpdated = true;
                    history.Id = id;
                    m_lsHistoryModels[nIndex] = history;
                    break;
                }
            }

            if (bUpdated)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "History successfully updated");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "History not found");
            }
        }

        [SwaggerConsumes("application/json")]
        [SwaggerProduces("application/json")]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [SwaggerResponse(HttpStatusCode.NotFound, "History not found")]
        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeleteHistory([FromUri] int id)
        {
            HistoryModel history = null;

            m_lsHistoryModels.ForEach(h =>
            {
                if (h.Id == id)
                {
                    history = h;
                    return;
                }
            });

            if (history != null)
            {
                m_lsHistoryModels.Remove(history);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "History not found");
            }
        }
    }
}
