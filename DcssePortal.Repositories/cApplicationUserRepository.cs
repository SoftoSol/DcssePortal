using DcssePortal.Data;
using DcssePortal.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcssePortal.Repositories
{
  public class cApplicationUserRepository
  {
    private ApplicationDbContext _DbContext;
    public cApplicationUserRepository(ApplicationDbContext dbContext)
    {
      _DbContext = dbContext;
    }

    public ApplicationUser Set(ApplicationUser newUser)
    {
      return _DbContext.Users.Add(newUser);
    }

    public void Update(ApplicationUser newUser)
    {
      _DbContext.Users.AddOrUpdate(newUser);
    }

    public ApplicationUser Delete(ApplicationUser newUser)
    {
      return _DbContext.Users.Remove(newUser);
    }

    public ApplicationUser Authenticate(string username, string password)
    {
      return null;
    }
  }
}
