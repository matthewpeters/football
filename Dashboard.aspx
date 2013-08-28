<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="title" ContentPlaceHolderID="page_title" Runat="Server">
    UF Training App | Football | Dashboard
</asp:Content>

<asp:Content ID="scripts" ContentPlaceHolderID="page_scripts" Runat="Server">
    <script type="text/javascript">
        function starting() {
            <%=isRegistered %>
          <%=isAdmin %>
          var tempMessage = "<%=userMessage%>";
          if (tempMessage != "") {
            displayMessage(tempMessage);
          }
        }
    </script>
    <script type="text/javascript"> 
      var datefield = document.createElement("input");
        datefield.setAttribute("type", "date");
        if (datefield.type != "date") { //if browser doesn't support input type="date", load files for jQuery UI Date Picker
            document.write('<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />\n');
            document.write('<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"><\/script>\n');
            document.write('<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"><\/script>\n');
        }
    </script>
    <script type="text/javascript">
        if (datefield.type != "date") { //if browser doesn't support input type="date", initialize date picker widget:
            jQuery(function ($) { $('#a5gameStartDate').datepicker(); }); //on document.ready
        }
    </script>
    <style type="text/css">
    @import "timepicker/jquery.timeentry.css";
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js"></script>
    <script type="text/javascript" src="timepicker/jquery.timeentry.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#defaultEntry').timeEntry({ spinnerImage: 'timepicker/spinnerDefault.png' });
        });
    </script>
    <script type="text/javascript">
        function selectAll(id) {
            document.getElementById(id).focus();
            document.getElementById(id).select();
        }
    </script>
    <script type="text/javascript" src="ibox/ibox.js"></script>
    <script type="text/javascript">
        iBox.padding = 50;
        iBox.inherit_frames = false;
    </script>
    </asp:Content>

<asp:Content ID="header_info1" ContentPlaceHolderID="header_info" Runat="Server">
    <div id="last_login">
        Last login: <%=cLastLogin %>
    </div>
    <div id="welcome">
        Welcome, <%=cDisplayName %>
    </div>
    <div id="quick_stats">
        Season: <%=cCurrentSeason %>, Rank: <%=cCurrentRank %>, Score: <%=cCurrentScore %>
    </div>
</asp:Content>

<asp:Content runat="server" ID="header_info2" ContentPlaceHolderID="page_name_info">
    <div id="header_title">
        <%=cDisplayName %>'s <%=header_page_name %>
    </div>
    <div id="header_subtext">
        You are number <%=cCurrentRank %> with <%=cCurrentScore %> points!
    </div>
</asp:Content>

<asp:Content runat="server" ID="user_message" ContentPlaceHolderID="user_message">
    <%--<div id="userStatusArea">
      <h3>Current Guess: </h3>
      
    </div>--%>
</asp:Content>

