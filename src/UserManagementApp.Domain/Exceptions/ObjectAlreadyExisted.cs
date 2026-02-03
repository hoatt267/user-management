namespace UserManagementApp.Domain.Exceptions
{
    public class ObjectAlreadyExistedException : Exception
    {
        public ObjectAlreadyExistedException(string entityName, string value, string table)
            : base($"The {entityName} with value {value} existed already in {table} table.")
        {
        }
    }
}