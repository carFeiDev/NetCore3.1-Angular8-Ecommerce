using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;
using Project.TakuGames.Model.Helpers;
using Project.TakuGames.Model.Dal;

namespace Project.TakuGames.Business
{
    public abstract class BaseBusiness
    {
        public ComponentError ComponentError { get; set; }

        protected readonly IUnitOfWork UnitOfWork;
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;

        protected BaseBusiness(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger logger)
        {
            UnitOfWork = unitOfWork;
            ComponentError = new ComponentError();
            _logger = logger;
            _mapper = mapper;
        }


        protected void CambiarValor<T, X>(Expression<Func<X>> propiedad, T objeto, X nuevoValor, ref bool cambio)
        {
            var propiedadNombre = GetMemberName(propiedad);
            var classProperty = objeto.GetType().GetProperty(propiedadNombre);
            var propertyValue = classProperty.GetValue(objeto);
            if (!(propertyValue == null && object.Equals(nuevoValor, default(X)))
                && !Equals(propertyValue, nuevoValor))
            {
                classProperty.SetValue(objeto, nuevoValor);
                cambio = true;
            }
        }

        private string GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }
    }
}
