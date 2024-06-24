﻿using Microsoft.EntityFrameworkCore;
using Sep490_G60_Backend_SmartBuildPC.DTOs;
using Sep490_G60_Backend_SmartBuildPC.Models;
using Sep490_G60_Backend_SmartBuildPC.Responses;
using System.Collections;
using System.Xml.Linq;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public class ProductRepository : IProductRepository
{

    private readonly SMARTPCContext _context;
    public ProductRepository(SMARTPCContext context)
    {
        _context = context;

    }
    public async Task<ProductDTO> GetProduct(int id)
{
    try
    {
        var product = await (from p in _context.Products
                             where p.ProductId == id
                             select new
                             {
                                 p.ProductId,
                                 p.Category.CategoryName,
                                 p.ProductName,
                                 p.Price,
                                 p.Description,
                                 p.Warranty,
                                 p.Brand,
                                 p.Tag,
                                 p.Tdp,
                                 p.ImageLink,
                                 StoreNames = _context.ProductStores
                                                .Where(ps => ps.ProductId == p.ProductId)
                                                .Select(ps => ps.Store.StoreName)
                                                .ToList()
                             }).SingleOrDefaultAsync();

        if (product == null)
        {
            return null;
        }

        var productDTO = new ProductDTO
        {
            ProductId = product.ProductId,
            CategoryName = product.CategoryName,
            ProductName = product.ProductName,
            Price = product.Price,
            Description = product.Description,
            Warranty = product.Warranty,
            Brand = product.Brand,
            Tag = product.Tag,
            TDP = (int)product.Tdp,
            ImageLink = product.ImageLink,
            StoreNames = product.StoreNames
        };

        return productDTO;
    }
    catch (Exception ex)
    {
        throw new Exception("An error occurred while getting the product.", ex);
    }
}


    public async Task<List<ProductDTO>> GetProductByBrand(string brandName)
    {
        try
        {
            var products = await _context.Products
                .Where(x => x.Brand == brandName)
                .Select(n => new ProductDTO
                {
                    ProductId = n.ProductId,
                    CategoryName = n.Category.CategoryName,
                    ProductName = n.ProductName,
                    Description = n.Description,
                    Price = n.Price,
                    Warranty = n.Warranty,
                    Brand = n.Brand,
                    Tag = n.Tag,
                    TDP = (int)n.Tdp
                })
                .ToListAsync();

            return products;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting the products by brand.", ex);
        }
    }






    public async Task<List<ProductDTO>> GetProductByGroup(string name)
    {
        try
        {
            var products = await (from g in _context.Groups
                                  join pb in _context.PcbuildParts on g.PcbuildId equals pb.PcbuildId
                                  join p in _context.Products on pb.ProductId equals p.ProductId
                                  where g.Pctype == name
                                  select new ProductDTO
                                  {
                                      ProductId = p.ProductId,
                                      ProductName = p.ProductName,
                                      Description = p.Description,
                                      Price = p.Price,
                                      Warranty = p.Warranty,
                                      Brand = p.Brand,
                                      Tag = p.Tag,
                                      TDP = (int)p.Tdp,
                                      ImageLink = p.ImageLink,
                                      StoreNames = _context.ProductStores
                                                          .Where(ps => ps.ProductId == p.ProductId)
                                                          .Select(ps => ps.Store.StoreName)
                                                          .ToList()
                                  }).ToListAsync();

            return products;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting the products by group.", ex);
        }
    }




    public async Task<List<ProductDTO>> GetAllProducts(int pageNumber = 1, int pageSize = 50)
    {
        try
        {
            var query = _context.Products.AsQueryable();

            var totalItems = await query.CountAsync();

            var products = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(n => new ProductDTO
                {
                    ProductId = n.ProductId,
                    CategoryName = n.Category.CategoryName,
                    ProductName = n.ProductName,
                    Description = n.Description,
                    Price = n.Price,
                    Warranty = n.Warranty,
                    Brand = n.Brand,
                    Tag = n.Tag,
                    TDP = (int)n.Tdp
                })
                .ToListAsync();

            return products;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting all products.", ex);
        }
    }












    public async Task<IEnumerable<ProductDTO>> GetProductsByCategory(int categoryID)
    {
        try
        {
            var products = await _context.Products.Include(x => x.Category).Where(x => x.CategoryId == categoryID).ToListAsync();

            return products.Select(x => new ProductDTO
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                Brand = x.Brand,
                Warranty = x.Warranty,
                CategoryName = x.Category.CategoryName
            }); ;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting the products by group.", ex);
        }
    }




    


    public async Task<PreviewProductDTO> PreviewProduct(int id)
    {
        try
        {
            var product = await _context.Products.Include(x => x.ProductStores).ThenInclude(y => y.Store).FirstOrDefaultAsync(x => x.ProductId == id);
            List<StoreDTO> stores = new();
            var products_stores = product.ProductStores;
            foreach (ProductStore ps in products_stores)
            {
                stores.Add(new StoreDTO()
                {
                    Name = ps.Store.StoreName,
                    Address = ps.Store.Address
                });
            }
            return new PreviewProductDTO
            {
                price = product.Price,
                Warranty = product.Warranty,
                DetailProduct = product.Description,
                AvaibleStore = stores
            };
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting the products to preview.", ex);
        }
    }



    public async Task<List<ProductDTO>> GetProductsByKeyword(string keyword, int pageNumber = 1, int pageSize = 50)
    {
        try
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(p => p.ProductName.Contains(keyword) || p.Description.Contains(keyword) || p.Tag.Contains(keyword));
            }

            var products = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(n => new ProductDTO
                {
                    ProductId = n.ProductId,
                    CategoryName = n.Category.CategoryName,
                    ProductName = n.ProductName,
                    Description = n.Description,
                    Price = n.Price,
                    Warranty = n.Warranty,
                    Brand = n.Brand,
                    Tag = n.Tag,
                    TDP = (int)n.Tdp
                })
                .ToListAsync();

            return products;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting products by keyword.", ex);
        }
    }



    public async Task<ProductDetailsDTO> GetProductDetailsWithSimilarPriceRange(int id, decimal priceRange)
{
    try
    {
        var product = await (from p in _context.Products
                             where p.ProductId == id
                             select new
                             {
                                 p.ProductId,
                                 p.Category.CategoryName,
                                 p.ProductName,
                                 p.Price,
                                 p.Description,
                                 p.Warranty,
                                 p.Brand,
                                 p.Tag,
                                 p.Tdp,
                                 p.ImageLink,
                                 StoreNames = _context.ProductStores
                                                    .Where(ps => ps.ProductId == p.ProductId)
                                                    .Select(ps => ps.Store.StoreName)
                                                    .ToList()
                             }).SingleOrDefaultAsync();

        if (product == null)
        {
            return null;
        }

        var similarPriceProducts = await (from p in _context.Products
                                          where p.Price >= product.Price - priceRange && p.Price <= product.Price + priceRange && p.ProductId != id
                                          select new ProductDTO
                                          {
                                              ProductId = p.ProductId,
                                              CategoryName = p.Category.CategoryName,
                                              ProductName = p.ProductName,
                                              Price = p.Price,
                                              Description = p.Description,
                                              Warranty = p.Warranty,
                                              Brand = p.Brand,
                                              Tag = p.Tag,
                                              TDP = (int)p.Tdp,
                                              ImageLink = p.ImageLink,
                                              StoreNames = _context.ProductStores
                                                                  .Where(ps => ps.ProductId == p.ProductId)
                                                                  .Select(ps => ps.Store.StoreName)
                                                                  .ToList()
                                          }).ToListAsync();

        var productDetailsDTO = new ProductDetailsDTO
        {
            Product = new ProductDTO
            {
                ProductId = product.ProductId,
                CategoryName = product.CategoryName,
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                Warranty = product.Warranty,
                Brand = product.Brand,
                Tag = product.Tag,
                TDP = (int)product.Tdp,
                ImageLink = product.ImageLink,
                StoreNames = product.StoreNames
            },
            SimilarPriceProducts = similarPriceProducts
        };

        return productDetailsDTO;
    }
    catch (Exception ex)
    {
        throw new Exception("An error occurred while getting the product details with similar price range.", ex);
    }
}



    public async Task<ProductDTO> CreateProduct(CreateProductDTO createProductDTO)
{
    using var transaction = await _context.Database.BeginTransactionAsync();
    try
    {
        
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.CategoryId == createProductDTO.CategoryID);

        if (category == null)
        {
            throw new Exception("Category not found.");
        }

        
        var product = new Product
        {
            CategoryId = createProductDTO.CategoryID,
            ProductName = createProductDTO.ProductName,
            Description = createProductDTO.Description,
            Price = createProductDTO.Price,
            Warranty = createProductDTO.Warranty,
            Brand = createProductDTO.Brand,
            Tag = createProductDTO.Tag,
            Tdp = createProductDTO.TDP,
            ImageLink = createProductDTO.ImageLink
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        
        foreach (var storeStock in createProductDTO.StoreStocks)
        {
            var store = await _context.Stores
                .FirstOrDefaultAsync(s => s.StoreId == storeStock.StoreID);

            if (store == null)
            {
                throw new Exception($"Store with ID {storeStock.StoreID} not found.");
            }

            var productStore = new ProductStore
            {
                ProductId = product.ProductId,
                StoreId = store.StoreId,
                StockQuantity = storeStock.StockQuantity
            };

            _context.ProductStores.Add(productStore);
        }

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        
        var productDTO = new ProductDTO
        {
            ProductId = product.ProductId,
            CategoryID = category.CategoryId,  
            CategoryName = category.CategoryName,
            ProductName = product.ProductName,
            Description = product.Description,
            Price = product.Price,
            Warranty = product.Warranty,
            Brand = product.Brand,
            Tag = product.Tag,
            TDP = (int)product.Tdp,
            ImageLink = product.ImageLink,
            StoreNames = createProductDTO.StoreStocks.Select(ss => _context.Stores.FirstOrDefault(s => s.StoreId == ss.StoreID)?.StoreName).ToList()
        };

        return productDTO;
    }
    catch (Exception ex)
    {
        await transaction.RollbackAsync();
        throw new Exception("An error occurred while creating the product.", ex);
    }
}



