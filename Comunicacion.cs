using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace ComunicacionRayen
{
    /// <summary>
    /// CLASE DE COMUNICACIÓN, INTEGRACIÓN CON SERVICIOS WEB RAYEN SALUD
    /// SERVICIOS WEB HTTPS TLS1.2 DE FARMACIA Y CITAS
    /// BY JUAN GONZÁLEZ
    /// 26/01/2018
    /// 
    /// EDIT: MODIFICACION DE WEB SERVICES DE FARMACIA Y CITAS PARA APUNTAR A
    ///     AMBIENTE DE PREPRODUCCION DE RAYEN. SE AJUSTARON REQUEST Y RESPONSES A NUEVOS
    ///     SERVICIOS PARA LOS NUEVOS PARAMETROS DE LOS MISMOS.
    ///     BY SEBASTIAN GARCÉS - 22/05/2019
    /// </summary>

    public class Comunicacion
    {
        //WS Farmacia actualizado en preproduccion
        private WSFarmaciaTotem1.FarmaciaTotem wsFarmacia;
        //WS Citas actualizado en preproduccion
        private WSCitas.MetodosTotemDTS wsCitas;
        private Log log;
        private String _connectionString;

        public Comunicacion() {
            try
            {
                // tratamos de leer el connection string desde el web.config
                _connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
            }
            catch
            {
                _connectionString = "";
            }
        }



        /*
         * by JGonzález
         * Método que permite la integración con servicio web rayen salud y la obtención de las citas asociadas
         * a un paciente por su RUT.
         */

        public List<CitasPaciente> RayenObtenerCitasaPacientes(string runPaciente, string CodigoEstablecimiento, string FechaHoraMensaje, string TipoMensaje)
        {
            List<CitasPaciente> cita = new List<CitasPaciente>();
            CitasPaciente citasPaciente = new CitasPaciente();
            Cita citas = null;

            log = new Log();
            log.Escribe("RayenObtenerCitasaPacientes : Init");

            wsCitas = new WSCitas.MetodosTotemDTS(); //.Cita();
            WSCitas.ParametroObtenerCitasPaciente paramRequest = new WSCitas.ParametroObtenerCitasPaciente();
            WSCitas.RespuestaObtenerCitasPaciente paramResponse = new WSCitas.RespuestaObtenerCitasPaciente();
            WSCitas.ParametroBase paramBase = new WSCitas.ParametroBase();

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //Obtiene las credenciales para los metodos de citas
                ArrayList credenciales = GetCredencialesCitas();
                wsCitas.Credentials = new NetworkCredential(credenciales[0].ToString(), credenciales[1].ToString());

                log.Escribe("RayenObtenerCitasaPacientes : paramRequest : RunPaciente: " + runPaciente +
                            ", CodigoEstablecimiento: " + CodigoEstablecimiento +
                            ", IdCentro: " + FechaHoraMensaje +
                            ", TipoMensaje: " + TipoMensaje);

                paramRequest.RunPaciente = runPaciente.ToString().Trim().ToLower();

                paramBase.CodigoEstablecimiento = CodigoEstablecimiento;
                paramBase.FechaHoraMensaje = FechaHoraMensaje;
                paramBase.IdSitioSoftware = "1";
                paramBase.IdSoftwareInforma = "1";
                paramBase.TipoMensaje = TipoMensaje;
                paramBase.VersionSoftwareInforma = "";

                paramRequest.ParametroBase = paramBase;

                //se consume servicio web de rayen salud para obtener citas asociadas a un paciente por su rut.
                paramResponse = wsCitas.ObtenerCitasPaciente(paramRequest);

                log.Escribe("RayenObtenerCitasaPacientes : paramResponse : ObtenerCitasPaciente: " +
                            ", Codigo: " + paramResponse.RespuestaBase.Codigo +
                            ", Descripcion: " + paramResponse.RespuestaBase.Descripcion);


                if (paramResponse.RespuestaBase.Codigo == 0)
                {

                    //Anexa data de Paciente
                    citasPaciente.Paciente.ApellidoPaterno = paramResponse.Paciente.ApellidoPaterno;
                    citasPaciente.Paciente.ApellidoMaterno = paramResponse.Paciente.ApellidoMaterno;
                    citasPaciente.Paciente.FechaNacimiento = paramResponse.Paciente.FechaNacimiento;//en documento integración totem citas V1.1 sale retorno como DATETIME y está retornando string
                    citasPaciente.Paciente.Nombres = paramResponse.Paciente.Nombres;
                    citasPaciente.Paciente.Sexo = paramResponse.Paciente.Sexo;//en documento integración totem citas V1.1 sale retorno como INT y está retornando string
                    log.Escribe("RayenObtenerCitasaPacientes : paramResponse : Paciente: OK");

                    //Anexa data de citas asociadas a paciente
                    for (int i = 0; i < paramResponse.Citas.Length; i++)
                    {
                        citas = new Cita();
                        citas.Id = paramResponse.Citas[i].Id;
                        citas.Fecha = paramResponse.Citas[i].Fecha;
                        citas.Hora = paramResponse.Citas[i].Hora;
                        citas.Duracion = "";
                        citas.Sector = paramResponse.Citas[i].Sector;
                        citas.Estado = paramResponse.Citas[i].Estado;

                        citas.Profesional.Nombres = paramResponse.Citas[i].Profesional.Nombres;
                        citas.Profesional.ApellidoPaterno = paramResponse.Citas[i].Profesional.ApellidoPaterno;
                        citas.Profesional.ApellidoMaterno = paramResponse.Citas[i].Profesional.ApellidoMaterno;
                        citas.Profesional.Sexo = paramResponse.Citas[i].Profesional.Sexo;

                        citasPaciente.Citas.Add(citas);
                    }

                    log.Escribe("RayenObtenerCitasaPacientes : paramResponse : Citas: OK. Cantidad: " + citasPaciente.Citas.Count);

                    //Respuesta Base que informa estado de petición al método obtener citas paciente en el servicio web.
                    citasPaciente.RespuestaBase.Codigo = paramResponse.RespuestaBase.Codigo;
                    citasPaciente.RespuestaBase.Descripcion = paramResponse.RespuestaBase.Descripcion;
                }
                else {

                    log.Escribe("RayenObtenerCitasaPacientes : paramResponse : Citas: NOK");
                    //Respuesta Base
                    citasPaciente.RespuestaBase.Codigo = paramResponse.RespuestaBase.Codigo;
                    citasPaciente.RespuestaBase.Descripcion = paramResponse.RespuestaBase.Descripcion;
                }

            }
            catch (Exception ex)
            {
                citasPaciente.RespuestaBase.Codigo = 0001;
                citasPaciente.RespuestaBase.Descripcion = "ERROR: " + ex.Message;
                log.Escribe("*******ERROR: RayenObtenerCitasaPacientes Respuesta: " + ex.Message);
            }

            cita.Add(citasPaciente);

            return cita;
        }



        /*
         * by Sebastian Garcés
         * Método que permite la integración con servicio web rayen salud y enviar el alerta
         * para reconocer que el paciente llego, al servicio de rayen
         */

        public string RayenInformarLlegadaPaciente(string IdCita, string CodigoEstablecimiento, string FechaHoraMensaje, string TipoMensaje)
        {
            string respWS = "";

            log = new Log();
            log.Escribe("RayenInformarLlegadaPaciente : Init");

            wsCitas = new WSCitas.MetodosTotemDTS(); //.Cita();
            WSCitas.ParametroInformarLlegadaPaciente paramRequest = new WSCitas.ParametroInformarLlegadaPaciente();
            WSCitas.RespuestaInformarLlegadaPaciente paramResponse = new WSCitas.RespuestaInformarLlegadaPaciente();
            WSCitas.ParametroBase paramBase = new WSCitas.ParametroBase();

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //Obtiene las credenciales para los metodos de citas
                ArrayList credenciales = GetCredencialesCitas();
                wsCitas.Credentials = new NetworkCredential(credenciales[0].ToString(), credenciales[1].ToString());

                paramRequest.IdCita = IdCita;

                paramBase.CodigoEstablecimiento = CodigoEstablecimiento;
                paramBase.FechaHoraMensaje = FechaHoraMensaje;
                paramBase.IdSitioSoftware = "";
                paramBase.IdSoftwareInforma = "";
                paramBase.TipoMensaje = TipoMensaje;
                paramBase.VersionSoftwareInforma = "";

                paramRequest.ParametroBase = paramBase;

                log.Escribe("RayenInformarLlegadaPaciente : paramRequest : IdCita: " + IdCita +
                            ", CodigoEstablecimiento: " + CodigoEstablecimiento +
                            ", IdCentro: " + FechaHoraMensaje +
                            ", TipoMensaje: " + TipoMensaje);

                paramResponse = wsCitas.InformarLlegadaPaciente(paramRequest);

                log.Escribe("RayenInformarLlegadaPaciente : paramResponse : ObtenerCitasPaciente: " +
                            ", Codigo: " + paramResponse.RespuestaBase.Codigo +
                            ", Descripcion: " + paramResponse.RespuestaBase.Descripcion);

                respWS = "" + paramResponse.RespuestaBase.Codigo + "■" + paramResponse.RespuestaBase.Descripcion;

            }
            catch (Exception ex)
            {
                respWS = "ERROR: " + ex.Message;
                log.Escribe("*******ERROR: RayenInformarLlegadaPaciente Respuesta: " + ex.Message);
            }

            return respWS;
        }




        /*
         * by Sebastian Garcés
         * Método que permite la integración con servicio web rayen salud y permite la
         * validar la generacion de ticket si el paciente tiene recetas pendientes
         */

        public RespuestaFarmacia RayenGenerarTicket(int diasTolerancia, string idCentro, string rut)
        {
            string respWS = "";

            log = new Log();
            log.Escribe("RayenGenerarTicket : Init");

            wsFarmacia = new WSFarmaciaTotem1.FarmaciaTotem();
            WSFarmaciaTotem1.ParametroGenerarTicket paramRequest = new WSFarmaciaTotem1.ParametroGenerarTicket();
            WSFarmaciaTotem1.ParametroRespuestaGenerarTicket paramResponse = new WSFarmaciaTotem1.ParametroRespuestaGenerarTicket();

            RespuestaFarmacia rf = new RespuestaFarmacia();

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //Obtiene credenciales de farmacia
                ArrayList credenciales = GetCredenciales();
                wsFarmacia.Credentials = new NetworkCredential(credenciales[0].ToString(), credenciales[1].ToString());

                paramRequest.DiasTolerancia = diasTolerancia;
                paramRequest.IdCentro = idCentro;
                paramRequest.Rut = rut.ToString().Trim().Replace("\r\n", "").Replace("\n", "").Replace("\r", "").ToLower();

                log.Escribe("paramRequest : diasTolerancia: " + diasTolerancia +
                            ", IdCentro: " + idCentro +
                            ", rut: " + rut);

                paramResponse = wsFarmacia.GenerarTicket(paramRequest);

                log.Escribe("paramResponse : Respuesta: EntregaTicket: " + paramResponse.EntregaTicket +
                            ", Codigo: " + paramResponse.RespuestaBase.Codigo +
                            ", Descripcion: " + paramResponse.RespuestaBase.Descripcion+
                            ", Edad: " + paramResponse.Edad +
                            ", Embarazo: " + paramResponse.Embarazo +
                            ", Discapacidad: " + paramResponse.Discapacidad);

                //Almacena data en objeto rf = respuesta farmacia, para retornar respuesta                
                rf.EntregaTicket = paramResponse.EntregaTicket;
                rf.NombresPaciente = paramResponse.NombresPaciente;
                rf.PrimerApellidoPaciente = paramResponse.PrimerApellidoPaciente;
                rf.SegundoApellidoPaciente = paramResponse.SegundoApellidoPaciente;
                rf.RespuestaBase.Codigo = paramResponse.RespuestaBase.Codigo;
                rf.RespuestaBase.Descripcion = paramResponse.RespuestaBase.Descripcion;
                Boolean esPreferente = false;
                if (paramResponse.Edad != null || !paramResponse.Edad.Equals(""))
                    if (int.Parse(paramResponse.Edad) >= 60)
                        esPreferente = true;
                if (paramResponse.Embarazo != null || !paramResponse.Embarazo.Equals(""))
                    if (paramResponse.Embarazo.ToUpper().Equals("TRUE"))
                        esPreferente = true;
                if (paramResponse.Discapacidad != null || !paramResponse.Discapacidad.Equals(""))
                    if (paramResponse.Discapacidad.ToUpper().Equals("TRUE"))
                        esPreferente = true;
                rf.EsPreferente = esPreferente;
            }
            catch (Exception ex)
            {
                respWS = "ERROR: " + ex.Message;
                log.Escribe("*******ERROR: RayenGenerarTicket Respuesta: " + ex.Message);
            }
            return rf;
        }




        /*
         * by Sebastian Garcés
         * Método que permite actualizar un ticket de tipo cita
         */

        public string InsertaTicketCita(int IdCita, string IdTicket, int idOficina, string codigoEstablecimiento, string UrlWSTicketero)
        {
            string respWS = "";

            log = new Log();
            log.Escribe("InsertaTicketCita : Init");

            SqlConnection conexion = new SqlConnection(_connectionString);
            SqlDataAdapter adapter = null;
            DataSet ds = new System.Data.DataSet();

            try
            {
                conexion.Open();
                adapter = new SqlDataAdapter("sp_InsertaTicketCita", conexion);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                adapter.SelectCommand.Parameters.AddWithValue("IdCita", IdCita);
                adapter.SelectCommand.Parameters.AddWithValue("IdTicket", IdTicket);
                adapter.SelectCommand.Parameters.AddWithValue("idOficina", idOficina);
                adapter.SelectCommand.Parameters.AddWithValue("codigoEstablecimiento", codigoEstablecimiento);
                adapter.SelectCommand.Parameters.AddWithValue("UrlWSTicketero", UrlWSTicketero);

                adapter.Fill(ds);

                respWS = "OK";// ds.Tables[0].Rows[0]["URLServicio"].ToString();
                log.Escribe("OK: ActualizaTicketCita Respuesta: OK");

            }
            catch (Exception ex)
            {
                respWS = "ERROR: " + ex.Message;
                log.Escribe("*******ERROR: ActualizaTicketCita Respuesta: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }

            return respWS;
        }

        //Obtiene credenciales para farmacia (User y Pw) de web.config
        private ArrayList GetCredenciales()
        {
            ArrayList credenciales = new ArrayList();
            credenciales.Add(Base64Decode(ConfigurationManager.AppSettings["User"]));
            credenciales.Add(Base64Decode(ConfigurationManager.AppSettings["Pw"]));
            return credenciales;
        }

        //Obtiene credenciales para citas (UserCitas y PwCitas) de web.config
        private ArrayList GetCredencialesCitas()
        {
            ArrayList credenciales = new ArrayList();
            credenciales.Add(Base64Decode(ConfigurationManager.AppSettings["UserCitas"]));
            credenciales.Add(Base64Decode(ConfigurationManager.AppSettings["PwCitas"]));
            return credenciales;
        }


        //Decodifica string codificado en base 64
        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}