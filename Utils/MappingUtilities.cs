namespace EasyConfig.Utils {
    /// <summary>
    /// Utility for mapping values to fields or variables.
    /// </summary>
    public class MappingUtilities {
        public void MapAllFields(object source, object dst)
        {
            System.Reflection.FieldInfo[] ps = source.GetType().GetFields();
            bool isField = false;
            foreach (var item in ps)
            {
                var o = item.GetValue(source);
                var p = dst.GetType().GetField(item.Name);
                if (p != null)
                {
                    Type t = Nullable.GetUnderlyingType(p.FieldType) ?? p.FieldType;
                    object safeValue = (o == null) ? null : Convert.ChangeType(o, t);
                    p.SetValue(dst, safeValue);
                }
            }
        }
        
        public void MapAllProperties(object source, object dst)
        {
            System.Reflection.PropertyInfo[] ps = source.GetType().GetProperties();
            foreach (var item in ps)
            {
                var o = item.GetValue(source);
                var p = dst.GetType().GetProperty(item.Name);
                if (p != null)
                {
                    Type t = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
                    object safeValue = (o == null) ? null : Convert.ChangeType(o, t);
                    p.SetValue(dst, safeValue);
                }
            }
        }
    }
}