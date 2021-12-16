//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
//using System.Web.Mvc;

namespace Project.TakuGames.Model.Helpers
{
    public class ComponentError
    {
        /// <summary>
        /// Lista de errores
        /// </summary>
        public List<ComponentErrorDetail> Errors { get; set; }
        public ComponentError()
        {
            Errors = new List<ComponentErrorDetail>();
        }

        public ComponentError(ModelStateDictionary modelState)
        {
            Errors = new List<ComponentErrorDetail>();
            foreach (var item in modelState)
            {
                foreach (var error in item.Value.Errors)
                {
                    Errors.Add(new ComponentErrorDetail()
                    {
                        Exception = error.Exception,
                        Field = item.Key,
                        Message = error.ErrorMessage
                    });
                }
            }
        }

        public void AddModelError(string field, Exception ex)
        {
            Errors.Add(new ComponentErrorDetail { Exception = ex, Field = field });
        }

        [JsonIgnore]
        public bool IsValid
        {
            get { return !Errors.Any(); }
        }
    }

    public class ComponentErrorDetail
    {
        private string message = "";
        /// <summary>
        /// Mensaje de error
        /// </summary>
        public string Message
        {
            get
            {
                return message == "" && Exception != null ? Exception.Message : message;
            }
            set
            {
                message = value;
            }
        }
        [JsonIgnore]
        public Exception Exception { get; set; }
        /// <summary>
        /// Campo que genero el error
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// Tipo de error: Validation | Business
        /// </summary>
        public string @Type
        {
            get
            {
                if (Exception == null)
                {
                    return "Validation";
                }

                return Exception is Exceptions.BusinessException ? "Business" : "Validation";
            }
        }
        /// <summary>
        /// Codigo del error
        /// </summary>
        public string ErrorCode
        {
            get
            {
                if (Exception == null)
                {
                    return "";
                }

                return Exception is Exceptions.BusinessException be ? be.ErrorCode : "";
            }
        }
    }
}
