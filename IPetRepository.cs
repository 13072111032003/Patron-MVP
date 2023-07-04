using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrónMVP.Models
{
   public interface IPetRepository
    {
        void Añadir(PetModel petModel);
        void Editar(PetModel petModel);
        void Borrar(int id);
        IEnumerable<PetModel> GetAll();
        IEnumerable<PetModel> GetByValue(string value);//Buscar

    }
}
