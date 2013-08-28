using System;
using System.Data;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data.SqlClient;

public partial class Dashboard : System.Web.UI.Page
{
    #region variables
    
    //2do: mk dynamic
    protected string header_page_name = "Dashboard"; 

    //used on page
    protected string logonTrimmed = String.Empty;
    protected string debug = String.Empty;
    protected string userMessage = String.Empty;
    protected string errMessage = String.Empty;
    protected string isRegistered = String.Empty;
    protected string isAdmin = String.Empty;
    protected int gamesOpen = 0;

    //user page vars
    protected string cUserID = String.Empty;
    protected string cPassword = String.Empty;
    protected string cGroupID = String.Empty;
    protected string cRole = String.Empty;
    protected string cTeamIDPref = String.Empty;
    protected string cDisplayName = String.Empty;
    protected string cCurrentSeason = String.Empty;
    protected string cCurrentScore = String.Empty;
    protected string cCurrentRank = String.Empty;
    protected string cSpyPoints = String.Empty;
    protected string cUserLink = String.Empty;
    protected string cAvatarLink = String.Empty;
    protected string cDateJoined = String.Empty;
    protected string cLastLogin = String.Empty;
    
    //2do: clean all this up...

    DataTable userStats;
    protected string stats_gameID = String.Empty;
    protected string guess = String.Empty;
    protected string score = String.Empty;
    protected string rank = String.Empty;
    protected string scoreDiff = String.Empty;

    DataTable game;
    protected string gameID = String.Empty;
    protected string gameStartDateTime = String.Empty;
    protected string gameStartTimestamp = String.Empty;
    protected string isOpen = String.Empty;
    protected string teamOneID = String.Empty;
    protected string teamTwoID = String.Empty;
    protected string scoreOne = String.Empty;
    protected string scoreTwo = String.Empty;
    protected string scoreDiffOneTwo = String.Empty;
    protected string scoreDiffTwoOne = String.Empty;

    DataTable gameboard;
    DataTable globalScoreboard;
    private DataTable user;
    private DataTable settings;

    //my global interater
    protected int i = 0;

    protected string highestUserID = String.Empty;
    protected int currentSeason = 0;

    #endregion


    //page load scripts
    protected void Page_Load(object sender, EventArgs e)
    {

      //to break it
      //DataTable games = FootballDb_Gateway.GET_ALL_GAMES(currentSeason);
      //int gameID333 = Convert.ToInt32(games.Rows[0]["gameID"].ToString());


        //validate user
        #region

        //get UFAD
        string logon = Page.User.Identity.Name;
        //TRIM it
        string logonTrimmed = logon.Remove(0, 5);
        //send/search db for user
        user = FootballDb_Gateway.SELECT_USER(logonTrimmed);
        //if user found... (validation)
        if ((user != null) && (user.Rows.Count > 0))
        {
            //is this the first time user has logged in?
            string dateJoined = user.Rows[0]["dateJoined"].ToString();
            if (string.IsNullOrEmpty(dateJoined))
            {
                userMessage = "This is your first time, please define a display name below.";
                //2do: make this easier to read/edit
                isRegistered = "document.getElementById('register').style.display = 'block';";
                //isRegistered = "<script type=\"text/javascript\">function isRegisterDisplay() { document.getElementById('register2').style.display = 'block'; }</script>";
            }
            else
            {
                //2do: should I do something here?
                isRegistered = "<!-- This is not your first time logging in. -->";
            }

            //lets get the user role
            string userRole = user.Rows[0]["role"].ToString();
            //are we the leader here?
            if (userRole.Contains("admin"))
            {
                //yes, 2do: make this easier to read/edit
                isAdmin = "document.getElementById('buttonsAdmin').style.display = 'block';";
            }
            else
            {
                //nope, should I do anyting here?
                isAdmin = "<!-- not Admin -->";
            }
        }
        //if there is no user
        else
        {
            //send to default page (IE auth)
            Response.Redirect("Default.aspx");
        }
        #endregion


        //get current season
        #region

        string applicationUserID = "0"; //define system user id
        settings = FootballDb_Gateway.SELECT_SETTINGS(applicationUserID);
        //assign current season 
        //reset local i
        i = 0;
        //validate to see if there is anything open...
        if ((settings != null) && (settings.Rows.Count > 0))
        {
          //yep there is, begin loooping through rows
          do
          {
            //get application ID
            if(settings.Rows[i]["userID"].ToString()==applicationUserID)
            {
              //get the gooods
              currentSeason = Convert.ToInt16(settings.Rows[i]["seasonID"].ToString());
              cCurrentSeason = Convert.ToString(currentSeason); 
            }
            //iterate to do it again!
            i++;
          } while (settings.Rows.Count != i);
        }
        else
        {
          userMessage = "Warning: There is no Current Season";
        }
        
        #endregion


        //check game statuses
        #region

        //run sql to see if there are any open
        game = FootballDb_Gateway.GAME_ISOPEN();
        //reset local i
        i = 0;
        //validate to see if there is anything open...
        if ((game != null) && (game.Rows.Count > 0))
        {
            //yep there is, begin loooping through rows
            do
            {
                //get the gooods
                gameStartDateTime = game.Rows[i]["gameStartDateTime"].ToString();
                gameID = game.Rows[i]["gameID"].ToString();
                //time (advanced var) conversion and definition here
                DateTime gametime = Convert.ToDateTime(gameStartDateTime);
                //has the game started?
                if (gametime >= DateTime.Now)
                {
                    //yep, or it is open.
                    isOpen = "true";
                    gamesOpen++;
                }
                else
                {
                    //nope
                    isOpen = "false";
                    //set game status to false
                    FootballDb_Gateway.CHANGE_GAME_ISOPEN(Convert.ToInt32(gameID), currentSeason);
                }
                //iterate to do it again!
                i++;
            } while (game.Rows.Count != i);
        }
        else
        {
            gamesOpen = 0;
        }

        #endregion
      

        //handling buttons when clicked
        #region

        if ( IsPostBack )
        {
            //grab the js hidden form thing
            string action = Request.Form["item_action"];
            //send it away!
            switch (action)
            {
                case "action1":
                    save_user_clicked();
                    //refreshes page so that the register box clears and change applies
                    Response.Redirect("Dashboard.aspx");
                    //2do: make a message saying thanks pop up.
                    break;
                case "action2":
                    admin_save_game_scores_clicked();
                    break;
                case "action3":
                    user_save_guess_clicked();
                    break;
                case "action4":
                    user_update_profile_clicked();
                    //this forces a reload to display the changes
                    Response.Redirect("Dashboard.aspx");
                    break;
                case "action5":
                    admin_add_game_clicked();
                    break;
                case "action6":
                    admin_add_team_clicked();
                    break;
                case "action7":
                    admin_add_user_clicked();
                    break;
                case "action8":
                    admin_new_season_clicked();
                    break;
                case "action9":
                    change_season_view();
                    break;
                case "action10":
                    //expansion...
                    break;
                case "action00":
                    TEMP_DELETE_SELF();
                    break;
                case "action000":
                    TEMP_GRANT_ADMIN();
                    break;
                //expansion...
            }
        }
        #endregion


        //assign page/user values
        #region

        if ((user != null) && (user.Rows.Count > 0))
        {

            //get the goodies
            cUserID = user.Rows[0]["userID"].ToString();
            cPassword = user.Rows[0]["password"].ToString();
            cGroupID = user.Rows[0]["groupID"].ToString();
            cRole = user.Rows[0]["role"].ToString();
            cTeamIDPref = user.Rows[0]["teamIDPref"].ToString();
            cDisplayName = user.Rows[0]["displayName"].ToString();
            cCurrentScore = user.Rows[0]["currentScore"].ToString();
            cCurrentRank = user.Rows[0]["currentRank"].ToString();
            cSpyPoints = user.Rows[0]["spyPoints"].ToString();
            cUserLink = user.Rows[0]["userLink"].ToString();
            cAvatarLink = user.Rows[0]["avatarLink"].ToString();
            cDateJoined = user.Rows[0]["dateJoined"].ToString();
            cLastLogin = user.Rows[0]["lastLogin"].ToString();
        }

        #endregion

    }

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void SaveItem(String sendData, String data2)
    {
      switch (sendData)
      {
        case "1":
          //currentSeason = Convert.ToInt16(data2);
          //Response.Redirect(Request.RawUrl,false);
          break;
      }
    }


