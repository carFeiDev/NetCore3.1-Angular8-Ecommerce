
using Project.TakuGames.Model.Domain;
using System.Collections.Generic;

namespace Project.TakuGames.Model.Business
{
    public interface IGamesBusiness : IBusiness
    {
        List<Games> Listar();
        Games Obtener(int gameId);
        Games Crear(Games game);
        Games Modificar(Games game);
        void Eliminar(int id);
    }
}
