using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComunicacionRayen
{
    public class LugarAtencion
    {
        int id;
        string nombre;
        int idBox;
        string nombreBox;

        public LugarAtencion()
        {
        }

        public string NombreBox
        {
            get
            {
                return nombreBox;
            }

            set
            {
                nombreBox = value;
            }
        }

        public int IdBox
        {
            get
            {
                return idBox;
            }

            set
            {
                idBox = value;
            }
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
    }
}