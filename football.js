var popups = []; //stores div popups
var standardVersion = true; //used to tell if we should use the inline popups or not

function changeToMobile() {
  standardVersion = false;
}

//close all the actions
function init() {
 
  document.getElementById("blanket").addEventListener("click", function () {
    showHide("popups");
    showHide("blanket");
  }, false);
  
  try {
    de("adding popup div ids...");
    //2do make this iterate trhough divs to find them automatically
    popups[0] = { name: "adminNew", width: 0, height: 0 };
    popups[1] = { name: "adminScore", width: 0, height: 0 };
    popups[2] = { name: "adminGame", width: 0, height: 0 };
    popups[3] = { name: "adminTeam", width: 0, height: 0 };
    popups[4] = { name: "adminUser", width: 0, height: 0 };
    popups[5] = { name: "userProfile", width: 0, height: 0 };
    popups[6] = { name: "userGuess", width: 0, height: 0 };
    popups[7] = { name: "userSpy", width: 0, height: 0 };
    popups[8] = { name: "userMessage", width: 0, height: 0 };
    popups[9] = { name: "report1", width: 0, height: 0 };
    popups[10] = { name: "report2", width: 0, height: 0 };
    popups[11] = { name: "report3", width: 0, height: 0 };
    popups[12] = { name: "report4", width: 0, height: 0 };

    for (var i = 0; i < popups.length; i++) {
      //assign popup information 
      de("assigning popup info to: " + popups[i].name);
      de("width: " + document.getElementById(popups[i].name).offsetWidth);
      de("height: " + document.getElementById(popups[i].name).scrollHeight);
      popups[i].width = document.getElementById(popups[i].name).offsetWidth;
      popups[i].height = document.getElementById(popups[i].name).scrollHeight;
      //visually close the popup
      de("closing popup");
      document.getElementById(popups[i].name).style.display = "none";
      //assign visual class for popout
      if (standardVersion == true) {
        de("standardVersion: " + standardVersion);
        document.getElementById(popups[i].name).className = document.getElementById(popups[i].name).className + " popup"; //note the space
      }
    }
  }catch (e)
  {
    //do nothing
    de("no popups detected, changing to alternative version");
    standardVersion = false;
  }
  
  //close loading
  document.getElementById("blanket_loading").style.display = "none";

}

function popUp(id) {
  var popUpBox = document.getElementById(id);

  popUpBox.style.position = "fixed";
  popUpBox.style.display = "block";

  popUpBox.style.zIndex = "6";

  popUpBox.style.top = "50%";
  popUpBox.style.left = "50%";

  var height = popUpBox.offsetHeight;
  var width = popUpBox.offsetWidth;
  var marginTop = (height / 2) * -1;
  var marginLeft = (width / 2) * -1;

  popUpBox.style.marginTop = marginTop + "px";
  popUpBox.style.marginLeft = marginLeft + "px";
  
  //show blanket
  document.getElementById("blanket").style.display = "block"; 

}

function press(actionNumber) {
  // Clear the hidden value this data
  document.getElementById('item_action').value = 'action' + actionNumber.toString();
}

function getUserGuess(selectedGameGuess) {
  de("selected Game: " + selectedGameGuess);
  if (selectedGameGuess == null) {
    document.getElementById("guess").value = 0;
  } else {
    document.getElementById("guess").value = selectedGameGuess;
  }
  
}

function closeAllPopups() {
  //close all other buttons
  adminNew.style.display = "none";
  adminGame.style.display = "none";
  adminUser.style.display = "none";
  adminTeam.style.display = "none";
  adminScore.style.display = "none";
  userProfile.style.display = "none";
  userGuess.style.display = "none";
  userSpy.style.display = "none";
  userMessage.style.display = "none";
  report1.style.display = "none";
  report2.style.display = "none";
  report3.style.display = "none";
  report4.style.display = "none";
}

function showHide(id) {
  
  if (id == "popups") {
    closeAllPopups();
  } else {
    var e = document.getElementById(id);
    if (e.style.display == 'block')
      e.style.display = 'none';
    else
      e.style.display = 'block';
  }

  
}

