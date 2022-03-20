using ASP_Net_MVC.Data;
using ASP_Net_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_Net_MVC.Helpers
{
        //public interface IAddressManager
        //{
        //    Task<IEnumerable<AppAddress>> GetAddressesAsync();
        //    Task<AppAddress> GetUserAddressAsync(AppUser user);
        //    Task CreateUserAddressAsync(AppUser user, AppAddress address);

        //}


        //public class AddressManager : IAddressManager
        //{


        //    private readonly ApplicationDbContext _context;

        //    public AddressManager(ApplicationDbContext context)
        //    {
        //        _context = context;
        //    }

        //    //Create User Address
        //    public async Task CreateUserAddressAsync(AppUser user, AppAddress address)
        //    {
        //        var userAddress = new AppUserAddress();

        //        var _address = await _context.Addresses.FirstOrDefaultAsync(x => x.AddressLine == address.AddressLine && x.PostalCode == address.PostalCode);

        //        if(_address == null)
        //        {
        //            _context.Addresses.Add(address);
        //            await _context.SaveChangesAsync();
                
        //            userAddress = new AppUserAddress { UserId = user.Id, AddressId = address.Id };
        //    }
        //    else
        //        {
        //        userAddress = new AppUserAddress { UserId = user.Id, AddressId = _address.Id };
        //        }


        //        _context.UserAddresses.Add(userAddress);
        //        await _context.SaveChangesAsync();
        //    }


        //    //Gets all addresses
        //public async Task<IEnumerable<AppAddress>> GetAddressesAsync()
        //    {
        //        return await _context.Addresses.ToListAsync();
        //    }

            
        //    //Get user address
        //    public async Task<AppAddress> GetUserAddress(AppUser user)
        //    {
        //        var result = await _context.UserAddresses.Include(t => t.Address).FirstOrDefaultAsync(x => x.UserId == user.Id);
        //        return result.Address;
        //    }

        //    public Task<AppAddress> GetUserAddressAsync(AppUser user)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }

