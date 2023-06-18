using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDataAccess.DataAccess;

namespace MongoDataAccess.Models;

public class PlayerModel
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JerseyNumber { get; set; }
    public string Position { get; set; }
    public string Height { get; set; }
    public string Weight { get; set; }
    public UserModel AddedBy { get; set; }

    public static PlayerModel CreatePlayer(UserModel userLogged)
    {
        PlayerModel player = new PlayerModel();
        string msg, temp;

        Console.Clear();

        msg = "Enter first name of player: ";
        temp = Validate.StringInput(msg);
        player.FirstName = Validate.LettersSize(temp);

        msg = "Enter last name of player: ";
        temp = Validate.StringInput(msg);
        player.LastName = Validate.LettersSize(temp);

        msg = "Enter player's jersey number: ";
        temp = Validate.StringInput(msg);
        player.JerseyNumber = Validate.LettersSize(temp);

        msg = "Enter the position of player: ";
        temp = Validate.StringInput(msg);
        player.Position = Validate.LettersSize(temp);

        msg = "Enter the height of player: ";
        temp = Validate.StringInput(msg);
        player.Height = Validate.LettersSize(temp);

        msg = "Enter the weight of player: ";
        temp = Validate.StringInput(msg);
        player.Weight = Validate.LettersSize(temp);

        player.AddedBy = userLogged;

        return player;
    }
    public static void Print(List<PlayerModel> playersResult)
    {
        ConsoleKeyInfo i;
        Console.Clear();
        foreach (var player in playersResult)
        {
            Console.WriteLine(
                $"First name: {player.FirstName}\n" +
                $"Last name: {player.LastName}\n" +
                $"Jersey number: {player.JerseyNumber}\n" +
                $"Position: {player.Position}\n" +
                $"Height: {player.Height}\n" +
                $"Weight: {player.Weight}\n" +
                $"Added by: {player.AddedBy.Login}\n");
        }           
    }
}