    //...DROPDOWNS...

  protected void change_season_view()
  {
    currentSeason = Convert.ToInt16(Request.Form["viewSeasonID"]);
  }

    //create team conditional dropdown
    protected void create_team_conditional_dropdown()
    {
        string teamCDropdown = String.Empty;
        int LDTI = 0;
        DataTable teams = FootballDb_Gateway.GET_ALL_TEAMS();
        do
        {
            int teamID = Convert.ToInt32(teams.Rows[LDTI]["teamID"].ToString());
            string teamAbbrev = teams.Rows[LDTI]["teamAbbrev"].ToString();
            if (teamID == Convert.ToInt32(cTeamIDPref))
            {
                teamCDropdown += "   <option value=\"" + teamID + "\" selected=\"selected\">" + teamAbbrev + "</option><br />";
            }
            else
            {
                teamCDropdown += "   <option value=\"" + teamID + "\">" + teamAbbrev + "</option><br />";
            }
            LDTI++;
        } while (LDTI != teams.Rows.Count);
        Response.Write(teamCDropdown);
        return;
    }

    //create team1 dropdown
    protected void create_team1_dropdown()
    {
        string team1Dropdown = String.Empty;
        int LDTI = 0;
        DataTable teams = FootballDb_Gateway.GET_ALL_TEAMS();
        do
        {
            int teamID = Convert.ToInt32(teams.Rows[LDTI]["teamID"].ToString());
            string teamAbbrev = teams.Rows[LDTI]["teamAbbrev"].ToString();
            team1Dropdown += "   <option value=\"" + teamID + "\">" + teamAbbrev + "</option><br />";
            LDTI++;
        } while (LDTI != teams.Rows.Count);
        Response.Write(team1Dropdown);
        return;
    }

    //create team2 dropdown
    protected void create_team2_dropdown()
    {
        string team2Dropdown = String.Empty;
        int LDTI = 0;
        DataTable teams = FootballDb_Gateway.GET_ALL_TEAMS();
        do
        {
            int teamID = Convert.ToInt32(teams.Rows[LDTI]["teamID"].ToString());
            string teamAbbrev = teams.Rows[LDTI]["teamAbbrev"].ToString();
            if (LDTI == 1)
            {
                team2Dropdown += "   <option selected value=\"" + teamID + "\">" + teamAbbrev + "</option><br />";
            }
            else
            {
                team2Dropdown += "   <option value=\"" + teamID + "\">" + teamAbbrev + "</option><br />";
            }
            LDTI++;
        } while (LDTI != teams.Rows.Count);
        Response.Write(team2Dropdown);
        return;
    }

    //create game dropdown
    protected void create_game_dropdown()
    {
        string gameDropdown = String.Empty;
        int LDTI = 0;
        try
        {
          DataTable games = FootballDb_Gateway.GET_ALL_GAMES(currentSeason);
          do
          {
            int gameID = Convert.ToInt32(games.Rows[LDTI]["gameID"].ToString());
            string gameDesc = games.Rows[LDTI]["gameDesc"].ToString();
            try
            {
              DataTable specificUserStats = FootballDb_Gateway.SELECT_GUESS(cUserID, currentSeason, gameID);
              int userGuess = Convert.ToInt32(specificUserStats.Rows[0]["guess"].ToString());
              gameDropdown += "   <option onclick=\"getUserGuess(" + userGuess + ");\" value=\"" + gameID + "\" size=\"8\" >" + gameDesc + " (" + gameID + ") </option><br />";
            }
            catch (Exception)
            {
              gameDropdown += "   <option onclick=\"getUserGuess(0);\" value=\"" + gameID + "\" size=\"8\" >" + gameDesc + " (" + gameID + ") </option><br />";
            }
            
            LDTI++;
          } while (LDTI != games.Rows.Count);
        }
        catch (Exception)
        {
          gameDropdown += "   <option value=\"0\" size=\"8\" >No Games</option><br />";
        }
        
        Response.Write(gameDropdown);
        return;
    }

    //user dropdown
    protected void create_user_dropdown()
    {
        string usersDropdown = String.Empty;
        int LDTI = 0;
        DataTable users = FootballDb_Gateway.SELECT_USER();
        do
        {
            string userID = users.Rows[LDTI]["userID"].ToString();
            string displayName = users.Rows[LDTI]["displayName"].ToString();
            usersDropdown += "   <option value=\"" + userID +","+ displayName + "\" size=\"8\" >" + displayName + " (" + userID + ") </option><br />";
            LDTI++;
        } while (LDTI != users.Rows.Count);
        Response.Write(usersDropdown);
        return;
    }


    //...BUTTONS...

    //this is button action 1
    protected void save_user_clicked()
    {
        
        string regDisplayName = String.Empty;
        cUserID = user.Rows[0]["userID"].ToString();
        regDisplayName = Request.Form["regUserDisplayName"];
        FootballDb_Gateway.SAVE_USER_INFO(cUserID, regDisplayName);
        userMessage = "User Updated.";

    }
    
