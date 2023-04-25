using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class WritingManager : IWritingService
	{
        IWritingDal _writingDal;

        public WritingManager(IWritingDal writingDal)
        {
            _writingDal = writingDal;
        }

        public void TAdd(Writing t)
        {
            _writingDal.Insert(t);
        }

        public void TDelete(Writing t)
        {
            _writingDal.Delete(t);
        }

        public void TDeleteSql(Writing t)
        {
            _writingDal.DeleteSql(t);
        }

        public Writing TGetByID(int id)
        {
            return _writingDal.Get(x => x.WritingId == id);
        }

        public List<Writing> TGetList()
        {
            return _writingDal.GetList();
        }

        public void TUpdate(Writing t)
        {
            _writingDal.Update(t);
        }
    }
}
