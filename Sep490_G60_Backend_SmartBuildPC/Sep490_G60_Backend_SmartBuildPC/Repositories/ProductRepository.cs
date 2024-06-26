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

                var list = await _context.Products.Include(x => x.Category).Where(x => x.ProductId == id).Select(n => new ProductDTO { ProductId = n.ProductId, CategoryName = n.Category.CategoryName, ProductName = n.ProductName, Price = n.Price, Description = n.Description, Warranty = n.Warranty, Brand = n.Brand }).SingleAsync();

                return list;
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
                        ProductName = n.ProductName
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
                                      join p in _context.Products on pb.Product.ProductId equals p.ProductId
                                      where g.Pctype == name
                                      select new ProductDTO
                                      {
                                          ProductId = p.ProductId,

                                          ProductName = p.ProductName,
                                          Description = p.Description,
                                          Price = p.Price,
                                          Warranty = p.Warranty,
                                          Brand = p.Brand
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

        public async Task<PreviewProductDTO> PreviewProduct(string id)
        {
            try
            {
                var product = await _context.Products.Include(x => x.ProductStores).ThenInclude(y => y.Store).FirstOrDefaultAsync(x => x.ProductId.Equals(id));
                List<StoreDTO> stores = new();
                if (product != null)
                {
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

                return null;

                
                
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the products to preview.", ex);
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

    }
}
