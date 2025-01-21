using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ApiProveedores.Attributes
{
    /// <summary>
    /// Clase encargada de implementar un validador para las propiedades string que requieran solo numeros
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SoloNumerosAttribute : Attribute, IModelValidator
    {
        /// <summary>
        /// Propiedad usada para obtener el mensaje de error del modelo
        /// </summary>
        public string ErrorMessage { get; set; } = "";

        /// <summary>
        /// Método que realiza la validación de la propiedad
        /// </summary>
        /// <param name="context">Contexto de la propiedad</param>
        /// <returns>En caso de no cumplir la validación retorna el mensaje de error, caso contrario retorna un objeto vacío</returns>
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            string parametro = @"^[0-9]+$";
            IEnumerable<ModelValidationResult> resultado = new List<ModelValidationResult>();
            if (context.Model is not string valor)
                return resultado;
            else if (Regex.IsMatch(valor, parametro))
                return resultado;

            resultado = new List<ModelValidationResult>() { new ModelValidationResult(context.ModelMetadata.PropertyName, string.Format(ErrorMessage, context.ModelMetadata.DisplayName)) };

            return resultado;
        }
    }
}