function showHide2(id) {
  var e = document.getElementById(id);
  if (e.style.display == 'block') {
    de("closing " + id);
    e.style.display = 'none';
  }
  else {
    
    //opening div
    de("opening " + id);
    
    if (standardVersion == true) {

      closeAllPopups();
      popUp(id);
      
      //does not work
      ////get the unique xy for the div
      //var temp1;
      //var temp2;
      //for (var i = 0; i < popups.length; i++) {
      //  if (popups[i].name == id) {
      //    de(popups[i].name + " attributes:");
      //    temp1 = popups[i].width;
      //    de("width: " + temp1);
      //    temp2 = popups[i].height;
      //    de("height: " + temp2);
      //  }
      //}
      //e.style.marginLeft = "-" + Math.round(temp1 / 2) + "px";
      //de("marginLeft: " + "-" + Math.round(temp1 / 2) + "px");
      //e.style.marginTop = "-" + Math.round(temp2 / 2) + "px";
      //de("marginTop: " + "-" + Math.round(temp2 / 2) + "px");

    }
    e.style.display = 'block';
  }
}

//imported functions
var RIBMode = false;                    //holds a marker for running in background mode (do not display messages)
var debugMode = false;                  //holds debug marker
var messageCount = 0;                   //holds the running count of all the messages written in a session
var debugs = 0;

//display an inline message
function displayMessage(message) {

  //debug log this message
  de("message #" + messageCount + ": " + message); //send to debugger for logging

  //keep a count of messages
  messageCount++;

  //check to see if RIB is on
  if (RIBMode == true) {
    de("RIB Mode: " + RIBMode);
    return;
  } else {
    //display the message

    //debug
    de("RIB Mode: " + RIBMode);

    //compile divID
    var currentDivId = "message" + messageCount;

    //create unique message div
    var messageDiv = document.createElement("div");
    messageDiv.setAttribute("id", currentDivId);
    messageDiv.className = "message";
    document.getElementById("message_content").appendChild(messageDiv);

    //assign the message
    document.getElementById(currentDivId).innerHTML = message;

    //show message
    document.getElementById(currentDivId).style.display = "block"; //display element

    //fade message out
    setTimeout(function () {
      $("#" + currentDivId).fadeOut("slow", function () {
        $("#" + currentDivId).remove();
      });
    }, 3000); //after 3 sec
  }
}

//keypress shortcuts/actions
window.onkeyup = keypress;
function keypress(e) {
  var keycode = null;
  if (window.event) {
    keycode = window.event.keyCode;
  } else if (e) {
    keycode = e.which;
  }
  de("key up: " + keycode);
  switch (keycode) {
    case 13: //enter
      //nothing yet
      break;
    case 27: //esc
      closeAllPopups();
      document.getElementById("blanket").style.display = "none";
      document.getElementById("loading_blanket").style.display = "none";
      break;
    case 17: //ctrl
      if (isCntrlDown == false) {
        isCntrlDown = true;
      } else {
        isCntrlDown = false;
      }
      break;
    case 68: //D (for debuggin)
      if (isCntrlDown == true) {
        isCntrlDown = false;
        debugs++;
        if (debugs % 2 == 0) {
          document.getElementById("debug_container").style.display = "none";
          debugMode = false;
          displayMessage("Debug Mode Off");
        } else {
          document.getElementById("debug_container").style.display = "block";
          debugMode = true;
          displayMessage("Debug Mode On");
        }
      }
      break;
  }
}

//debugging 
var debugStringDefault = "<strong>Debug Panel:</strong> <a onclick=\"debugClear()\">(clear)</a><br><br>";
var debugString = debugStringDefault;
var debugs = 0; //used for keycode debugging
var isCntrlDown = false;

function de(message) {
  //create debug string
  var currentdate = new Date();
  var time = currentdate.getHours() + ":" + currentdate.getMinutes() + ":" + currentdate.getSeconds() + ":" + currentdate.getMilliseconds();
  debugString += "[" + time + "] " + message + "<br><hr>";
  document.getElementById("debugs").innerHTML = debugString;
}
function debugClear() {
  debugString = debugStringDefault;
  document.getElementById("debugs").innerHTML = debugString;

}

//sends save dataPackages to the server via json
function toServer(dataPackage, data2) {
  $.ajax({
    type: "POST",
    url: "dashboard.aspx/SaveItem",
    data: JSON.stringify({ sendData: dataPackage, data2: data2}),
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (result) {
      de("server result:" + result);
      displayMessage("Saved");
    }
  });
}