    //this is button action 2
    protected void admin_save_game_scores_clicked()
    {
      //get inputs
        //define input vars
        int adminGameID;
        int adminSeasonID;
        int GameScoreTeam1;
        int GameScoreTeam2;
        //verify that inputs are numeric  
        if (!Int32.TryParse(Request.Form["adminGameID"], out adminGameID))
        {
            userMessage = "Please enter a valid number.";
            return;
        }
        if (!Int32.TryParse(Request.Form["adminSeasonID"], out adminSeasonID))
        {
          userMessage = "Please enter a valid number.";
          return;
        }
        if (!Int32.TryParse(Request.Form["gameScoreTeam1"], out GameScoreTeam1))
        {
            userMessage = "Please enter a valid number.";
            return;
        }
        if (!Int32.TryParse(Request.Form["gameScoreTeam2"], out GameScoreTeam2))
        {
            userMessage = "Please enter a valid number.";
            return;
        }

        //calc diffs
        int GameScoreDiffT1T2 = GameScoreTeam1 - GameScoreTeam2; //calc score diff12
        int GameScoreDiffT2T1 = GameScoreTeam2 - GameScoreTeam1; //calc score diff21
        FootballDb_Gateway.ADMIN_SAVE_GAME_INFO(adminGameID, adminSeasonID, GameScoreTeam1, GameScoreTeam2, GameScoreDiffT1T2, GameScoreDiffT2T1); //send scores and diffs to DB userStats
      
        //determine indiviual scores
        DataTable adminUserStats = FootballDb_Gateway.ADMIN_GET_USERSTATS_INFO(adminGameID, currentSeason); //get guess and currentScore from DB userStats and currentScore from user 
        DataTable adminUser = FootballDb_Gateway.ADMIN_GET_USER_INFO(); //get current scores from DB user
        int maxUniqueGuessCount = adminUser.Rows.Count;  
        int totalUniqueGuessCount = adminUserStats.Rows.Count;
        string[] uniqueGuessUserIDs = new string[totalUniqueGuessCount];
        Boolean humanGuessFlag = false;

        //get unique guesses
        int worstScore = 0;
        int adminUserStatsDTI = 0; //dt iterater
        //define and parse results
        do
        {
          string userStatsID = adminUserStats.Rows[adminUserStatsDTI]["userID"].ToString(); //get user
          uniqueGuessUserIDs[adminUserStatsDTI] = userStatsID;
          int userStatsGuess = Convert.ToInt32(adminUserStats.Rows[adminUserStatsDTI]["guess"].ToString()); //get guess  
          int userStatsCurrentScore = Convert.ToInt32(adminUserStats.Rows[adminUserStatsDTI]["score"].ToString()); //get score
          int userStatsScoreDiff = Math.Abs(GameScoreDiffT1T2 - userStatsGuess); //calc user score diff
          int userStatsGameCurrentScore = userStatsCurrentScore + userStatsScoreDiff; //calc user score  
          FootballDb_Gateway.ADMIN_SAVE_USERSTATS_INFO(userStatsID, adminGameID, currentSeason, userStatsGameCurrentScore, userStatsScoreDiff); //send current score and score diff to DB userStats and user
          if (userStatsScoreDiff > worstScore)
          {
            worstScore = userStatsScoreDiff;
          }
          adminUserStatsDTI++; //iterate
        } while (adminUserStatsDTI != adminUserStats.Rows.Count); //continue parsing until the datatable is at it's end

        //assign guesses for people that did not vote
        if (totalUniqueGuessCount != maxUniqueGuessCount)
        {
          //for each registered user
          int it = 0;
          do
          {
            //get the userID
            string tempUserID1 = adminUser.Rows[it]["userID"].ToString();
            //check to see if they voted
            int it2 = 0;
            do
            {
              string tempUID = uniqueGuessUserIDs[it2];
              //going through each userID from the voted table
              if (tempUserID1 == tempUID)
              {
                //if match found, flag it as human
                //break it off
                humanGuessFlag = true;
                break;
              }
              else
              {
                //flag as nonhuman guess
                humanGuessFlag = false; 
              }
              it2++;
            } while (it2 < uniqueGuessUserIDs.Length);
            //no matching users found in guess id array

            //add a user guess of zero
            //FootballDb_Gateway.SAVE_GUESS(adminGameID, currentSeason, tempUserID1, 0);

            if (humanGuessFlag == false)
            {
              //make their guess a zero
              int userStatsGuess2 = 0; //get guess  
              //string userStatsUserID2 = adminUser.Rows[it]["userID"].ToString();
              int userStatsCurrentScore2 = Convert.ToInt32(adminUser.Rows[it]["currentScore"].ToString());
              int userStatsScoreDiff2 = Math.Abs(GameScoreDiffT1T2 - userStatsGuess2); //calc user score diff
              int userStatsGameCurrentScore2 = userStatsCurrentScore2 + userStatsScoreDiff2; //calc user score  
              FootballDb_Gateway.ADMIN_SAVE_USERSTATS_INFO(tempUserID1, adminGameID, currentSeason, userStatsGameCurrentScore2, userStatsScoreDiff2); //send current score and score diff to DB userStats and user  
            }
            
            ////change thieir score to the worst score
            //int tempUserCurrentScore = Convert.ToInt32(adminUser.Rows[it]["currentScore"].ToString()); //get score
            //int tempCurrentGameUserScore = tempUserCurrentScore + worstScore;
            //FootballDb_Gateway.ADMIN_SAVE_USERSTATS_INFO(tempUserID1, adminGameID, currentSeason, tempCurrentGameUserScore, worstScore); //send current score and score diff to DB userStats and user
            
            it++;
          } while (it < adminUser.Rows.Count);
        }

        //new ranking matrix
        DataTable rankings = FootballDb_Gateway.ADMIN_GET_USER_RANKS();
        string rankings_userID = String.Empty;
        int rankings_PreviousScore = -1;
        int rankings_ScoresRanked = 0;
        int rankings_MaxRanks = 11;
        int rankings_RankHolder = 0;
        do
        {
          //for each row
          for (int i = 0; i < rankings.Rows.Count; i++)
          {
            //add a rank number
            rankings_RankHolder++;

            //get userID
            rankings_userID = rankings.Rows[i]["userID"].ToString();
            
            //get current score
            int rankings_CurrentScore = Convert.ToInt32(rankings.Rows[i]["currentScore"].ToString());

            //is this the first rank?
            if (rankings_PreviousScore == -1)
            {
              //add another rank holder
              rankings_RankHolder++;
              //set prev to current
              rankings_PreviousScore = rankings_CurrentScore;
            }

            //is this the same score IE a duplicate rank?
            if (rankings_CurrentScore == rankings_PreviousScore)
            {
              //decrease in rank number
              rankings_RankHolder--;
            }

            //send the rank with params to the db to save
            FootballDb_Gateway.ADMIN_ADD_USER_RANK(rankings_userID, currentSeason, adminGameID, rankings_RankHolder);

            //iterate how many scores we have saved
            rankings_ScoresRanked++;

            //set prev to cur
            rankings_PreviousScore = rankings_CurrentScore;

          }
        } while (rankings_ScoresRanked < rankings_MaxRanks);


        #region old ranking method

        ////determine ranking
        //int startedOverallRanking = 0; //iterater to start ranking
        ////DataTable adminUser = FootballDb_Gateway.ADMIN_GET_USER_INFO(); //get current scores from DB user
        //int maxRankNumber = adminUser.Rows.Count; //get datatable row count and assign that to be the max number of ranks
        //int adminUserDTI = 0; //dt iterater
        //int leastValue = 0; //define initial least value
        //int rankIndex = 0; //rank index (IE user friendly ranking number)     
        //int prevLeastValue = -9999; //previous least value iterater/holder
        ////define and parse results
        //int leastUserIDContainsIT = 1; //used to tell how many least users ther are.
        //int special2 = maxRankNumber; //adminUser.Rows.Count;
        //int keeper1 = 0;
        //do
        //{

        //  //debug += "<br/>";

        //  string userID = adminUser.Rows[adminUserDTI]["userID"].ToString(); //get user
        //  int userCurrentScore = Convert.ToInt32(adminUser.Rows[adminUserDTI]["currentScore"].ToString()); //get and convert currentScore           
        //  int currentValue = userCurrentScore; //assign value
        //  string currentUserID = userID; //assign user
        //  string[] leastUserID = new string[maxRankNumber]; //define array to hold LUIDs and set it's max length to number of users (this is in case there are duplicates/ties)
        //  string rankLeastUserID = String.Empty;
        //  int leastUserIDIndex = 0; //define LUindex (to be used to determine number of duplicates/ties)
        //  int rankLeastUserIDIndex = 0; //ranking LUID index
        //  int loop3I = 0; //loop 3 iterater
        //  int leastUserIDRL = 0; //how many ppl are in leastUserDB
        //  int highestValue = 0;
        //  int duplicatesYes = 0;
        //  //for each row in user datatable 


        //  do
        //  {
        //    //get a row
        //    userID = adminUser.Rows[loop3I]["userID"].ToString(); //get user
        //    userCurrentScore = Convert.ToInt32(adminUser.Rows[loop3I]["currentScore"].ToString()); //get and convert currentScore
        //    currentValue = userCurrentScore; //assign value
        //    currentUserID = userID; //assign user
        //    int justdone = 0;
        //    //begin to compare

        //    //debug += "------------<br>";
        //    //debug += "compare: ";
        //    //debug += currentUserID;
        //    //debug += ", ";
        //    //debug += currentValue;
        //    //debug += ", ";
        //    //debug += leastValue;
        //    //debug += ", ";
        //    //debug += prevLeastValue;
        //    //debug += "<br>";
        //    //debug += "arguments: ";
        //    //debug += startedOverallRanking;
        //    //debug += ", ";
        //    //debug += justdone;
        //    //debug += ", ";
        //    //debug += leastUserIDContains;
        //    //debug += ", ";
        //    //debug += leastUserIDIndex;
        //    //debug += ", ";
        //    //debug += "<br>";
        //    //debug += "loops: ";

        //    if ((currentValue < leastValue) && (currentValue > prevLeastValue)) //no duplicates
        //    {
        //      leastValue = currentValue; //assign least value
        //      leastUserIDIndex = 1;
        //      duplicatesYes = 0;
        //      leastUserID[leastUserIDIndex] = currentUserID; //assign LUID
        //      justdone++; //to make sure the below if doesnt trigger
        //      //leastUserIDContainsIT = 1;
        //      //debug += "l1 ";
        //    }

        //    if (startedOverallRanking == 0) //should only happen once
        //    {
        //      leastValue = currentValue; //initialize
        //      //prevLeastValue = currentValue; //initialize
        //      leastUserID[leastUserIDIndex] = currentUserID; //assign LUID
        //      startedOverallRanking++;
        //      //leastUserIDContainsIT = 1;
        //      justdone++;
        //      //debug += "l3 ";
        //    }

        //    if ((currentValue == leastValue) && (justdone < 1) && (currentValue > prevLeastValue)) //there are duplicates
        //    {
        //      leastValue = currentValue; //assign least value (not technically needed)
        //      leastUserIDIndex++; //increment LUindex
        //      leastUserID[leastUserIDIndex] = currentUserID; //assign LUID
        //      //leastUserIDContainsIT++;
        //      duplicatesYes = 1;
        //      //debug += "l2 ";
        //    }


        //    //debug += "<br>";
        //    //debug += "checking: ";
        //    //debug += currentValue;
        //    //debug += ", ";
        //    //debug += leastValue;
        //    //debug += "<br>";
        //    //debug += "arguments: ";
        //    //debug += startedOverallRanking;
        //    //debug += ", ";
        //    //debug += justdone;
        //    //debug += ", ";
        //    //debug += leastUserIDContains;
        //    //debug += ", ";
        //    //debug += leastUserIDIndex;
        //    //debug += ", ";
        //    //debug += "<br>";

        //    //determine highest value
        //    if (currentValue > highestValue)
        //    {
        //      highestValue = currentValue + 1;
        //    }

        //    loop3I++; //iterate

        //  } while (loop3I < adminUser.Rows.Count); //until end of datatable
        //  //at the end of datatable

        //  prevLeastValue = leastValue; //iterate prevleastvalue
        //  leastValue = highestValue; //reset least value to highest value
        //  rankIndex++; //increment rank index
        //  rankLeastUserIDIndex = 0;
        //  //for each row if LUID

        //  //debug += "<br>";

        //  int TDLR = 0;
        //  if (duplicatesYes == 1)
        //  {
        //    do
        //    {

        //      rankLeastUserID = leastUserID[rankLeastUserIDIndex]; //assign LUID to each user in array

        //      if (rankLeastUserID == null)
        //      {
        //        //use alternative userID
        //        FootballDb_Gateway.ADMIN_SAVE_USER_INFO(userID, adminGameID, currentSeason, rankIndex); //send each LUID to DB user and userStats with rank  
        //      }
        //      else
        //      {
        //        FootballDb_Gateway.ADMIN_SAVE_USER_INFO(rankLeastUserID, adminGameID, currentSeason, rankIndex); //send each LUID to DB user and userStats with rank  
        //      }



        //      TDLR++; //times duplicate loop ran

        //      //debug += "sending: r";
        //      //debug += rankLeastUserID;
        //      //debug += ", ";
        //      //debug += rankIndex;
        //      //debug += "<br>";
        //      //debug += "values: p";
        //      //debug += prevLeastValue;
        //      //debug += ", l";
        //      //debug += leastValue;
        //      //debug += ", c";
        //      //debug += currentValue;
        //      //debug += ", h";
        //      //debug += highestValue;
        //      //debug += "<br>";
        //      //debug += "arguments: ";
        //      //debug += rankLeastUserIDIndex;
        //      //debug += ", ";
        //      //debug += leastUserIDIndex;
        //      //debug += ", ";
        //      //debug += leastUserIDContainsIT;
        //      //debug += "<br/>";

        //      rankLeastUserIDIndex++; //increment

        //    } while (rankLeastUserIDIndex < leastUserIDIndex); //continue until end of array with LUID
        //  }
        //  else
        //  {
        //    do
        //    {

        //      rankLeastUserID = leastUserID[rankLeastUserIDIndex]; //assign LUID to each user in array
        //      if (rankLeastUserID == null)
        //      {
        //        //use alternative userID
        //        FootballDb_Gateway.ADMIN_SAVE_USER_INFO(userID, adminGameID, currentSeason, rankIndex); //send each LUID to DB user and userStats with rank  
        //      }
        //      else
        //      {
        //        FootballDb_Gateway.ADMIN_SAVE_USER_INFO(rankLeastUserID, adminGameID, currentSeason, rankIndex); //send each LUID to DB user and userStats with rank  
        //      }
        //      TDLR = 1; //times duplicate loop ran

        //      //debug += "sending: r";
        //      //debug += rankLeastUserID;
        //      //debug += ", ";
        //      //debug += rankIndex;
        //      //debug += "<br>";
        //      //debug += "values: p";
        //      //debug += prevLeastValue;
        //      //debug += ", l";
        //      //debug += leastValue;
        //      //debug += ", c";
        //      //debug += currentValue;
        //      //debug += ", h";
        //      //debug += highestValue;
        //      //debug += "<br>";
        //      //debug += "arguments: r";
        //      //debug += rankLeastUserIDIndex;
        //      //debug += ", l";
        //      //debug += leastUserIDIndex;
        //      //debug += ", ";
        //      //debug += leastUserIDContainsIT;
        //      //debug += "<br/>";

        //      rankLeastUserIDIndex++; //increment

        //    } while (rankLeastUserIDIndex < leastUserIDIndex); //continue until end of array with LUID
        //  }

        //  //debug += "additional: s";
        //  //debug += special2;
        //  //debug += ", t";
        //  //debug += TDLR;
        //  //debug += ", ";
        //  //debug += leastUserIDContainsIT;
        //  //debug += ", ";

        //  //leastUserIDContainsIT--;
        //  ////special2 -= leastUserIDContainsIT;
        //  ////special2 -= TDLR;
        //  ////special2 --;
        //  //leastUserIDContainsIT = 1;

        //  keeper1 += TDLR;

        //  //debug += special2;
        //  //debug += ", k";
        //  //debug += keeper1;
        //  //debug += "<br>";

        //  adminUserDTI++; //iterate
        //} while (keeper1 < special2); //continue until end of datatable

        #endregion
      
    }

