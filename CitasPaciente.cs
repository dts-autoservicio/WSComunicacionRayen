using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComunicacionRayen
{
    public class CitasPaciente
    {
        private List<Cita> citas;
        private Paciente paciente;
        private RespuestaBase respuestaBase;

        public CitasPaciente()
        {
            Paciente = new Paciente();
            RespuestaBase = new RespuestaBase();
            Citas = new List<Cita>();
        }

        public Paciente Paciente
        {
            get { return paciente; }
            set
            {
                paciente = value;
            }
        }
        public RespuestaBase RespuestaBase
        {
            get { return respuestaBase; }
            set
            {
                respuestaBase = value;
            }
        }
        public List<Cita> Citas
        {
            get { return citas; }
            set
            {
                citas = value;
            }
        }
    }

    public class Cita
    {
        private int id;
        private string fecha;
        /*private string horaInicio;
        private string horaTermino;*/
        private string duracion;
        private string hora;
        private string sector;
        private string estado;
        private Profesional profesional;

        public Cita()
        {
            Profesional = new Profesional();
        }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
            }
        }
        public string Fecha
        {
            get { return fecha; }
            set
            {
                fecha = value;
            }
        }
        public string Hora
        {
            get { return hora; }
            set
            {
                hora = value;
            }
        }
        /*public string HoraInicio
        {
            get { return horaInicio; }
            set
            {
                horaInicio = value;
            }
        }
        public string HoraTermino
        {
            get { return horaTermino; }
            set
            {
                horaTermino = value;
            }
        }*/
        public string Duracion
        {
            get { return duracion; }
            set
            {
                duracion = value;
            }
        }

        public string Sector
        {
            get { return sector; }
            set
            {
                sector = value;
            }
        }
        public string Estado
        {
            get { return estado; }
            set
            {
                estado = value;
            }
        }
        public Profesional Profesional
        {
            get { return profesional; }
            set
            {
                profesional = value;
            }
        }
    }

    public class Profesional
    {
        private string nombres;
        private string apellidoPaterno;
        private string apellidoMaterno;
        private string sexo;

        public Profesional() {}

        public string Nombres
        {
            get { return nombres; }
            set
            {
                nombres = value;
            }
        }
        public string ApellidoPaterno
        {
            get { return apellidoPaterno; }
            set
            {
                apellidoPaterno = value;
            }
        }
        public string ApellidoMaterno
        {
            get { return apellidoMaterno; }
            set
            {
                apellidoMaterno = value;
            }
        }
        public string Sexo
        {
            get { return sexo; }
            set
            {
                sexo = value;
            }
        }
    }

    public class Paciente
    {
        private string nombres;
        private string apellidoPaterno;
        private string apellidoMaterno;
        private string fechaNacimiento;
        private string sexo;

        public Paciente() { }

        public string Nombres
        {
            get { return nombres; }
            set
            {
                nombres = value;
            }
        }
        public string ApellidoPaterno
        {
            get { return apellidoPaterno; }
            set
            {
                apellidoPaterno = value;
            }
        }
        public string ApellidoMaterno
        {
            get { return apellidoMaterno; }
            set
            {
                apellidoMaterno = value;
            }
        }
        public string FechaNacimiento
        {
            get { return fechaNacimiento; }
            set
            {
                fechaNacimiento = value;
            }
        }
        public string Sexo
        {
            get { return sexo; }
            set
            {
                sexo = value;
            }
        }
    }

    public class RespuestaBase
    {
        private int codigo;
        private string descripcion;

        public RespuestaBase() { }

        public int Codigo
        {
            get { return codigo; }
            set
            {
                codigo = value;
            }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                descripcion = value;
            }
        }
    }

}