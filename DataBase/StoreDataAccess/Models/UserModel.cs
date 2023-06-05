using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StoreDataAccess.Models;
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get ; set; }
        public string _firstName { get; set; }
        public string _lastName { get; set; }
        public string _fullName => $"{_firstName} {_lastName}";
        public DateOnly _dateOfBirth { get; set; }
        public string _email { get; set; }
        public string _password { get; set; }
}