    //this is button action 3
    protected void user_save_guess_clicked()
    {
        //define vars
        int userGameID;
        int userGuess;
        //validate
        if ( !Int32.TryParse(Request.Form["guess"], out userGuess ))
        {
            userMessage = "Please enter a valid guess.";
            return;
        }
        if (!Int32.TryParse(Request.Form["gameID"], out userGameID))
        {
            userMessage = "Game ID is invalid";
            return;
        }
        //2do: make this better? perhaps an if statement grabbing all games and comparing or just do it in sql
        //2do: why is this a try?
        //begin validation to save only if game is open
            game = FootballDb_Gateway.GAME_ISOPEN();
            int gamesOpenCount = 0;
            if (game.Rows.Count > 0)
            {
              do
              {
                int canSaveGameID = Convert.ToInt32(game.Rows[gamesOpenCount]["gameID"].ToString());
                if (canSaveGameID == userGameID)
                {
                  FootballDb_Gateway.SAVE_GUESS(userGameID, currentSeason, user.Rows[0]["userID"].ToString(), userGuess);
                  userMessage = "Guess updated.";
                }
                gamesOpenCount++;
              } while (gamesOpenCount < game.Rows.Count);

            }      
            else
            {
                userMessage = "Game is not open.";
            }
    }

    //this is button action 4
    protected void user_update_profile_clicked()
    {
        //get stuff
        string a4userID = Request.Form["a4userID"]; //readonly
      string a4password = "password"; //readonly
        //string a4password = Request.Form["a4password"];
        int a4groupID = Convert.ToInt32(Request.Form["a4groupID"]); //readonly
        string a4role = Request.Form["a4role"]; //readonly
        int a4teamIDPref = Convert.ToInt32(Request.Form["a4teamIDPref"]);
        string a4displayName = Request.Form["a4displayName"];
        int a4currentScore = Convert.ToInt32(Request.Form["a4currentScore"]); //readonly
        int a4currentRank = Convert.ToInt32(Request.Form["a4currentRank"]); //readonly
        int a4spyPoints = 0; //temp
        //int a4spyPoints = Convert.ToInt32(Request.Form["a4spyPoints"]); //readonly
        //DateTime a4dateJoined = Convert.ToDateTime(Request.Form["a4dateJoined"]); //readonly, not used
        //DateTime a4lastLogin = Convert.ToDateTime(Request.Form["a4lastLogin"]); //readonly, not used
        string a4userLink = Request.Form["a4userLink"];
        string a4avatarLink = Request.Form["a4avatarLink"];
        //validate
        if (!Int32.TryParse(Request.Form["a4groupID"], out a4currentScore))
        {
            userMessage = "Group ID is invalid.";
            return;
        }
        if (!Int32.TryParse(Request.Form["a4currentScore"], out a4currentScore))
        {
            userMessage = "Current score is invalid.";
            return;
        }
        if (!Int32.TryParse(Request.Form["a4currentRank"], out a4currentRank))
        {
            userMessage = "Current rank is invalid";
            return;
        }
        if (!Int32.TryParse(Request.Form["a4teamIDPref"], out a4teamIDPref))
        {
            userMessage = "Team preference ID is invalid";
            return;
        }
        //if (!Int32.TryParse(Request.Form["a4spyPoints"], out a4spyPoints))
        //{
        //    userMessage = "Spy points are invalid.";
        //    return;
        //}
        //send data to db user
        FootballDb_Gateway.ADMIN_ADD_USER(a4userID, a4password, a4groupID, a4role, a4teamIDPref, a4displayName, a4currentScore, a4currentRank, a4spyPoints, a4userLink, a4avatarLink);
        userMessage = "User Profile Updated.";
    }

