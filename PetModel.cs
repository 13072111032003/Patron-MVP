using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PatrónMVP.Models
{
    public class PetModel
    {
        //Variables
        private int id;
        private string nombre;
        private string tipo;
        private string color;

        //Propiedades
        [DisplayName("Ingrese el ID de la mascota")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        [DisplayName("Nombre de la mascota")]
        [Required(ErrorMessage ="El nombre de la mascota es necesario")]
        [StringLength(50,MinimumLength =3, ErrorMessage = "El nombre de la mascota debe tener entre 3 y 50 caracteres")]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        [DisplayName("Tipo de mascota")]
        [Required(ErrorMessage = "El tipo de la mascota es necesario")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El tipo de la mascota debe tener entre 3 y 50 caracteres")]
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        [DisplayName("Color de la mascota")]
        [Required(ErrorMessage = "El color de la mascota es necesario")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El color de la mascota debe tener entre 3 y 50 caracteres")]
        public string Color
        {
            get { return color; }
            set { color = value; }
        }
    }
}
