namespace API_PrototipoGestionPAP.Utils
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class PermissionAttribute : Attribute
    {
        /// <summary>
        /// Código de permiso requerido (por ejemplo, "Reportes|Escritura")
        /// </summary>
        public string PermissionCode { get; }

        public PermissionAttribute(string permissionCode)
        {
            PermissionCode = permissionCode;
        }
    }
}
