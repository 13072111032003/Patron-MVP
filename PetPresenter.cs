using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PatrónMVP.Models;
using PatrónMVP.Views;

namespace PatrónMVP.Presenters
{
    public class PetPresenter
    {
        //Fields
        private IPetView view;
        private IPetRepository repository;
        private BindingSource petsBindingSource;
        private IEnumerable<PetModel> petList;

        //Constructor
        public PetPresenter(IPetView view, IPetRepository repository)
        {
            this.petsBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;
            //Suscríbase a los métodos del controlador de eventos para ver los eventos
            this.view.Busca += BuscarMascota;
            this.view.Añadir += AñadirMascota;
            this.view.Edita += EditarMascota;
            this.view.Borrar += BorrarMascota;
            this.view.Guardar += GuardarMascota;
            this.view.Cancelar += CancelarAcción;
            //Establecer la fuente de enlace de las mascotas
            this.view.SetPetListBindingSource(petsBindingSource);
            //Cargar vista de lista de mascotas
            LoadAllPetList();
            //Mostrar
            this.view.Show();
        }


        //Metodos
        private void LoadAllPetList()
        {
            petList = repository.GetAll();
            petsBindingSource.DataSource = petList;//Establecer fuente de datos.
        }
        private void BuscarMascota(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.Buscar);
            if (emptyValue == false)
                petList = repository.GetByValue(this.view.Buscar);
            else petList = repository.GetAll();
            petsBindingSource.DataSource = petList;
        }
        private void AñadirMascota(object sender, EventArgs e)
        {
            view.Editar = false;          
        }
        private void EditarMascota (object sender, EventArgs e)
        {
            var pet = (PetModel)petsBindingSource.Current;
            view.PetId = pet.Id.ToString();
            view.PetNombre = pet.Nombre;
            view.PetTipo = pet.Tipo;
            view.PetColor = pet.Color;
            view.Editar = true;
        }
        private void GuardarMascota(object sender, EventArgs e)
        {
            var model = new PetModel();
            model.Id = Convert.ToInt32(view.PetId);
            model.Nombre = view.PetNombre;
            model.Tipo = view.PetTipo;
            model.Color = view.PetColor;
            try
            {
                new Common.ModelDataValidation().Validación(model);
                if(view.Editar)//Edita modelo
                {
                    repository.Editar(model);
                    view.Mensaje = "Pet edited successfuly";
                }
                else //Añadir nuevo modelo
                {
                    repository.Añadir(model);
                    view.Mensaje = "Pet added sucessfully";
                }
                view.Correcto = true;
                LoadAllPetList();
                CleanviewFields();
            }
            catch (Exception ex)
            {
                view.Correcto = false;
                view.Mensaje = ex.Message;
            }
        }
        private void CleanviewFields()
        {
            view.PetId = "0";
            view.PetNombre = "";
            view.PetTipo = "";
            view.PetColor = "";            
        }

        private void CancelarAcción(object sender, EventArgs e)
        {
            CleanviewFields();
        }
        private void BorrarMascota(object sender, EventArgs e)
        {
            try
            {
                var pet = (PetModel)petsBindingSource.Current;
                repository.Borrar(pet.Id);
                view.Correcto = true;
                view.Mensaje = "El nombre de la mascota ha sido eliminado correctamente";
                LoadAllPetList();
            }
            catch (Exception ex)
            {
                view.Correcto = false;
                view.Mensaje = "Ocurrió un error, no se pudo eliminar la mascota";
            }
        }

    }
}
