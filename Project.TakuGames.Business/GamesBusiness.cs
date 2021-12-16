using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Project.TakuGames.Model.Exceptions;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Dal;

namespace Project.TakuGames.Business
{
    public class GamesBusiness : BaseBusiness , IGamesBusiness
    {
     
        public GamesBusiness(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<GamesBusiness> logger)
            : base(unitOfWork , mapper, logger){ }

        public List<Games> Listar()
        {
            return ListarTodosDesdeDatabase();
        }

        public Games Obtener(int GameId)
        {
            return ListarTodosDesdeDatabase().Where(x => x.GameId == GameId).FirstOrDefault();
        }
        public Games Crear(Games game)
        {
            //ValidarCrearGame(game);
            UnitOfWork.GamesRepository.Insert(game);
            UnitOfWork.Save();
            return game;
        }
        public Games Modificar(Games game)
        {
            ValidarModificarGame(game);
            var gam = BuscarGame(game.GameId);
            gam.Name = game.Name.Trim();
            gam.Company = game.Company;  
            UnitOfWork.GamesRepository.Update(gam);
            UnitOfWork.Save();
            return gam;
        }

        public void Eliminar( int id)
        {
            var gam = BuscarGame(id);
            UnitOfWork.GamesRepository.Delete(gam);
            UnitOfWork.Save();
        }

        #region Validaciones
        private void ValidarModificarGame(Games game)
        {
            ValidarQueExistaElId(game);
            if (!ComponentError.IsValid)
            {
                throw new BadRequestException(ComponentError);
            }
        }
        private  void ValidarQueExistaElId(Games game)
        {
            var gam = BuscarGame(game.GameId);
            if (gam == null)
            {
                throw new BadRequestException();
            }
        }

        #endregion

        #region helpers
        private Games BuscarGame(int gameId)
        {
            return ListarTodosDesdeDatabase().Where(x => x.GameId == gameId).FirstOrDefault();
        }

        private List<Games> ListarTodosDesdeDatabase()
        {
            var resp = UnitOfWork.GamesRepository.Get().ToList();
            return resp;
        }

        #endregion
    }
}

