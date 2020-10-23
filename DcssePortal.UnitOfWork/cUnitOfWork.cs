using DcssePortal.Data;
using DcssePortal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcssePortal.UnitOfWork
{
  public class cUnitOfWork
  {
    private ApplicationDbContext _DbContext;
    public cUnitOfWork()
    {
      _DbContext = new ApplicationDbContext();
      ApplicationUserRepository = new cApplicationUserRepository(_DbContext);
    }
    public cApplicationUserRepository ApplicationUserRepository{ get; set; }


    public void Complete()
    {
      _DbContext.SaveChanges();
    }
  }
}