    //this is button action 5
    protected void admin_add_game_clicked()
    {

      //define goodies
      int a5gameID;
      int a5seasonID;
      string a5gameStartDate;
      string a5gameStartTime;
      DateTime a5gameStartDateTime;
      int a5teamOneID;
      int a5teamTwoID;
      int a5scoreOne;
      int a5scoreTwo;
      string a5isOpen;
      string a5gameDesc;
      string a5gameLink;

        //validate...
        if (!Int32.TryParse(Request.Form["a5gameID"], out a5gameID))
        {
            userMessage = "Game ID is invalid.";
            return;
        }
        if (!Int32.TryParse(Request.Form["a5seasonID"], out a5seasonID))
        {
          userMessage = "Game ID is invalid.";
          return;
        }
        if (!Int32.TryParse(Request.Form["a5teamOneID"], out a5teamOneID))
        {
            userMessage = "Team ID 1 is invalid";
            return;
        }
        if (!Int32.TryParse(Request.Form["a5teamTwoID"], out a5teamTwoID))
        {
            userMessage = "Team ID 2 is invalid.";
            return;
        }
        if (!Int32.TryParse(Request.Form["a5scoreOne"], out a5scoreOne))
        {
            userMessage = "Score One is invalid";
            return;
        }
        if (!Int32.TryParse(Request.Form["a5scoreTwo"], out a5scoreTwo))
        {
            userMessage = "Score Two is invalid.";
            return;
        }

        //get the goddies (and convert where necessary)
        a5gameID = Convert.ToInt32(Request.Form["a5gameID"]);
        a5seasonID = Convert.ToInt32(Request.Form["a5seasonID"]);
        a5gameStartDate = Request.Form["a5gameStartDate"];
        a5gameStartTime = Request.Form["a5gameStartTime"];
        DateTime.TryParse(a5gameStartDate + " " + a5gameStartTime, out a5gameStartDateTime);
        a5teamOneID = Convert.ToInt32(Request.Form["a5teamOneID"]);
        a5teamTwoID = Convert.ToInt32(Request.Form["a5teamTwoID"]);
        a5scoreOne = Convert.ToInt32(Request.Form["a5scoreOne"]);
        a5scoreTwo = Convert.ToInt32(Request.Form["a5scoreTwo"]);
        a5isOpen = Request.Form["a5isOpen"];
        a5gameDesc = Request.Form["a5gameDesc"];
        a5gameLink = Request.Form["a5gameLink"];

        //send to game db
        FootballDb_Gateway.ADMIN_ADD_GAME(a5gameID, a5seasonID, a5gameStartDateTime, a5teamOneID, a5teamTwoID, a5scoreOne, a5scoreTwo, a5isOpen, a5gameDesc, a5gameLink);
        userMessage = "Games Updated.";
    }

