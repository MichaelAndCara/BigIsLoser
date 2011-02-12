using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigISLoser.Repositories
{
    public class BaseBISL
    {
        //public const string ConnectionString = @"metadata=res://*/Repositories.MACEntityDataModel.csdl|res://*/Repositories.MACEntityDataModel.ssdl|res://*/Repositories.MACEntityDataModel.msl;provider=System.Data.SqlClient;provider connection string='Data Source=maclaptop\sqlexpress;Initial Catalog=BigIsLoser;Persist Security Info=True;User ID=macdbuser;Password=cjogre77'";
        public const string ConnectionString = @"metadata=res://*/Repositories.BISL_EntityDataModel.csdl|res://*/Repositories.BISL_EntityDataModel.ssdl|res://*/Repositories.BISL_EntityDataModel.msl;provider=System.Data.SqlClient;provider connection string='Data Source=.\sqlexpress;Initial Catalog=BigIsLoser;Persist Security Info=True;User ID=macdbuser;Password=cjogre77'";
    }
}