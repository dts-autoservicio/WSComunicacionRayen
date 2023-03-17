using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;

namespace ComunicacionRayen
{
    /// <summary>
    /// SERVICIO WEB QUE PERMITE LA INTEGRACIÓN CON RAYEN SALUD
    /// Y SUS SERVICIOS WEB HTTPS TLS1.2 DE FARMACIA Y CITAS
    /// BY JUAN GONZÁLEZ
    /// 24/01/2018
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class ComunicacionRayenSalud : System.Web.Services.WebService
    {

        private Comunicacion comunicacion = null;
        private List<CitasPaciente> cita = null;

        [WebMethod]
        public string Test()
        {
            return "OK";
        }

        /**********************************
         * FARMACIA
         * ********************************/
        [WebMethod]
        public RespuestaFarmacia GenerarTicket(int diasTolerancia, string idCentro, string rut)
        {
            comunicacion = new Comunicacion();
            RespuestaFarmacia respWS = comunicacion.RayenGenerarTicket(diasTolerancia, idCentro, rut);
            return respWS;
        }

        /**********************************
        * CITAS
        * ********************************
        * El Paciente deberá ingresar su RUN en el Tótem, 
        * con lo cual se generará una consulta vía integración a Rayen en donde, 
        * tras verificar si el Paciente existe para ese establecimiento, 
        * se entregará un listado con las citas agendadas que tenga registradas para el día en curso. 
        * De este listado, el paciente podrá seleccionar alguna de las citas, 
        * lo cual generará otra consulta notificando a Rayen que el paciente ha llegado.
        * *********************************/

        [WebMethod]
        public List<CitasPaciente> ObtenerCitasPaciente(string runPaciente, string CodigoEstablecimiento, string FechaHoraMensaje, string TipoMensaje)
        {
            comunicacion = new Comunicacion();
            cita = new List<CitasPaciente>();
            cita = comunicacion.RayenObtenerCitasaPacientes(runPaciente, CodigoEstablecimiento, FechaHoraMensaje, TipoMensaje);
            return cita;
        }

        [WebMethod]
        public string InformarLlegadaPaciente(string IdCita, string CodigoEstablecimiento, string FechaHoraMensaje, string TipoMensaje)
        {
            comunicacion = new Comunicacion();
            string respWS = comunicacion.RayenInformarLlegadaPaciente(IdCita, CodigoEstablecimiento, FechaHoraMensaje, TipoMensaje);
            return respWS;
        }

        [WebMethod]
        public string RegistraTicketCita(int IdCita, string IdTicket, int IdOficina, string UrlWSTicketero, string codigoEstablecimiento)
        {
            comunicacion = new Comunicacion();
            string respWS = comunicacion.InsertaTicketCita(IdCita, IdTicket, IdOficina, codigoEstablecimiento, UrlWSTicketero);
            return respWS;
        }

    }
}