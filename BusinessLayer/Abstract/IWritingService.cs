using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface IWritingService : IGenericService<Writing>
	{
        void TDeleteSql(Writing t);
    }
}
