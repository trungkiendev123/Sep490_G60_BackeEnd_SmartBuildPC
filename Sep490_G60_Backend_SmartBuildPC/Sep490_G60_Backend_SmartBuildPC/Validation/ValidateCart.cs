﻿using Sep490_G60_Backend_SmartBuildPC.Models;
using Sep490_G60_Backend_SmartBuildPC.Requests;

namespace Sep490_G60_Backend_SmartBuildPC.Validation
{
    public class ValidateCart
    {
        private readonly SMARTPCContext _context;
        public ValidateCart(SMARTPCContext context)
        {
            _context = context;
        }
        public List<string> validateCart(ChangeCartRequest request)
        {
            List<string> errors = new List<string>();
            try
            {
                if(request.Quantity < 0)
                {
                    errors.Add("Quantity must > 0");
                }
                if (!isExistProductID(request.ProductID))
                {
                    errors.Add("ProductID not exist");
                }

            }
            catch (Exception e)
            {
                errors.Add(e.Message);
            }
            return errors;
        }
        public bool isExistProductID(int productID)
        {
            var ids = _context.Products.ToList().Select(x => x.ProductId);
            if (!ids.Contains(productID))
            {
                return false;
            }
            return true;
        }
    }
}
