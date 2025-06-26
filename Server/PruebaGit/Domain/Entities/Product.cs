using PruebaRaddarStudios.Common.Abstract;
using PruebaRaddarStudios.Domain.Entities.ValueObjects;

namespace PruebaRaddarStudios.Domain.Entities;

public class Product : TEntity
{
    public required Nombre Nombre { get; set; }
    public required Descripcion Descripcion { get; set; }
    public required Precio Precio { get; set; }
    public required Stock Stock { get; set; }
    public required DateTime FechaCreacion { get; set; }

    protected Product() { }

    public static Product Build(int id, string nombre, string descripcion, decimal precio, int stock, DateTime fechaCreacion)
    {
        return new Product
        {
            Id = id,
            Nombre = new Nombre(nombre),
            Descripcion = new Descripcion(descripcion),
            Precio = new Precio(precio),
            Stock = new Stock(stock),
            FechaCreacion = fechaCreacion
        };
    }

    public static Product Create(string nombre, string descripcion, decimal precio, int stock)
    {
        return new Product
        {
            Nombre = new Nombre(nombre),
            Descripcion = new Descripcion(descripcion),
            Precio = new Precio(precio),
            Stock = new Stock(stock),
            FechaCreacion = DateTime.Now
        };
    }

    public void Update(string nombre, string descripcion, decimal precio, int stock)
    {
        Nombre = new Nombre(nombre);
        Descripcion = new Descripcion(descripcion);
        Precio = new Precio(precio);
        Stock = new Stock(stock);
    }
    
}