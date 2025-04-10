using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaAmaris.Models.Employee;

public class Employee
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Range(0, 120, ErrorMessage = "La edad debe estar entre 0 y 120.")]
    public int Age { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "El salario no puede ser negativo.")]
    public int Salary { get; set; }

    [Required(ErrorMessage = "La imagen de perfil es obligatoria.")]
    public string ProfileImg { get; set; } = string.Empty;

    public int AnualSalary => Salary * 12;
}

