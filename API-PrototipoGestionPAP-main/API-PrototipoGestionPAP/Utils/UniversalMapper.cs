using System;
using System.Linq;
using System.Reflection;

namespace API_PrototipoGestionPAP.Utils
{
    public static class UniversalMapper
    {
        /// <summary>
        /// Mapea las propiedades de <typeparamref name="TSource"/> a <typeparamref name="TDestination"/> sin crear una nueva instancia.
        /// </summary>
        public static void Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            if (source == null || destination == null)
                throw new ArgumentNullException("Source or destination cannot be null");

            var sourceProperties = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var destinationProperties = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProp in sourceProperties)
            {
                var destProp = destinationProperties.FirstOrDefault(x => x.Name == sourceProp.Name &&
                                                                           x.PropertyType == sourceProp.PropertyType &&
                                                                           x.CanWrite);
                if (destProp != null)
                {
                    destProp.SetValue(destination, sourceProp.GetValue(source));
                }
            }
        }

        /// <summary>
        /// Crea una nueva instancia de <typeparamref name="TDestination"/> a partir de la fuente.
        /// </summary>
        public static TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            if (source == null)
                throw new ArgumentNullException("source cannot be null");

            var destination = new TDestination();
            Map(source, destination);
            return destination;
        }
    }

}
