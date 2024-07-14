using Microsoft.EntityFrameworkCore;
using Sep490_G60_Backend_SmartBuildPC.Models;
using Sep490_G60_Backend_SmartBuildPC.Responses;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public class BillRepository: IBillRepository
    {
        private readonly SMARTPCContext _context;

        public BillRepository(SMARTPCContext context)
        {
            _context = context;
        }

        public async Task CreateBillAsync(CreateBillDTO createBillDTO)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var bill = new Bill
                    {
                        OrderId = createBillDTO.OrderId,
                        BillDate = createBillDTO.BillDate,
                        TaxIn = createBillDTO.TaxIn,
                        Address = createBillDTO.Address
                    };

                    _context.Bills.Add(bill);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new InvalidOperationException($"Error creating bill: {ex.Message}");
                }
            }



        }


        public async Task<List<BillDTO>> GetAllBillsAsync()
        {
            return await _context.Bills
                .Select(b => new BillDTO
                {
                    BillID = b.BillId,
                    OrderID = (int)b.OrderId,
                    BillDate = (DateTime)b.BillDate,
                    TaxIN = (int)b.TaxIn,
                    Address = b.Address
                })
                .ToListAsync();
        }
    }
}