    //this is button action 6
    protected void admin_add_team_clicked()
    {
        //get the goddies (and convert where necessary)
        int a6teamID = Convert.ToInt32(Request.Form["a6teamID"]);
        string a6teamName = Request.Form["a6teamName"];
        string a6teamAbbrev = Request.Form["a6teamAbbrev"];
        int a6seasonWin = Convert.ToInt32(Request.Form["a6seasonWin"]);
        int a6seasonLoss = Convert.ToInt32(Request.Form["a6seasonLoss"]);
        string a6teamLink = Request.Form["a6teamLink"];

        //validate...
        if (!Int32.TryParse(Request.Form["a6teamID"], out a6teamID))
        {
            userMessage = "Team ID is invalid.";
            return;
        }
        if (!Int32.TryParse(Request.Form["a6seasonWin"], out a6seasonWin))
        {
            userMessage = "Season Wins are invalid";
            return;
        }
        if (!Int32.TryParse(Request.Form["a6seasonLoss"], out a6seasonLoss))
        {
            userMessage = "Season Losses are invalid.";
            return;
        }

        //send to game db
        FootballDb_Gateway.ADMIN_ADD_TEAM(a6teamID, a6teamName, a6teamAbbrev, a6seasonWin, a6seasonLoss, a6teamLink);
        userMessage = "Team Updated.";
    }

    //this is button action 7
    protected void admin_add_user_clicked()
    {
        //get the goddies (and convert where necessary)
        string a7userID = Request.Form["a7userID"];
        string a7password = Request.Form["a7password"];
        int a7groupID = Convert.ToInt32(Request.Form["a7groupID"]);
        string a7role = Request.Form["a7role"];
        string a7displayName = Request.Form["a7displayName"];
        int a7currentScore = Convert.ToInt32(Request.Form["a7currentScore"]);
        int a7currentRank = Convert.ToInt32(Request.Form["a7currentRank"]);
        int a7teamIDPref = Convert.ToInt32(Request.Form["a7teamIDPref"]);
        int a7spyPoints = Convert.ToInt32(Request.Form["a7spyPoints"]);
        string a7userLink = Request.Form["a7userLink"];
        string a7avatarLink = Request.Form["a7avatarLink"];
        //DateTime a7dateJoined; //null them, not used
        //DateTime a7lastLogin; //null them, not used
        
        //validate...
        if (!Int32.TryParse(Request.Form["a7groupID"], out a7groupID))
        {
            userMessage = "Group ID is invalid.";
            return;
        }
        if (!Int32.TryParse(Request.Form["a7currentScore"], out a7currentScore))
        {
            userMessage = "Current score is invalid.";
            return;
        }
        if (!Int32.TryParse(Request.Form["a7currentRank"], out a7currentRank))
        {
            userMessage = "Current rank is invalid";
            return;
        }
        if (!Int32.TryParse(Request.Form["a7teamIDPref"], out a7teamIDPref))
        {
            userMessage = "Team preference ID is invalid";
            return;
        }
        if (!Int32.TryParse(Request.Form["a7spyPoints"], out a7spyPoints))
        {
            userMessage = "Spy points are invalid.";
            return;
        }

        //send to game db
        FootballDb_Gateway.ADMIN_ADD_USER(a7userID, a7password, a7groupID, a7role, a7teamIDPref, a7displayName, a7currentScore, a7currentRank, a7spyPoints, a7userLink, a7avatarLink);
        userMessage = "User Updated.";
    }

