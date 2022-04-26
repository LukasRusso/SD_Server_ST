using Server.Models;
using Server_ST.Utilities;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Server_ST.Controllers
{
    public class MeasurementController : ApiController
    {
        private static List<MeasurementsModel> m_lsMeasurementsModels = new List<MeasurementsModel>();

        [SwaggerConsumes("application/json")]
        [SwaggerProduces("application/json")]
        [SwaggerResponse(HttpStatusCode.Created, "Measurement saved successfuly")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Machine can't be empty \nWorker can't be empty \nLocation can't be empty \nDateTime is out of range (2000 - Now)")]
        [HttpPost]
        public HttpResponseMessage SaveMaintenance([FromBody] MeasurementsModel measurements)
        {
            string strMessage = "";
            if (!measurements.Validate(ref strMessage))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, strMessage);
            }

            int nCount = m_lsMeasurementsModels.Count;
            measurements.Id = nCount != 0 ? m_lsMeasurementsModels[nCount - 1].Id + 1 : 1;

            m_lsMeasurementsModels.Add(measurements);
            return Request.CreateResponse(HttpStatusCode.OK, "Measurement saved successfuly");
        }

        [SwaggerConsumes("application/json")]
        [SwaggerProduces("application/json")]
        [SwaggerResponse(HttpStatusCode.OK, "List of Measurement Models", typeof(MaintenanceModel))]
        [HttpGet]
        public HttpResponseMessage ListMaintenance()
        {
            return Request.CreateResponse(HttpStatusCode.OK, m_lsMeasurementsModels);
        }

        [SwaggerConsumes("application/json")]
        [SwaggerProduces("application/json")]
        [SwaggerResponse(HttpStatusCode.OK, "Measurement object", typeof(HistoryModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Measurement not found")]
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetMaintenance([FromUri] int id)
        {
            MeasurementsModel maintenance = null;

            m_lsMeasurementsModels.ForEach(m =>
            {
                if (m.Id == id)
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
                return Request.CreateResponse(HttpStatusCode.NotFound, "Measurement not found");
            }
        }

        [SwaggerConsumes("application/json")]
        [SwaggerProduces("application/json")]
        [SwaggerResponse(HttpStatusCode.OK, "Measurement successfully updated")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Measurement not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Machine can't be empty \nWorker can't be empty \nDateTime is out of range (2000 - Now)")]
        [HttpPut]
        [Route("{id}")]
        public HttpResponseMessage UpdateMaintenance([FromUri] int id, [FromBody] MeasurementsModel measurements)
        {
            bool bUpdated = false;
            string strMessage = "";
            if (!measurements.Validate(ref strMessage))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, strMessage);
            }

            for (int nIndex = 0; nIndex < m_lsMeasurementsModels.Count; nIndex++)
            {
                if (m_lsMeasurementsModels[nIndex].Id == id)
                {
                    bUpdated = true;
                    measurements.Id = id;
                    m_lsMeasurementsModels[nIndex] = measurements;
                    break;
                }
            }

            if (bUpdated)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Measurement successfully updated");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Measurement not found");
            }
        }

        [SwaggerConsumes("application/json")]
        [SwaggerProduces("application/json")]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [SwaggerResponse(HttpStatusCode.NotFound, "Measurement not found")]
        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeleteMaintenance([FromUri] int id)
        {
            MeasurementsModel measurement = null;

            m_lsMeasurementsModels.ForEach(m =>
            {
                if (m.Id == id)
                {
                    measurement = m;
                    return;
                }
            });

            if (measurement != null)
            {
                m_lsMeasurementsModels.Remove(measurement);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Measurement not found");
            }
        }
    }
}
