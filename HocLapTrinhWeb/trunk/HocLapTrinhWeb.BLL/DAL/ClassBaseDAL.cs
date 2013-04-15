using System.Data;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.DAL
{
    public class ClassBaseDAL : DataAccess
    {
        public ClassBaseDAL(Connection cnn, DataTable dt)
            : base(cnn, dt == null ? null : dt.TableName)
        {
        }
    }
}