public async Task<bool> DeleteProduct(int id)
{
    try
    {
        var product = await _context.Products
            .Include(p => p.ProductStores)
            .FirstOrDefaultAsync(p => p.ProductId == id);

        if (product == null)
        {
            return false;
        }

        _context.ProductStores.RemoveRange(product.ProductStores); 
        _context.Products.Remove(product); 
        await _context.SaveChangesAsync();

        return true;
    }
    catch (Exception ex)
    {
        throw new Exception("An error occurred while deleting the product.", ex);
    }
}



public async Task<ProductDTO> UpdateProduct(int id, UpdateProductDTO updateProductDTO)
{
    try
    {
        var product = await _context.Products
            .Include(p => p.ProductStores)
            .FirstOrDefaultAsync(p => p.ProductId == id);

        if (product == null)
        {
            return null;
        }

        product.CategoryId = updateProductDTO.CategoryID;
        product.ProductName = updateProductDTO.ProductName;
        product.Description = updateProductDTO.Description;
        product.Price = updateProductDTO.Price;
        product.Warranty = updateProductDTO.Warranty;
        product.Brand = updateProductDTO.Brand;
        product.Tag = updateProductDTO.Tag;
        product.Tdp = updateProductDTO.TDP;
        product.ImageLink = updateProductDTO.ImageLink;

        
        foreach (var storeStock in updateProductDTO.StoreStocks)
        {
            var productStore = product.ProductStores
                .FirstOrDefault(ps => ps.StoreId == storeStock.StoreID);

            if (productStore == null)
            {
                product.ProductStores.Add(new ProductStore
                {
                    ProductId = product.ProductId,
                    StoreId = storeStock.StoreID,
                    StockQuantity = storeStock.StockQuantity
                });
            }
            else
            {
                productStore.StockQuantity = storeStock.StockQuantity;
            }
        }

        _context.Products.Update(product);
        await _context.SaveChangesAsync();

        
        var productDTO = new ProductDTO
        {
            ProductId = product.ProductId,
            CategoryID = (int)product.CategoryId,
            ProductName = product.ProductName,
            Description = product.Description,
            Price = product.Price,
            Warranty = product.Warranty,
            Brand = product.Brand,
            Tag = product.Tag,
            TDP = (int)product.Tdp,
            ImageLink = product.ImageLink,
            StoreNames = updateProductDTO.StoreStocks.Select(ss => _context.Stores.FirstOrDefault(s => s.StoreId == ss.StoreID)?.StoreName).ToList()
        };

        return productDTO;
    }
    catch (Exception ex)
    {
        throw new Exception("An error occurred while updating the product.", ex);
    }
}


    
    
    
    public async Task<List<ProductDTO>> FilterProducts(ProductFilterDTOHome filterDTO)
{
    var query = _context.Products.AsQueryable();

    if (!string.IsNullOrEmpty(filterDTO.StoreName))
    {
        query = query.Where(p => p.ProductStores.Any(ps => ps.Store.StoreName == filterDTO.StoreName));
    }

    if (filterDTO.PriceFrom.HasValue)
    {
        query = query.Where(p => p.Price >= filterDTO.PriceFrom.Value);
    }

    if (filterDTO.PriceTo.HasValue)
    {
        query = query.Where(p => p.Price <= filterDTO.PriceTo.Value);
    }

    

    if (!string.IsNullOrEmpty(filterDTO.Category))
    {
        query = query.Where(p => p.Category.CategoryName == filterDTO.Category);
    }

    var products = await query.Select(p => new ProductDTO
    {
        ProductId = p.ProductId,
        ProductName = p.ProductName,
        Description = p.Description,
        Price = p.Price,
        Warranty = p.Warranty,
        Brand = p.Brand,
        Tag = p.Tag,
        TDP = (int)p.Tdp,
        ImageLink = p.ImageLink,
        
        CategoryName = p.Category.CategoryName,
        StoreNames = p.ProductStores.Select(ps => ps.Store.StoreName).ToList()
    }).ToListAsync();

    return products;
}


}
}
