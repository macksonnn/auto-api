//using MongoDB.Driver;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.NetworkInformation;
//using System.Text;
//using System.Threading.Tasks;

//namespace States.Mongo.Connections
//{
//    public class ConnectionMongo : MongoBaseState
//    {
//        public ConnectionMongo(MongoBaseState stateContext, IDpState dp) : base(stateContext, dp)
//        {
//        }
//        public IMongoCollection<DevPrime.State.Repositories.Client.Model.Client> Client
//        {
//            get
//            {
//                return Db.GetCollection<DevPrime.State.Repositories.Client.Model.Client>("Client");
//            }
//        }
//        public IMongoCollection<DevPrime.State.Repositories.Subscription.Model.Subscription> Subscription
//        {
//            get
//            {
//                return Db.GetCollection<DevPrime.State.Repositories.Subscription.Model.Subscription>("Subscription");
//            }
//        }
//    }
//}
