using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComunicacionRayen
{
    ///Clase para almacenar y retornar respuesta de consulta de farmacia para 
    ///generar ticket. Se implemento para manejar respuesta de nuevo servicio de
    ///farmacia que retorna data asociada al paciente
    ///
    ///BY Sebastian Garcés - 22/05/2019

    
    public class RespuestaFarmacia
    {
        public RespuestaBase respuestaBase;
        public String EntregaTicket;
        public String NombresPaciente;
        public String PrimerApellidoPaciente;
        public String SegundoApellidoPaciente;
        public Boolean? EsPreferente;

        public RespuestaFarmacia()
        {
            //Data de paciente y verificador para entregar ticket o no
            EntregaTicket = null;
            NombresPaciente = null;
            PrimerApellidoPaciente = null;
            SegundoApellidoPaciente = null;
            EsPreferente = null;
            RespuestaBase = new RespuestaBase();
        }

        public RespuestaBase RespuestaBase
        {
            get { return respuestaBase; }
            set
            {
                respuestaBase = value;
            }
        }
    }

    
}