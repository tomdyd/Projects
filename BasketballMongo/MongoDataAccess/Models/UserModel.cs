using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDataAccess.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public static UserModel CreateUser()
        {
            UserModel user = new UserModel();
            string msg, temp, temp1;

            msg = "Enter your login: ";
            temp = Validate.StringInput(msg);
            user.Login = temp.ToLower();

            msg = "Set your password: ";
            temp = Validate.SetPassword(msg);
            user.Password = temp;

            return user;
        }
    }
}
