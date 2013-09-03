using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public static class FootballDb_Gateway
{
    private static string connectionstring = String.Empty;
    //private static Exception exception;

    /// <summary> Static constructor for this class </summary>
    static FootballDb_Gateway()
    {
        try
        {
            // Get the connection string from the web.config file
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["football_database"];
            connectionstring = settings.ConnectionString;
        }
        catch (Exception ee)
        {
            throw new ApplicationException("Error retrieving necessary values from the web.config file for TravelDb_Gateway static constructor.", ee);
        }
    }

    public static DataTable SELECT_USER()
    {
        //?can we just call above 'footballdb_gateway'?
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("Get_All_Users", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet tmp = new DataSet();
        adapter.Fill(tmp);
        return tmp.Tables[0];
    }

    public static DataTable SELECT_SETTINGS(string UserID)
    {
      //?can we just call above 'footballdb_gateway'?
      SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
      

      SqlCommand cmd = new SqlCommand("Get_User_Settings", connection);
      cmd.CommandType = CommandType.StoredProcedure;

      cmd.Parameters.AddWithValue("@userID", UserID);

      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      DataSet tmp = new DataSet();
      adapter.Fill(tmp);
      return tmp.Tables[0];
    }

    public static DataTable SELECT_USER_MESSAGES(string UserID)
    {
      //?can we just call above 'footballdb_gateway'?
      SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");


      SqlCommand cmd = new SqlCommand("Get_User_Messages", connection);
      cmd.CommandType = CommandType.StoredProcedure;

      cmd.Parameters.AddWithValue("@userID", UserID);

      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      DataSet tmp = new DataSet();
      adapter.Fill(tmp);
      return tmp.Tables[0];
    }

    public static DataTable SELECT_GUESS(string userID, int seasonID, int gameID)
    {
        //?can we just call above 'footballdb_gateway'?
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("Get_Guess_From_UserID", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@userID", userID);
        cmd.Parameters.AddWithValue("@seasonID", seasonID);
        cmd.Parameters.AddWithValue("@gameID", gameID);

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet tmp = new DataSet();
        adapter.Fill(tmp);
        return tmp.Tables[0];
    }

    public static DataTable Get_GuessBoard(int seasonID)
    {
      //?can we just call above 'footballdb_gateway'?
      SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");


      SqlCommand cmd = new SqlCommand("Get_All_Guesses_Scores", connection);
      cmd.CommandType = CommandType.StoredProcedure;

      cmd.Parameters.AddWithValue("@seasonID", seasonID);

      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      DataSet tmp = new DataSet();
      adapter.Fill(tmp);
      return tmp.Tables[0];
    }

    public static DataTable SELECT_USER_STATS(string UserID, int seasonID)
    {
        //?can we just call above 'footballdb_gateway'?
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("Get_UserStats_From_UserID", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@userID", UserID);
        cmd.Parameters.AddWithValue("@seasonID", seasonID);

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet tmp = new DataSet();
        adapter.Fill(tmp);
        return tmp.Tables[0];
    }

    public static DataTable GAME_ISOPEN()
    {
        //?can we just call above 'footballdb_gateway'?
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("Get_Game_IsOpen", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet tmp = new DataSet();
        adapter.Fill(tmp);
        return tmp.Tables[0];

    }

    public static int CHANGE_GAME_ISOPEN(int gameID, int seasonID)
    {
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("Change_Game_IsOpen", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@gameID", gameID);
        cmd.Parameters.AddWithValue("@seasonID", seasonID);

        connection.Open();
        cmd.ExecuteNonQuery();
        connection.Close();
        return 0;
    }

    public static DataTable SELECT_USER(string UserID)
    {
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("Get_User_From_UserID", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@userID", UserID);

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet tmp = new DataSet();
        adapter.Fill(tmp);
        return tmp.Tables[0];
    }

    public static DataTable AUTH_USER(string UserID, string Password)
    {
        //?can we just call above 'footballdb_gateway'?
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("Auth_User", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@userID", UserID);
        cmd.Parameters.AddWithValue("@password", Password);

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet tmp = new DataSet();
        adapter.Fill(tmp);
        return tmp.Tables[0];
    }

    public static int SAVE_USER_INFO( string UserID, string DisplayName)
    {
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("Save_User_Info", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@userID", UserID);
        cmd.Parameters.AddWithValue("@displayName", DisplayName);

        connection.Open();
        cmd.ExecuteNonQuery();
        connection.Close();
        return 0;
    }

    public static int REGISTER_UFAD(string userID)
    {
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("Register_UFAD", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@userID", userID);

        connection.Open();
        cmd.ExecuteNonQuery();
        connection.Close();
        return 0;
    }

    public static int SAVE_GUESS(int gameID, int seasonID, string userID, int guess)
    {
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("SAVE_GUESS", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@gameID", gameID);
        cmd.Parameters.AddWithValue("@seasonID", seasonID);
        cmd.Parameters.AddWithValue("@userID", userID);
        cmd.Parameters.AddWithValue("@guess", guess);

        connection.Open();
        cmd.ExecuteNonQuery();
        connection.Close();
        return 0;
    }

    public static int SAVE_GAME_SCORE(int gameID, int seasonID, int scoreOne, int scoreTwo, int calcScoreDiffOneTwo, int calcScoreDiffTwoOne)
    {
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("SAVE_GAME_SCORE", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@gameID", gameID);
        cmd.Parameters.AddWithValue("@seasonID", seasonID);
        cmd.Parameters.AddWithValue("@scoreOne", scoreOne);
        cmd.Parameters.AddWithValue("@scoreTwo", scoreTwo);
        cmd.Parameters.AddWithValue("@scoreDiffOneTwo", calcScoreDiffOneTwo);
        cmd.Parameters.AddWithValue("@scoreDiffTwoOne", calcScoreDiffTwoOne);

        connection.Open();
        cmd.ExecuteNonQuery();
        connection.Close();
        return 0;
    }

    public static DataTable MATH1(int GameID, int SeasonID)
    {
        //?can we just call above 'footballdb_gateway'?
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("Do_Math", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@gameID", GameID);
        cmd.Parameters.AddWithValue("@seasonID", SeasonID);

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet tmp = new DataSet();
        adapter.Fill(tmp);
        return tmp.Tables[0];
    }
    
    public static int MATH2(string userID, int scoreDiff, int score, int gameID, int seasonID)
    {
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("Do_Math2", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@userID", userID);
        cmd.Parameters.AddWithValue("@scoreDiff", scoreDiff);
        cmd.Parameters.AddWithValue("@score", score);
        cmd.Parameters.AddWithValue("@gameID", gameID);
        cmd.Parameters.AddWithValue("@seasonID", seasonID);

        connection.Open();
        cmd.ExecuteNonQuery();
        connection.Close();
        return 0;
    }

    public static DataTable SELECT_GAMES(int seasonID)
    {
        //?can we just call above 'footballdb_gateway'?
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("Get_All_Games", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@seasonID", seasonID);

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet tmp = new DataSet();
        adapter.Fill(tmp);
        return tmp.Tables[0];
    }
    
    public static DataTable SELECT_STATS()
    {
        //?can we just call above 'footballdb_gateway'?
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("Get_All_Stats", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        //cmd.Parameters.AddWithValue("@seasonID", seasonID);

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet tmp = new DataSet();
        adapter.Fill(tmp);
        return tmp.Tables[0];
    }

    public static DataTable SELECT_SCORES()
    {
        //?can we just call above 'footballdb_gateway'?
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("Get_All_Scores", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        //cmd.Parameters.AddWithValue("@seasonID", seasonID);

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet tmp = new DataSet();
        adapter.Fill(tmp);
        return tmp.Tables[0];
    }
    
    public static int SET_USER_RANK(int userRank, string userRankID)
    {
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("Set_User_Rank", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@userRank", userRank);
        cmd.Parameters.AddWithValue("@userRankID", userRankID);

        connection.Open();
        cmd.ExecuteNonQuery();
        connection.Close();
        return 0;
    }

    public static int ADMIN_SAVE_GAME_INFO(int adminGameID, int adminSeasonID, int GameScoreTeam1, int GameScoreTeam2, int GameScoreDiffT1T2, int GameScoreDiffT2T1)
    {
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("ADMIN_SAVE_GAME_INFO", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@gameID", adminGameID);
        cmd.Parameters.AddWithValue("@seasonID", adminSeasonID);
        cmd.Parameters.AddWithValue("@scoreOne", GameScoreTeam1);
        cmd.Parameters.AddWithValue("@scoreTwo", GameScoreTeam2);
        cmd.Parameters.AddWithValue("@scoreDiffOneTwo", GameScoreDiffT1T2);
        cmd.Parameters.AddWithValue("@scoreDiffTwoOne", GameScoreDiffT2T1);

        connection.Open();
        cmd.ExecuteNonQuery();
        connection.Close();
        return 0;
    }
    
    public static DataTable ADMIN_GET_USERSTATS_INFO(int adminGameID, int adminSeasonID)
    {
        //?can we just call above 'footballdb_gateway'?
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("ADMIN_GET_USERSTATS_INFO", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@gameID", adminGameID);
        cmd.Parameters.AddWithValue("@seasonID", adminSeasonID);

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet tmp = new DataSet();
        adapter.Fill(tmp);
        return tmp.Tables[0];
    }

    public static DataTable ADMIN_GET_USER_RANKS()
    {
      //?can we just call above 'footballdb_gateway'?
      SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
      

      SqlCommand cmd = new SqlCommand("ADMIN_GET_USER_RANKS", connection);
      cmd.CommandType = CommandType.StoredProcedure;

      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      DataSet tmp = new DataSet();
      adapter.Fill(tmp);
      return tmp.Tables[0];
    }

    public static int ADMIN_ADD_USER_RANK(string userID, int gameID, int seasonID, int rank)
    {
      SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
      

      SqlCommand cmd = new SqlCommand("ADMIN_ADD_USER_RANK", connection);
      cmd.CommandType = CommandType.StoredProcedure;

      cmd.Parameters.AddWithValue("@userID", userID);
      cmd.Parameters.AddWithValue("@gameID", gameID);
      cmd.Parameters.AddWithValue("@seasonID", seasonID);
      cmd.Parameters.AddWithValue("@rank", rank);


      connection.Open();
      cmd.ExecuteNonQuery();
      connection.Close();
      return 0;
    }

    public static int SAVE_MESSAGE(string toUserID, string fromUserID, string messageContent)
    {
      SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");


      SqlCommand cmd = new SqlCommand("SAVE_MESSAGE", connection);
      cmd.CommandType = CommandType.StoredProcedure;

      cmd.Parameters.AddWithValue("@toUserID", toUserID);
      cmd.Parameters.AddWithValue("@fromUserID", fromUserID);
      cmd.Parameters.AddWithValue("@messageContent", messageContent);
      
      connection.Open();
      cmd.ExecuteNonQuery();
      connection.Close();
      return 0;
    }
    
    public static int ADMIN_SAVE_USERSTATS_INFO(string userStatsID, int adminGameID, int adminSeasonID, int userStatsGameCurrentScore, int userStatsScoreDiff)
    {
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("ADMIN_SAVE_USERSTATS_INFO", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@userID", userStatsID);
        cmd.Parameters.AddWithValue("@gameID", adminGameID);
        cmd.Parameters.AddWithValue("@seasonID", adminSeasonID);
        cmd.Parameters.AddWithValue("@score", userStatsGameCurrentScore); //goes to userStats
        cmd.Parameters.AddWithValue("@currentScoreDiff", userStatsScoreDiff); //goes to user
        cmd.Parameters.AddWithValue("@scoreDiff", userStatsScoreDiff);

        connection.Open();
        cmd.ExecuteNonQuery();
        connection.Close();
        return 0;
    }
    
    public static DataTable ADMIN_GET_USER_INFO()
    {
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("ADMIN_GET_USER_INFO", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet tmp = new DataSet();
        adapter.Fill(tmp);
        return tmp.Tables[0];
    }
    
    public static int ADMIN_SAVE_USER_INFO(string rankLeastUserID, int adminGameID, int adminSeasonID, int rankIndex)
    {
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("ADMIN_SAVE_USER_INFO", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@userID", rankLeastUserID); //used with both userStats and user
        cmd.Parameters.AddWithValue("@gameID", adminGameID); //used with userStats only
        cmd.Parameters.AddWithValue("@seasonID", adminSeasonID); //used with userStats only
        cmd.Parameters.AddWithValue("@currentRank", rankIndex); //goes to user
        cmd.Parameters.AddWithValue("@rank", rankIndex); //goes to userStats

        connection.Open();
        cmd.ExecuteNonQuery();
        connection.Close();
        return 0;
    }

    public static int ADMIN_ADD_GAME(int gameID, int seasonID, DateTime gameStartDateTime, int teamOneID, int teamTwoID, int scoreOne, int scoreTwo, string isOpen, string gameDesc, string gamelink)
    {
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("ADMIN_ADD_GAME", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@seasonID", seasonID);
        cmd.Parameters.AddWithValue("@gameID", gameID);
        cmd.Parameters.AddWithValue("@gameStartDateTime", gameStartDateTime);
        cmd.Parameters.AddWithValue("@teamOneID", teamOneID);
        cmd.Parameters.AddWithValue("@teamTwoID", teamTwoID);
        cmd.Parameters.AddWithValue("@scoreOne", scoreOne);
        cmd.Parameters.AddWithValue("@scoreTwo", scoreTwo);
        cmd.Parameters.AddWithValue("@isopen", isOpen);
        cmd.Parameters.AddWithValue("@gameDesc", gameDesc);
        cmd.Parameters.AddWithValue("@gameLink", gamelink);

        connection.Open();
        cmd.ExecuteNonQuery();
        connection.Close();
        return 0;
    }
    
    public static int ADMIN_ADD_TEAM(int teamID, string teamName, string teamAbbrev, int seasonWin, int seasonLoss, string teamLink)
    {
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("ADMIN_ADD_TEAM", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@teamID", teamID);
        cmd.Parameters.AddWithValue("@teamName", teamName);
        cmd.Parameters.AddWithValue("@teamAbbrev", teamAbbrev);
        cmd.Parameters.AddWithValue("@seasonWin", seasonWin);
        cmd.Parameters.AddWithValue("@seasonLoss", seasonLoss);
        cmd.Parameters.AddWithValue("@teamLink", teamLink);

        connection.Open();
        cmd.ExecuteNonQuery();
        connection.Close();
        return 0;
    }
    
    public static int ADMIN_ADD_USER(string userID, string password, int groupID, string role, int teamIDPref, string displayName, int currentScore, int currentRank, int spyPoints, string userLink, string avatarLink)
    {
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("ADMIN_ADD_USER", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@userID", userID);
        cmd.Parameters.AddWithValue("@password", password);
        cmd.Parameters.AddWithValue("@groupID", groupID);
        cmd.Parameters.AddWithValue("@role", role);
        cmd.Parameters.AddWithValue("@teamIDPref", teamIDPref);
        cmd.Parameters.AddWithValue("@displayName", displayName);
        cmd.Parameters.AddWithValue("@currentScore", currentScore);
        cmd.Parameters.AddWithValue("@currentRank", currentRank);
        cmd.Parameters.AddWithValue("@spyPoints", spyPoints);
        cmd.Parameters.AddWithValue("@userLink", userLink);
        cmd.Parameters.AddWithValue("@avatarLink", avatarLink);
        //cmd.Parameters.AddWithValue("@dateJoined", dateJoined); not used
        //cmd.Parameters.AddWithValue("@lastLogin", lastLogin); not used

        connection.Open();
        cmd.ExecuteNonQuery();
        connection.Close();
        return 0;
    }
    
    public static DataTable GET_ALL_TEAMS()
    {
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("GET_ALL_TEAMS", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet tmp = new DataSet();
        adapter.Fill(tmp);
        return tmp.Tables[0];
    }
    
    public static DataTable GET_ALL_GAMES(int seasonID)
    {
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("GET_ALL_GAMES", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@seasonID", seasonID); 

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet tmp = new DataSet();
        adapter.Fill(tmp);
        return tmp.Tables[0];
    }

    public static int NEW_SEASON(int seasonID)
    {
      SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
      

      SqlCommand cmd = new SqlCommand("NEW_SEASON", connection);
      cmd.CommandType = CommandType.StoredProcedure;

      cmd.Parameters.AddWithValue("@seasonID", seasonID);

      connection.Open();
      cmd.ExecuteNonQuery();
      connection.Close();
      return 0;
    }

    //temp delete user
    public static int TEMP_DELETE_SELF(string UserID)
    {
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("TEMP_DELETE_SELF", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@userID", UserID);

        connection.Open();
        cmd.ExecuteNonQuery();
        connection.Close();
        return 0;
        }

    //temp add admin
    public static int TEMP_GRANT_ADMIN()
    {
        SqlConnection connection = new SqlConnection("data source=LIB-UFDC-CACHE\\UFDCPROD;initial catalog=Football;integrated security=Yes;");
        

        SqlCommand cmd = new SqlCommand("TEMP_GRANT_ADMIN", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        connection.Open();
        cmd.ExecuteNonQuery();
        connection.Close();
        return 0;
    }

}