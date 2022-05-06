using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Server.Models;
using Server_ST.Utilities;
using Swashbuckle.Swagger.Annotations;

namespace Server_ST.Controllers
{
    [RoutePrefix("Maintenance")]
    public class MaintenanceController : ApiController
    {
        private static List<MaintenanceModel> m_lsMaintenanceModels = new List<MaintenanceModel>();

        [SwaggerConsumes("application/json")]
        [SwaggerProduces("application/json")]
        [SwaggerResponse(HttpStatusCode.Created, "Maintenance saved successfuly")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Machine can't be empty \nWorker can't be empty \nLocation can't be empty \nDateTime is out of range (2000 - Now)")]
        [HttpPost]
        public HttpResponseMessage SaveMaintenance([FromBody] MaintenanceModel maintenance)
        {
            string strMessage = "";

            if (!maintenance.Validate(ref strMessage))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, strMessage);
            }

            int nCount = m_lsMaintenanceModels.Count;
            maintenance.id = nCount != 0 ? m_lsMaintenanceModels[nCount - 1].id + 1 : 1;
           
            m_lsMaintenanceModels.Add(maintenance);
            return Request.CreateResponse(HttpStatusCode.OK, "Maintenance saved successfuly");           
        }

        [SwaggerConsumes("application/json")]
        [SwaggerProduces("application/json")]
        [SwaggerResponse(HttpStatusCode.OK, "List of Maintenance Models", typeof(MaintenanceModel))]
        [HttpGet]
        public HttpResponseMessage ListMaintenance()
        {
            return Request.CreateResponse(HttpStatusCode.OK, m_lsMaintenanceModels);
        }

        [SwaggerConsumes("application/json")]
        [SwaggerProduces("application/json")]
        [SwaggerResponse(HttpStatusCode.OK, "Maintenance object", typeof(HistoryModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Maintenance not found")]
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetMaintenance([FromUri] int id)
        {
            MaintenanceModel maintenance = null;

            m_lsMaintenanceModels.ForEach(m =>
            {
                if (m.id == id)
                {
                    maintenance = m;
                    return;
                }
            });

            if (maintenance != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, maintenance);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Maintenance not found");
            }
        }

        [SwaggerConsumes("application/json")]
        [SwaggerProduces("application/json")]
        [SwaggerResponse(HttpStatusCode.OK, "Maintenance successfully updated")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Maintenance not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Machine can't be empty \nWorker can't be empty \nDateTime is out of range (2000 - Now)")]
        [HttpPut]
        [Route("{id}")]
        public HttpResponseMessage UpdateMaintenance([FromUri] int id, [FromBody] MaintenanceModel maintenance)
        {
            bool bUpdated = false;
            string strMessage = "";
            if (!maintenance.Validate(ref strMessage))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, strMessage);
            }

            for (int nIndex = 0; nIndex < m_lsMaintenanceModels.Count; nIndex++)
            {
                if (m_lsMaintenanceModels[nIndex].id == id)
                {
                    bUpdated = true;
                    maintenance.id = id;
                    m_lsMaintenanceModels[nIndex] = maintenance;
                    break;
                }
            }

            if (bUpdated)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Maintenance successfully updated");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Maintenance not found");
            }
        }

        [SwaggerConsumes("application/json")]
        [SwaggerProduces("application/json")]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [SwaggerResponse(HttpStatusCode.NotFound, "History not found")]
        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeleteMaintenance([FromUri] int id)
        {
            MaintenanceModel maintenance = null;

            m_lsMaintenanceModels.ForEach(m =>
            {
                if (m.id == id)
                {
                    maintenance = m;
                    return;
                }
            });

            if (maintenance != null)
            {
                m_lsMaintenanceModels.Remove(maintenance);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Maintenance not found");
            }
        }
    }
}
