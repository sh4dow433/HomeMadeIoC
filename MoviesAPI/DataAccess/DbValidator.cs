using MoviesAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DataAccess;

public static class DbValidator
{
    public static bool IsValid(Entity entity, out List<ValidationResult> validationResults)
    {
        var vc = new ValidationContext(entity);
        validationResults = new();
        return Validator.TryValidateObject(entity, vc, validationResults);
    }
}
