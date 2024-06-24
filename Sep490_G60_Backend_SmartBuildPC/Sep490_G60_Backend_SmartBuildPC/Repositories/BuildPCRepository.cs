using Microsoft.EntityFrameworkCore;
using Sep490_G60_Backend_SmartBuildPC.Models;
using Sep490_G60_Backend_SmartBuildPC.Responses;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public class BuildPCRepository : IBuildPCRepository
    {
        private readonly SMARTPCContext _context;
        public BuildPCRepository (SMARTPCContext context)
        {
            _context = context;
        }

        public void AddProductToPC(int productID)
        {
            try
            {
                

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while do this action", ex);
            }
        }

        public async Task<IEnumerable<FilterDTO>> getFilterOfCategory(int cate_id)
        {
            try
            {
                var filter_categories = await _context.FilterTypeCategories.Include(X => X.FilterType).Where(x => x.CategoryId == cate_id).ToListAsync();
                List<FilterDTO> dtos = new List<FilterDTO>();
                foreach (var item in filter_categories)
                {
                    FilterDTO dto = new FilterDTO();
                    dto.FilterName = item.FilterType.FilterType1;
                    List<string> values = new List<string>();
                    if (dto.FilterName.Contains("Brand"))
                    {
                        var products = await _context.Products.Where(x => x.CategoryId == cate_id).ToListAsync();
                        var brands = products.Select(x => x.Brand).Distinct().ToList();
                        foreach(var brand in brands)
                        {
                            values.Add(brand);
                        }
                    }
                    else
                    {
                        var FilterStrings = await _context.FilterStrings.Where(X => X.FilterTypeCategoryId == item.FilterTypeCategoryId).ToListAsync();
                        foreach(var fValue in FilterStrings)
                        {
                            values.Add(fValue.FilterString1);
                        }
                    }
                    dto.FilterValue = values;
                    dtos.Add(dto);

                }
                return dtos;


            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while do this action", ex);
            }
        }

        public async Task<IEnumerable<CategoryDTO>> listCategory()
        {
            try
            {
                var list = await _context.Categories.ToListAsync();
                return  list.Select(x => new CategoryDTO
                {
                    CategoryName= x.CategoryName,
                    CategoryId= x.CategoryId
                });

            }catch(Exception ex)
            {
                throw new Exception("An error occurred while do this action", ex);
            }
        }

        public async Task<MatchingDTO> GetMatching(string email,int buildID)
        {
            try
            {
                int totalTDP = 0;
                int? supplier = null;
                List<string> tags = new List<string>();
                var account = _context.Accounts.FirstOrDefault(x => x.Email.Equals(email));
                var customer = _context.Customers.FirstOrDefault(x => x.AccountId.Equals(account.AccountId));
                var buildPC = await _context.Pcbuilds.FirstOrDefaultAsync(x => x.CustomerId.ToString().ToUpper().Equals(customer.CustomerId.ToString().ToUpper()) && x.PcbuildId == buildID);
                if(buildPC != null)
                {
                    var items = await _context.PcbuildParts.Include(x => x.Product).Where(x => x.PcbuildId == buildPC.PcbuildId).ToListAsync();
                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            totalTDP += (int)item.Product.Tdp;
                            if(item.Product.CategoryId == 7)
                            {
                                supplier = item.Product.Tdp;
                            }
                            tags.Add(item.Product.Tag);

                        }
                    }
                }
                return new MatchingDTO()
                {
                    totalTDP = totalTDP,
                    listTag = tags,
                    supplierChoosen = supplier
                };
               
                
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while do this action", ex);
            }
        }
    }
}
