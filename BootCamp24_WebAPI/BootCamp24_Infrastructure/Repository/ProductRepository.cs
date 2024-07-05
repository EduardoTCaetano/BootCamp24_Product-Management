using BootCamp24_Domain.Interfaces.Repository;
using BootCamp24_Domain.Models;
using BootCamp_WebAPI.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProductRepository : IProductRepository
{
    private readonly DataContext _context;

    public ProductRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductModel>> GetAll()
    {
        return await _context.Product.ToListAsync();
    }

    public async Task<ProductModel> GetById(Guid id)
    {
        return await _context.Product.FindAsync(id);
    }

    public async Task Add(ProductModel product)
    {
        await _context.Product.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task Update(ProductModel product)
    {
        _context.Product.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var product = await _context.Product.FindAsync(id);
        if (product != null)
        {
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> Exists(Guid id)
    {
        return await _context.Product.AnyAsync(e => e.Id == id);
    }
}
