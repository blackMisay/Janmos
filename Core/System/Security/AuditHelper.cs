using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.System.Security
{
    public static class AuditHelper
    {
        public static (string oldJson, string newJson, string description) GetDifferences<T>(T oldObject, T newObject)
        {
            Dictionary<string, object> oldValues = new Dictionary<string, object>();
            Dictionary<string, object> newValues = new Dictionary<string, object>();
            StringBuilder descriptionBuilder = new StringBuilder();

            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                var oldValue = prop.GetValue(oldObject);
                var newValue = prop.GetValue(newObject);

                bool changed =
                    (oldValue == null && newValue != null) ||
                    (oldValue != null && newValue == null) ||
                    (oldValue != null && !oldValue.Equals(newValue));

                if (changed)
                {
                    string fieldName = prop.Name;
                    string oldValStr = oldValue?.ToString() ?? "null";
                    string newValStr = newValue?.ToString() ?? "null";

                    oldValues[fieldName] = oldValue;
                    newValues[fieldName] = newValue;

                    descriptionBuilder.AppendLine($"- {fieldName} changed from '{oldValStr}' to '{newValStr}'");
                }
            }
            string oldJson = JsonConvert.SerializeObject(oldValues, Formatting.None);
            string newJson = JsonConvert.SerializeObject(newValues, Formatting.None);
            string description = descriptionBuilder.Length > 0
                ? descriptionBuilder.ToString().Trim()
                : "No changes detected.";

            return (oldJson, newJson, description);
        }
    }
}