    //this is action 8
    protected void admin_new_season_clicked()
    {
    int tempSeason;

    if (!Int32.TryParse(Request.Form["a8seasonID"], out tempSeason))
    {
            userMessage = "Season ID is invalid.";
            return;
        }

    tempSeason = Convert.ToInt32(Request.Form["a8seasonID"]);

    if(currentSeason!=tempSeason)
    {
      currentSeason = tempSeason;
      FootballDb_Gateway.NEW_SEASON(currentSeason);
      Response.Redirect(Request.RawUrl,false);
      userMessage = "Season Changed";
    }
    
  }

    //this is button action 00
    protected void TEMP_DELETE_SELF()
    {

        cUserID = user.Rows[0]["userID"].ToString();
        FootballDb_Gateway.TEMP_DELETE_SELF(cUserID);
        userMessage = "User Deleted.";
        Response.Redirect("Dashboard.aspx");
    }

    //this is button action 000
    protected void TEMP_GRANT_ADMIN()
    {
        FootballDb_Gateway.TEMP_GRANT_ADMIN();
        userMessage = "User Role Updated.";
        Response.Redirect("Dashboard.aspx");
    }


    //...REPORTS...

    //personal scoreboard
    public void write_scoreboard()
    {
        //define local table
        userStats = FootballDb_Gateway.SELECT_USER_STATS(cUserID, currentSeason);
        //reset local i
        i = 0;
        //validate if tables present
        if ((userStats != null) && (userStats.Rows.Count > 0))
        {
            //start and add table headers
            StringBuilder resultString = new StringBuilder();
            resultString.AppendLine("<table style=\"width:100%;padding:2px;text-align:center;\">");
            resultString.AppendLine("   <tr>");
            resultString.AppendLine("       <td style=\"background:#DB9370\">Game</td>");
            resultString.AppendLine("       <td style=\"background:#DB9370\">Guess</td>");
            resultString.AppendLine("       <td style=\"background:#DB9370\">+/-</td>");
            resultString.AppendLine("       <td style=\"background:#DB9370\">Score</td>");
            resultString.AppendLine("       <td style=\"background:#DB9370\">Rank</td>");
            resultString.AppendLine("   <tr>");
            //begin looping through each row
            do
            {
                //get/assign tanbe elements
                userStats = FootballDb_Gateway.SELECT_USER_STATS(cUserID, currentSeason);
                //if user found...
                DataTable teamGameIDs = FootballDb_Gateway.SELECT_GAMES(currentSeason);
                for (int it = 0; it < teamGameIDs.Rows.Count; it++)
                {
                  if (teamGameIDs.Rows[i]["gameID"].ToString() == userStats.Rows[i]["gameID"].ToString())
                  {
                    stats_gameID = teamGameIDs.Rows[i]["gameDesc"].ToString();  
                  }
                }
                
                //stats_gameID = userStats.Rows[i]["gameID"].ToString();
                guess = userStats.Rows[i]["guess"].ToString();
                score = userStats.Rows[i]["score"].ToString();
                rank = userStats.Rows[i]["rank"].ToString();
                scoreDiff = userStats.Rows[i]["scoreDiff"].ToString();
                //add table elements
                resultString.AppendLine("   <tr>");
                resultString.AppendLine("       <td>" + stats_gameID + "</td>");
                resultString.AppendLine("       <td>" + guess + "</td>");
                resultString.AppendLine("       <td>" + scoreDiff + "</td>");
                resultString.AppendLine("       <td>" + score + "</td>");
                resultString.AppendLine("       <td>" + rank + "</td>");
                resultString.AppendLine("   </tr>");
                //iterate
                i++;
            } while (userStats.Rows.Count != i);
            //end and write table
            resultString.AppendLine("</table>");
            string scoreboard_string = string.Empty;
            scoreboard_string = resultString.ToString();
            Response.Output.Write(scoreboard_string);
        }
        else
        {
            Response.Write("<h4>No Report To Display</h4>");
        }
    }

    //gameboard
    public void write_gameboard()
    {
        //define local table
        gameboard = FootballDb_Gateway.SELECT_GAMES(currentSeason);
        //reset local i
        i = 0;
        //validate
        if ((gameboard != null) && (gameboard.Rows.Count > 0))
        {
            //start and add table headers
            StringBuilder resultString = new StringBuilder();
            resultString.AppendLine("<table style=\"width:100%;padding:2px;text-align:center;\">");
            resultString.AppendLine("   <tr>");
            resultString.AppendLine("       <td style=\"background:#DB9370\">Game Desc</td>");
            //resultString.AppendLine("       <td style=\"background:#DB9370\">Description</td>");
            resultString.AppendLine("       <td style=\"background:#DB9370\">Game Scores</td>");  
            resultString.AppendLine("       <td style=\"background:#DB9370\">Kickoff</td>");           
            resultString.AppendLine("       <td style=\"background:#DB9370\">Voting</td>");
            resultString.AppendLine("   <tr>");
            do
            {
                //assign table elements
                //string gameboardGameID = gameboard.Rows[i]["gameID"].ToString();
                string gameboardGameStartDateTime = gameboard.Rows[i]["gameStartDateTime"].ToString();
                //string gameboardTeamOneID = gameboard.Rows[i]["teamOneID"].ToString();
                //string gameboardTeamTwoID = gameboard.Rows[i]["teamTwoID"].ToString();
                string gameboardGameDesc = gameboard.Rows[i]["gameDesc"].ToString();
                string gameboardScoreOne = gameboard.Rows[i]["scoreOne"].ToString();
                string gameboardScoreTwo = gameboard.Rows[i]["scoreTwo"].ToString();
                string gameboardIsOpen = gameboard.Rows[i]["isOpen"].ToString();
                string gameboardIsOpenTranslated = string.Empty;
                //expansion...
                //string gameboardGameDesc = gameboard.Rows[i]["gameDesc"].ToString();
                //string gameboardGameLink = gameboard.Rows[i]["GameLink"].ToString();
                //translate
                if(gameboardIsOpen=="false")
                {
                    gameboardIsOpenTranslated = "Closed";
                }
                else
                {
                    gameboardIsOpenTranslated = "Open";
                }
                //add table elements
                resultString.AppendLine("   <tr>");
                //resultString.AppendLine("       <td>" + gameboardGameID + "</td>");
                resultString.AppendLine("       <td>" + gameboardGameDesc + "</td>");  
                //resultString.AppendLine("       <td>" + gameboardTeamOneID + " vs " + gameboardTeamTwoID + "</td>");
                resultString.AppendLine("       <td>" + gameboardScoreOne + " / " + gameboardScoreTwo + "</td>");
                resultString.AppendLine("       <td width='15%'>" + gameboardGameStartDateTime + "</td>");
                resultString.AppendLine("       <td>" + gameboardIsOpenTranslated + "</td>");
                resultString.AppendLine("   </tr>");
                //iterate
                i++;
            } while (gameboard.Rows.Count != i);
            //add table close
            resultString.AppendLine("</table>");
            //write table to string
            string gameboard_string = resultString.ToString();
            Response.Output.Write(gameboard_string);
        }
        else
        {
            Response.Write("<h4>No Report To Display</h4>");
        }
    }

