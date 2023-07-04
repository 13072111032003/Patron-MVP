using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatrónMVP.Views
{
    public interface IPetView
    {
        //Propiedades
        string PetId { get; set; }
        string PetNombre { get; set; }
        string PetTipo { get; set; }
        string PetColor { get; set; }
        string Buscar { get; set; }
        bool Editar { get; set; }
        bool Correcto { get; set; }
        string Mensaje { get; set; }

        //Events
        event EventHandler Busca;
        event EventHandler Añadir;
        event EventHandler Edita;
        event EventHandler Borrar;
        event EventHandler Guardar;
        event EventHandler Cancelar;

        //Methods
        void SetPetListBindingSource(BindingSource petList);
        void Show();//Opcional

    }
}
