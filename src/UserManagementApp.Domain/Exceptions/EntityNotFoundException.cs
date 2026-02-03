namespace UserManagementApp.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entity, object? id = null)
        : base(id == null ? $"Default {entity} not found." : $"{entity}: {id} not found.")
        {
        }
    }
}