    //global scoreboard
    public void write_global_scoreboard()
    {
        //define local table
        globalScoreboard = FootballDb_Gateway.SELECT_STATS();
        //reset local i
        i = 0;
        //validate
        if ((globalScoreboard != null) && (globalScoreboard.Rows.Count > 0))
        {
            //start and add table headers
            StringBuilder resultString = new StringBuilder();
            resultString.AppendLine("<table style=\"max-height:90%;width:100%;padding:2px;text-align:center;\" class=\"sortable\">");
            resultString.AppendLine("   <tr>");
            resultString.AppendLine("       <td style=\"background:#DB9370\">Current Rank</td>");
            resultString.AppendLine("       <td style=\"background:#DB9370\">Current Score</td>");
            resultString.AppendLine("       <td style=\"background:#DB9370\">Display Name</td>");
            //resultString.AppendLine("       <td style=\"background:#DB9370\">Last Login</td>");
            resultString.AppendLine("   <tr>");
            do
            {
                //assign table elements
                string globalboardDisplayName = globalScoreboard.Rows[i]["displayName"].ToString();
                string globalLastLogin = globalScoreboard.Rows[i]["lastLogin"].ToString();
                string globalboardCurrentScore = globalScoreboard.Rows[i]["currentScore"].ToString();
                string globalboardCurrentRank = globalScoreboard.Rows[i]["currentRank"].ToString();
                //expansion...
                //string globalboardUserLink = globalboard.Rows[i]["userLink"].ToString();
                //string globalboardAvatarLink = globalboard.Rows[i]["avatarLink"].ToString();
                //translate
                string globalLastLoginTranslated = String.Empty;
                if (globalLastLogin == "")
                {
                    globalLastLoginTranslated = "Never";
                }
                else
                {
                    globalLastLoginTranslated = globalLastLogin;
                }
              
                //add table elements
                resultString.AppendLine("   <tr>");
                resultString.AppendLine("       <td>" + globalboardCurrentRank + "</td>");
                resultString.AppendLine("       <td>" + globalboardCurrentScore + "</td>");
                resultString.AppendLine("       <td>" + globalboardDisplayName + "</td>");
                //resultString.AppendLine("       <td width='25%'>" + globalLastLoginTranslated + "</td>");
                resultString.AppendLine("   </tr>");
                //iterate
                i++;
            } while (globalScoreboard.Rows.Count != i);
            //add table close
            resultString.AppendLine("</table>");
            //write table to string
            string globalScoreBoard_string = resultString.ToString();
            Response.Output.Write(globalScoreBoard_string);
        }
        else
        {
            Response.Write("<h4>No Report To Display</h4>");
        }
    }

    //global guessboard
    public void write_guessboard()
    {
      //get all guesses
      DataTable guessboard = FootballDb_Gateway.Get_GuessBoard(currentSeason);
      int userCount = guessboard.Columns.Count + 1;
      //reset local i
      i = 0;
      //validate
      if ((guessboard != null) && (guessboard.Rows.Count > 0))
      {
        //start
        StringBuilder resultString = new StringBuilder();
        resultString.AppendLine("<table style=\"font-size:10px;max-height:90%;width:100%;padding:2px;text-align:center;\">");
        
        int currentRowCount = 0;
        do //for each row
        {
          
          //if it is the header row
          if (currentRowCount == 0)
          {
            //add the header
            resultString.AppendLine("   <tr>");
            int temp2 = 0;
            do
            {
              resultString.AppendLine("       <td style=\"background:#DB9370;max-width:35px;\">" + guessboard.Columns[temp2].ColumnName + "</td>");
              //resultString.AppendLine("       <td style=\"background:#DB9370;width:30px\">" + guessboard.Rows[0][temp2] + "</td>");
              temp2++;
            } while (temp2 < guessboard.Columns.Count);
            resultString.AppendLine("   </tr>");
          }
          
          //if this is the second to last row
          if (currentRowCount == (guessboard.Rows.Count - 1))
          {
              resultString.AppendLine("   <tr><td colspan=\""+guessboard.Columns.Count+"\"><hr/></td></tr>");  
          }

          resultString.AppendLine("   <tr>");
          int currentColumnCount = 0;
          do //for each column
          {
            resultString.Append("   <td>");
            resultString.Append(guessboard.Rows[currentRowCount][currentColumnCount].ToString());
            resultString.AppendLine("</td>");
            currentColumnCount++;
          } while (currentColumnCount < guessboard.Columns.Count);
          resultString.AppendLine("   </tr>");
          currentRowCount++;
        } while (currentRowCount < guessboard.Rows.Count); 

        //add table close
        resultString.AppendLine("</table>");
        
        //write table to string
        string guessboard_string = resultString.ToString();
        Response.Output.Write(guessboard_string);
      }
      else
      {
        Response.Write("<h4>No Report To Display</h4>");
      }
    }

    //...OTHER...

    //personal scoreboard
    public void write_messageboard()
    {
      //define local table
      DataTable userMessages = FootballDb_Gateway.SELECT_USER_MESSAGES(cUserID);
      //reset local i
      i = 0;
      //validate if tables present
      if ((userMessages != null) && (userMessages.Rows.Count > 0))
      {
        //start and add table headers
        StringBuilder resultString = new StringBuilder();
        resultString.AppendLine(" <ul> ");
        //begin looping through each row
        do
        {
          string fromUserID = userMessages.Rows[i]["fromUserID"].ToString();
          string messageContent = userMessages.Rows[i]["messageContent"].ToString();
          string messageFlag = userMessages.Rows[i]["messageFlag"].ToString();
          if (messageFlag == "active")
          {
            resultString.AppendLine("   <li>" + fromUserID + " Says: \"" + messageContent + "\"</li>");
          }
          //iterate
          i++;
        } while (userMessages.Rows.Count != i);
        //end and write table
        resultString.AppendLine("</ul>");
        string messageboard_string = string.Empty;
        messageboard_string = resultString.ToString();
        Response.Output.Write(messageboard_string);
      }
      else
      {
        Response.Write("<ul><li>No Messages To Display</li></ul>");
      }
    }
  

    //...DEBUGGER...

    //2do: add debug writer here
    //2do: have debugger write out the ammount of time to load page
}