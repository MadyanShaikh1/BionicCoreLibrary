using System.Reflection;

namespace BionicCoreLibrary.Common.Helper
{
    public class Projection
    {
        /// <summary>
        /// Converts an object of type TSource to an object of type TDestination,
        /// assuming that property names of both source and destination objects are the same.
        /// </summary>
        /// <typeparam name="TDestination">The type of the destination object.</typeparam>
        /// <typeparam name="TSource">The type of the source object.</typeparam>
        /// <param name="project">The source object to be converted.</param>
        //
        public static TDestination Project<TDestination>(object project)
        {
            var result = Activator.CreateInstance<TDestination>();

            var properties = typeof(TDestination).GetProperties();

            foreach (var property in properties)
            {
                var columnName = GetColumnName(property);

                if (!string.IsNullOrEmpty(columnName))
                {
                    var sourceProperty = project.GetType().GetProperties()
                        .FirstOrDefault(p => string.Equals(GetColumnName(p), columnName, StringComparison.OrdinalIgnoreCase));

                    if (sourceProperty != null)
                    {
                        var value = sourceProperty.GetValue(project);
                        if (value != null)
                        {
                            property.SetValue(result, Convert.ChangeType(value, property.PropertyType));
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Converts an object List of type TSource to an object List of type TDestination,
        /// assuming that property names of both source and destination objects are the same.
        /// </summary>
        /// <typeparam name="TDestination">The type of the destination object.</typeparam>
        /// <typeparam name="TSource">The type of the source object.</typeparam>
        /// <param name="project">The source object to be converted.</param>
        //
        public static List<TDestination> Project<TDestination>(IEnumerable<object> projects)
        {
            var results = new List<TDestination>();
            var properties = typeof(TDestination).GetProperties();

            foreach (var project in projects)
            {
                var result = Activator.CreateInstance<TDestination>();

                foreach (var property in properties)
                {
                    var columnName = GetColumnName(property);

                    if (!string.IsNullOrEmpty(columnName))
                    {
                        var sourceProperty = project.GetType().GetProperties()
                            .FirstOrDefault(p => string.Equals(GetColumnName(p), columnName, StringComparison.OrdinalIgnoreCase));

                        if (sourceProperty != null)
                        {
                            var value = sourceProperty.GetValue(project);
                            if (value != null)
                            {
                                property.SetValue(result, Convert.ChangeType(value, property.PropertyType));
                            }
                        }
                    }
                }

                results.Add(result);
            }

            return results;
        }

        private static string GetColumnName(PropertyInfo property)
        {
            var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();
            return columnAttribute?.Name ?? property.Name;
        }

    }
}