<asp:Content ID="page_content" ContentPlaceHolderID="page_content" Runat="Server">
    
    <input type="hidden" id="item_action" name="item_action" value="" />

    <div id="registerContainer">
        <div id="register" style="text-align:right;padding-top:5px;font-size:0.875em;font-family:'Courier New', Courier, monospace;display:none;text-align:center;background:url('img/folder1_325x200.png')no-repeat;background-size:325px 200px;background-position:center;">
            <div style="margin-left:-25%;">
                <h3><a>Registration</a></h3>
            </div>
            <table style="text-align:right;padding:0;">
                <tr>
                    <td>Choose A Display Name:</td>
                    <td><input id="regUserDisplayName" name="regUserDisplayName" type="text" value="<%= cDisplayName %>" size="15" style="background:antiquewhite;" onclick="selectAll('regUserDisplayName');" style="font-size:1em;font-family:'Courier New', Courier, monospace;"/></td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Your Display Name appears for all other users."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                <tr>
                    <td>Your Team Preference:</td>
                    <td><input id="regUserTeamPrefID" name="regUserTeamPrefID" type="text" value="<%=cTeamIDPref %>" size="15" onclick="selectAll('regUserTeamPrefID');" readonly="readonly" style="background-color:#a9a9a9;font-size:1em;font-family:'Courier New', Courier, monospace;"/></td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Your Team Preference is the team you are rooting for (currently you cannot change this, but who would)."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                <tr>
                    <td>Your Role:</td>
                    <td><input id="regUserRole" name="regUserRole" type="text" value="<%=cRole %>" size="15" onclick="selectAll('regUserRole');" readonly="readonly" style="background-color:#a9a9a9;font-size:1em;font-family:'Courier New', Courier, monospace;"/></td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Your role is used to determine what privileges you have."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="text-align:right;"><input id="regUser" name="action1" type="submit" value="Register" class="button2a" onclick="press(1);" /></td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <p style="clear:both;"></p>
        </div>
    </div>
    
    <div id="anonouncementsContainer">
        <h4><a href="#" onclick="showHide('vis1');">Messages</a></h4>
        <div id="vis1" style="display:block;">
          <%write_messageboard(); %>
            <%--<ul>
                <li>Mobile version will launch soon!</li>
                <li>Please email me with feedback and suggestions! (matthew.peters@ufl.edu)!</li>
            </ul>--%>
        </div>
        <hr/>
        <p>&nbsp;</p>
    </div>
  
  <div id="navButtonContainer">
    <div class="navButtonGroup">
      <div class="navButton"></div>
      <div class="navButton"></div>
      <div class="navButton"></div>
      <div class="navButton"></div>
      <div class="navButton"></div>
    </div>
    <div class="navButtonGroup">
      <div class="navButton"></div>
      <div class="navButton"></div>
    </div>
    <div class="navButtonGroup">
      <div class="navButton"></div>
      <div class="navButton"></div>
      <div class="navButton"></div>
    </div>
    
  </div>
    
    <div style="clear:both;"></div>

    <div id="buttonsContainer" style="float:left;width:45%">
        <div id="buttonsAdmin" style="display:none;">
            <a href="#"><h4>Admin</h4></a>
            <a onclick="showHide2('adminNew');"><img src="img/buttons2/button-admin-new.png" title="Seasons Admin"/></a>
            &nbsp;&nbsp;&nbsp;
            <a onclick="showHide2('adminScore');"><img src="img/buttons2/button-admin-scores.png" title="Scores Admin"/></a>
            &nbsp;&nbsp;&nbsp;
            <a onclick="showHide2('adminGame');"><img src="img/buttons2/button-admin-games.png" title="Games Admin"/></a>
            &nbsp;&nbsp;&nbsp;
            <a onclick="showHide2('adminTeam');"><img src="img/buttons2/button-admin-teams.png" title="Teams Admin"/></a>
            &nbsp;&nbsp;&nbsp;
            <a onclick="showHide2('adminUser');"><img src="img/buttons2/button-admin-users.png" title="Users Admin"/></a>
            <p>&nbsp;</p>
        </div>
        <div id="buttonsUser">
            <a href="#"><h4>Actions</h4></a>
            <ul>  
              <li><a onclick="showHide2('userGuess');">Make a Guess</a></li>
              <li><a onclick="showHide2('userProfile');">Manage Profile</a></li>
              <%--<li><a onclick="showHide2('userSpy');">Manage Spy Points</a></li>--%>
              <%--<li><a onclick="showHide2('userMessage');">Manage Messages</a></li>--%>
            </ul>
        </div>
      <p>&nbsp;</p>
        <div id="buttonsReports">
            <a href="#"><h4>Reports</h4></a>
            <ul>  
              <li><a onclick="showHide2('report1');">Personal Scoreboard</a></li>
              <li><a onclick="showHide2('report2');">Game Board</a></li>
              <li><a onclick="showHide2('report3');">Global Scoreboard</a></li>
            </ul>
            <p>&nbsp;</p>
        </div>
      <%--<div id="buttonsUser">
            <a href="#"><h4>User Actions</h4></a>
            <a onclick="showHide2('userProfile');"><img src="img/buttons2/button-user-profile.png" title="User Profile"/></a>
            &nbsp;&nbsp;&nbsp;
            <a onclick="showHide2('userGuess');"><img src="img/buttons2/button-user-guess.png" title="User Guess"/></a>
            <p>&nbsp;</p>
        </div>
        <div id="buttonsReports">
            <a href="#"><h4>Reports</h4></a>
            <a onclick="showHide2('report1');"><img src="img/buttons2/button-report.png" title="Report1"/></a>
            &nbsp;&nbsp;&nbsp;
            <a onclick="showHide2('report2');"><img src="img/buttons2/button-report.png" title="Report2"/></a>
            &nbsp;&nbsp;&nbsp;
            <a onclick="showHide2('report3');"><img src="img/buttons2/button-report.png" title="Report3"/></a>
            <p>&nbsp;</p>
        </div>--%>
    </div>

    <div id="actionsContainer" style="float:right;width:45%">
        <div id="adminActionsContainer">
          
          <div id="adminNew" class="">
              <h4>Season Management</h4>
                <table style="float:left;">
                <tr>
                    <td>Current Season ID:</td>
                    <td style="text-align:right;">
                        <input id="a8seasonID" name="a8seasonID" type="text" value="<%=currentSeason %>" style="width:85px;background:antiquewhite;" onclick="selectAll('a8seasonID');"/>
                    </td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                  <tr><td colspan="2" style="text-align:right;"><h6>Note: This <strong>will</strong> reset all users scores/rank.</h6></td></tr>
                <tr>
                    <td colspan="2" style="text-align:right;">
                        <input id="action8" name="action8" type="submit" value="Modify" class="button2" onclick="return press(8);" />
                      </td>
                    <td></td>
                </tr>
                </table>
            </div>

            <div id="adminScore" class="">
                <h4>Modify Game Score</h4>
                <table style="float:left;">
                <tr>
                    <td style="text-align:right;">Game ID:</td>
                    <td>
                        <select name="adminGameID" style="width:90px;background:antiquewhite;">
                            <% create_game_dropdown(); %>
                        </select>
                    </td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                  <tr>
                    <td style="text-align:right;">Season ID:</td>
                    <td>
                        <input id="adminSeasonID" name="adminSeasonID" type="text" value="<%=currentSeason %>" style="width:85px;background:antiquewhite;" onclick="selectAll('adminSeasonID');"/>
                    </td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                <tr>
                    <td style="text-align:right;">Team 1 Score:</td>
                    <td><input id="gameScoreTeam1" name="gameScoreTeam1" type="text" value="999" style="width:85px;background:antiquewhite;" onclick="selectAll('gameScoreTeam1');"/></td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                    <tr>
                    <td style="text-align:right;">Team 2 Score:</td>
                    <td><input id="gameScoreTeam2" name="gameScoreTeam2" type="text" value="999" style="width:85px;background:antiquewhite;" onclick="selectAll('gameScoreTeam2');"/></td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                    <tr><td colspan="2" style="text-align:right;"><h6>Note: This <strong>will</strong> recalc scores/rank.</h6></td></tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="text-align:right;"><input id="save2" name="action2" type="submit" value="Modify" class="button2" onclick="return press(2);" /></td>
                    <td>&nbsp;</td>
                </tr>
                </table>
            </div>

            <div id="adminGame" class="">
                <h4>Create/Modify Single Game</h4>
                <table style="float:left;">
                <tr>
                    <td style="text-align:right;">Game ID:</td>
                    <td style="text-align:right;">
                        <input id="a5gameID" name="a5gameID" type="text" value="1" style="width:85px;background:antiquewhite;" onclick="selectAll('a5gameID');"/>
                    </td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                  <tr>
                    <td style="text-align:right;">Season ID:</td>
                    <td style="text-align:right;">
                        <input id="a5seasonID" name="a5seasonID" type="text" value="<%=currentSeason %>" style="width:85px;background:antiquewhite;" onclick="selectAll('a5seasonID');"/>
                    </td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                <tr>
                    <td style="text-align:right;">Voting Open:</td>
                    <td style="text-align:right;">
                        <select name="a5isOpen" style="width:90px;background:antiquewhite;">
                            <option value="true" selected="selected">Open</option>
                            <option value="false">Closed</option> 
                        </select>
                    </td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                    <tr>
                    <td style="text-align:right;">Game Start Date:</td>
                    <td style="text-align:right;"><input id="a5gameStartDate" name="a5gameStartDate" type="text" value="08/20/2013" style="width:85px;background:antiquewhite;" onclick="selectAll('a5gameStartDate');"/></td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                <tr>
                    <td style="text-align:right;">Game Start Time:</td>
                    <td style="text-align:right;"><input id="defaultEntry" name="a5gameStartTime" type="text" value="12:00 PM" style="width:63px;background:antiquewhite;"/></td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                <tr>
                    <td style="text-align:right;">Team 1 ID:</td>
                    <td style="text-align:right;">
                        <select name="a5teamOneID" style="width:90px;background:antiquewhite;">
                            <% create_team1_dropdown(); %>
                        </select>
                    </td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                <tr>
                    <td style="text-align:right;">Team 2 ID:</td>
                    <td style="text-align:right;">
                        <select name="a5teamTwoID" style="width:90px;background:antiquewhite;">
                            <% create_team2_dropdown(); %>
                        </select>
                    </td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                <tr>
                    <td style="text-align:right;">Team 1 Score:</td>
                    <td style="text-align:right;"><input id="a5scoreOne" name="a5scoreOne" type="text" value="999" style="width:85px;background:antiquewhite;" onclick="selectAll('a5scoreOne');"/></td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                    <tr>
                    <td style="text-align:right;">Team 2 Score:</td>
                    <td style="text-align:right;"><input id="a5scoreTwo" name="a5scoreTwo" type="text" value="999" style="width:85px;background:antiquewhite;" onclick="selectAll('a5scoreTwo');"/></td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                <tr>
                    <td colspan="2">Game Description:</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:right;"><textarea id="a5gameDesc" name="a5gameDesc" cols="25" rows="5" style="background:antiquewhite;" onclick="selectAll('a5gameDesc');">UF vs World</textarea></td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                <tr>
                    <td colspan="2">Game Link:</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:right;"><input id="a5gameLink" name="a5gameLink" type="text" value="gatorzone.com" style="width:220px;background:antiquewhite;" onclick="selectAll('a5gameLink');"/></td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                <tr><td colspan="2" style="text-align:right;"><h6>Note: This <strong>will not</strong> recalc scores/rank.</h6></td></tr>
                <tr>
                    <td colspan="2" style="text-align:right;">
                        <input id="action5" name="action5" type="submit" value="Create" class="button2" onclick="return press(5);" />
                        <input id="action5b" name="action5" type="submit" value="Modify" class="button2" onclick="return press(5);" />
                    </td>
                    <td></td>
                </tr>
                </table>
               </div>

            <div id="adminTeam" class="">
                <h4>Create/Modify Single Team</h4>
                <table style="float:left;">
                    <tr>
                        <td style="text-align:right;">Team ID:</td>
                        <td style="text-align:right;"><input id="a6teamID" name="a6teamID" type="text" value="1" size="11" style="background:antiquewhite;" onclick="selectAll('a6teamID');"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">Team Name:</td>
                        <td style="text-align:right;"><input id="a6teamName" name="a6teamName" type="text" value="University Of Florida" style="background:antiquewhite;" size="11" onclick="selectAll('a6teamName');"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">Team Abbrev:</td>
                        <td style="text-align:right;"><input id="a6teamAbbrev" name="a6teamAbbrev" type="text" value="UF" size="11" style="background:antiquewhite;" onclick="selectAll('a6teamAbbrev');"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">Season Wins:</td>
                        <td style="text-align:right;"><input id="a6seasonWin" name="a6seasonWin" type="text" value="0" size="11" style="background:antiquewhite;" onclick="selectAll('a6seasonWin');"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td>Season Losses:</td>
                        <td style="text-align:right;"><input id="a6seasonLoss" name="a6seasonLoss" type="text" value="0" size="11" style="background:antiquewhite;" onclick="selectAll('a6seasonLoss');"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td colspan="2">Team Link:</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:right;"><input id="a6teamLink" name="a6teamLink" type="text" value="gatorzone.com" size="26" style="background:antiquewhite;" onclick="selectAll('a6teamLink');"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:right;">
                            <input id="action6" name="action6" type="submit" value="Create" class="button2" onclick="press(6)" />
                            <input id="action6b" name="action6" type="submit" value="Update" class="button2" onclick="press(6);" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div> 

            <div id="adminUser" class="">
                <h4>Create/Modify Single User</h4>
                <table style="float:left;">
                    <tr>
                        <td style="text-align:right;">User ID:</td>
                        <td style="text-align:right;"><input id="a7userID" name="a7userID" type="text" value="new.user" style="width:85px;background:antiquewhite;" onclick="selectAll('a7userID');"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">User Password:</td>
                        <td style="text-align:right;"><input id="a7password" name="a7password" type="text" value="password" style="width:85px;background:antiquewhite;" onclick="selectAll('a7password');"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">User Group:</td>
                        <td style="text-align:right;"><input id="a7groupID" name="a7groupID" type="text" value="1" style="width:85px;background:antiquewhite;" onclick="selectAll('a7groupID');" readonly="readonly" style="background-color:#a9a9a9;"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">User Role:</td>
                        <td style="text-align:right;"><input id="a7role" name="a7role" type="text" value="user" style="width:85px;background:antiquewhite;" onclick="selectAll('a7role');"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">Display Name:</td>
                        <td style="text-align:right;"><input id="a7displayName" name="a7displayName" type="text" value="New User" style="width:85px;background:antiquewhite;" onclick="selectAll('a7displayName');"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">Current Score:</td>
                        <td style="text-align:right;"><input id="a7currentScore" name="a7currentScore" type="text" value="0" style="width:85px;background:antiquewhite;" onclick="selectAll('a7currentScore');"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">Current Rank:</td>
                        <td style="text-align:right;"><input id="a7currentRank" name="a7currentRank" type="text" value="0" style="width:85px;background:antiquewhite;" onclick="selectAll('a7currentRank');"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">Spy Points:</td>
                        <td style="text-align:right;"><input id="a7spyPoints" name="a7spyPoints" type="text" value="0" style="width:85px;background:antiquewhite;" onclick="selectAll('a7spyPoints');"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">Team Prefrence:</td>
                        <td style="text-align:right;">
                            <select id="a7teamIDPref" name="a7teamIDPref" style="width:90px;background:antiquewhite;">
                                <% create_team1_dropdown(); %>
                            </select>
                        </td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td colspan="2">User Link:</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:right;"><input id="a7userLink" name="a7userLink" type="text" value="gatorzone.com" style="width:220px;background:antiquewhite;" onclick="selectAll('a7userLink');"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td colspan="2">Avatar Link:</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:right;"><input id="a7avatarLink" name="a7avatarLink" type="text" value="gatorzone.com" style="width:220px;background:antiquewhite;" onclick="selectAll('a7avatarLink');"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:right;">
                            <input id="action7" name="action7" type="submit" value="Create" class="button2" onclick="press(7);" />
                            <input id="action7b" name="action7" type="submit" value="Update" class="button2" onclick="press(7);" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="userActionsContainer">
            <div id="userProfile" class="">
                <h4>Your Profile</h4>
                <table style="float:left;">
                    <tr>
                        <td style="text-align:right;">Display Name:</td>
                        <td><input id="a4displayName" name="a4displayName" type="text" value="<%=cDisplayName %>" size="11" style="background:antiquewhite;" onclick="selectAll('a4displayName');"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Your Display Name appears for all other users."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">User ID:</td>
                        <td><input id="a4userID" name="a4userID" type="text" value="<%=cUserID %>" size="11" onclick="selectAll('a4userID');" readonly="readonly" style="background-color:#a9a9a9;"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Same as gatorlink (not displayed)."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <%--<tr>
                        <td style="text-align:right;">User Password:</td>
                        <td><input id="a4password" name="a4password" type="text" value="<%=cPassword %>" size="11" onclick="selectAll('a4password');" readonly="readonly" style="background-color:#a9a9a9;"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Currently not used (same as gatorlink)."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>--%>
                    <tr>
                        <td style="text-align:right;">User Group:</td>
                        <td><input id="a4groupID" name="a4groupID" type="text" value="<%=cGroupID %>" size="11" onclick="selectAll('a4groupID');" readonly="readonly" style="background-color:#a9a9a9;"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Currently not used."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">User Role:</td>
                        <td><input id="a4role" name="a4role" type="text" value="<%=cRole %>" size="11" onclick="selectAll('a4role');" readonly="readonly" style="background-color:#a9a9a9;"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Your role is used to determine what privileges you have."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">Team Prefrence:</td>
                        <td >
                            <select id="a4teamIDPref" name="a4teamIDPref" readonly="readonly" style="background-color:#a9a9a9;width:90px;">
                                <% create_team_conditional_dropdown(); %>
                            </select>
                        </td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Your Team Preference is the team you are rooting for (currently you cannot change this, but who would)."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">Current Score:</td>
                        <td><input id="a4currentScore" name="a4currentScore" type="text" value="<%=cCurrentScore %>" size="11" onclick="selectAll('a4currentScore');" readonly="readonly" style="background-color:#a9a9a9;"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Your current individual score."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">Current Rank:</td>
                        <td><input id="a4currentRank" name="a4currentRank" type="text" value="<%=cCurrentRank %>" size="11" onclick="selectAll('a4currentRank');" readonly="readonly" style="background-color:#a9a9a9;"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Your current rank among all users."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <%--<tr>
                        <td style="text-align:right;">Spy Points:</td>
                        <td><input id="c4spyPoints" name="a4spyPoints" type="text" value="<%=cSpyPoints %>" size="11" onclick="selectAll('a4spyPoints');" readonly="readonly" style="background-color:#a9a9a9;"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Currently Not Used."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>--%>
                    <tr>
                        <td style="text-align:right;">Date Joined:</td>
                        <td><input id="a4dateJoined" name="a4dateJoined" type="text" value="<%=cDateJoined %>" size="11" onclick="selectAll('a4dateJoined');" readonly="readonly" style="background-color:#a9a9a9;"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Date and time you joined this system."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">Last Login:</td>
                        <td><input id="c4lastLogin" name="a4lastLogin" type="text" value="<%=cLastLogin %>" size="11" onclick="selectAll('a4lastLogin');" readonly="readonly" style="background-color:#a9a9a9;"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Date and time you last logged into this system."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td colspan="2">User Link:</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:right;"><input id="a4userLink" name="a4userLink" type="text" value="<%=cUserLink %>" size="28" style="background:antiquewhite;" onclick="selectAll('a4userLink');"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Currently Not Used."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td colspan="2">Avatar Link:</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:right;"><input id="a4avatarLink" name="a4avatarLink" type="text" value="<%=cAvatarLink %>" size="28" style="background:antiquewhite;" onclick="selectAll('a4avatarLink');"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Currently Not Used."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:right;">
                            <input id="action4" name="action4" type="submit" value="Update" class="button2" onclick="press(4);" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>

            <div id="userGuess" class="">
                <h3>Your Guess</h3>
                <table style="float:left;">
                    <tr>
                        <td style="text-align:right;">Game ID:</td>
                        <td>
                            <select name="gameID" style="width:150px;background:antiquewhite;">
                              <option onclick="getUserGuess('');" selected="selected">Select A Game</option>
                                <% create_game_dropdown(); %>
                            </select>
                        </td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="Select the game you wish to vote on."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                 <%-- <tr>
                    <td>Season ID:</td>
                    <td style="text-align:right;">
                        <input id="a5seasonID" name="a5seasonID" type="text" value="<%=currentSeason %>" style="width:85px;" onclick="selectAll('a5seasonID');"/>
                    </td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>--%>
                    <tr>
                        <td style="text-align:right;">Guess:</td>
                        <td><input id="guess" name="guess" type="text" value="" style="width:145px;background:antiquewhite;" onclick="selectAll('guess');"/></td>
                        <td><a href="javascript:void(0)" class="lytetip" data-tip="What score do you think your team will win by? Put a negative number if you expect a loss."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td><input id="save" name="action3" type="submit" value="Save" class="button2" onclick="return press(3);" /></td>
                        <td>&nbsp;</td>
                    </tr>
                  

                </table>
            </div>
          
          <div id="userSpy" class="">
              <h4>Spy Points Management</h4>
                <table style="float:left;">
                <tr>
                    <td>Spy On:</td>
                    <td style="text-align:right;">
                        <input id="a8seasonID" name="a8seasonID" type="text" value="" style="width:85px;background:antiquewhite;" onclick="selectAll('a8seasonID');"/>
                    </td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Tooltip: Help coming soon!"><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:right;">
                        <input id="action8" name="action8" type="submit" value="Commit" class="button2" onclick="return press(8);" />
                      </td>
                    <td></td>
                </tr>
                </table>
            </div>
          
          <div id="userMessage" class="">
              <h4>Message Management</h4>
                <table style="float:left;">
                <tr>
                    <td style="text-align:right;">Send Message To:</td>
                    <td >
                        <select id="a9toUserID" name="a9toUserID" style="background-color:antiquewhite;width:90px;">
                            <% create_user_dropdown(); %>
                        </select>
                    </td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Chose who to send a message to."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                <tr>
                    <td colspan="2" >Message Body:</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:right;">
                        <textarea id="a9messageContent" name="a9messageContent" cols="25" rows="5" style="background:antiquewhite;" onclick="selectAll('a9messageContent');"></textarea>
                    </td>
                    <td><a href="javascript:void(0)" class="lytetip" data-tip="Messsage Content: Type your message here. It may only contain 400 characters."><img border="0" alt="help..." src="img/help_19x19.png"/></a></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:right;">
                        <input id="action9" name="action8" type="submit" value="Send" class="button2" onclick="return press(9);" />
                      </td>
                    <td></td>
                </tr>
                </table>
            </div>

        </div>
    </div>
    
    <div style="clear:both;"></div>
    <hr/>
    <p>&nbsp;</p>

    <div id="reportsContainer">
        <div id="report1" class="">
            <h3><a href="javascript:void(0)" class="lytetip" data-tip="This report is your personal scoreboard."><%=cDisplayName %>'s Scoreboard</a></h3>
            <% write_scoreboard();%>
            <%--<p>&nbsp;</p>--%>
        </div>
      
        <div id="report2" class="">
            <h3><a href="javascript:void(0)" class="lytetip" data-tip="This report details all the games history">Game History</a></h3>
            <% write_gameboard(); %>
            <%--<p>&nbsp;</p>--%>
        </div>
    
        <div id="report3" class="">
            <h3><a href="javascript:void(0)" class="lytetip" data-tip="This report shows current placement of all users">Global Scoreboard</a></h3>
            <% write_global_scoreboard(); %>
            <%--<p>&nbsp;</p>--%>
        </div>
      
        <div id="report4" class="">
            <h3><a href="javascript:void(0)" class="lytetip" data-tip="This report displays all user guesses for all the games">Global Guessboard</a></h3>
            <% write_guessboard(); %>
            <p>&nbsp;</p>
        </div>
    </div>

</asp:Content>

<asp:Content ID="debugger" ContentPlaceHolderID="debug" Runat="Server">
    <%--<br/><p><a href="#adminTeam" rel="ibox" title="Test Box" >Test PopOut</a><br />
    <br/><p><a onclick="changeToMobile();" href="#" title="Mobile Version" >Mobile?</a><br />
    <input id="Submit00" name="action00" type="submit" value="Delete Self" onclick="press('00');" /><br/>
    <input id="Submit000" name="action000" type="submit" value="Add As Admin" onclick="press('000');" /></p><br/>
    <input id="viewSeasonID" name="viewSeasonID" type="text" value="1" style="width:15px;" onclick="selectAll('viewSeasonID');"/>
    <input id="Submit9" name="action9" type="button" value="Change Season" onclick="toServer('1','1');" /></p><br/>--%>
    <p><%=debug %></p>
</asp:Content>
