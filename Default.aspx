<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="page_title" Runat="Server">
	UF Training App | Authenticate
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_name_info" Runat="Server">
	<div id="header_title">
		Authentication Page
	</div>
	<div id="header_subtext">
		If you are seeing this page, you need to Authenticate.
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_content" Runat="Server">
	<script type="text/javascript">
	  function starting() {
	    //do nothing but keep this here
	  }
	  function showHide(id) {
	    var e = document.getElementById(id);
	    if (e.style.display == 'block')
	      e.style.display = 'none';
	    else
	      e.style.display = 'block';
	  }
	  function show(id) {
	    var e = document.getElementById(id);
	    e.style.display = 'block';
	  }
	  function hide(id) {
	    var e = document.getElementById(id);
	    e.style.display = 'none';
	  }
	  //2do: put in js file
	  function press1() {
	    // Clear the hidden value this data
	    document.getElementById('item_action').value = 'action1';
	  }
	  function press2() {
	    // Clear the hidden value this data
	    document.getElementById('item_action').value = 'action2';
	  }
	  function press3() {
	    // Clear the hidden value this data
	    document.getElementById('item_action').value = 'action3';
	  }
	</script>
	<input type="hidden" id="item_action" name="item_action" value="" />
	<div id="content_text">
		<div id="header_content_left">
			<% optionsMessageWriter(); %>
		</div>
		<div id="header_content_right_messages">
			<p>&nbsp;</p>
			<p>&nbsp;</p>
			<p>&nbsp;</p>
            <div id="registerUFAD" style="display:none;"> 
				<table>
					<tr>
						<td>Username:</td>
						<td><input id="userID_UFAD" name="userID" type="text" value="<%= suggestedUserID %>" /></td>
					</tr>
					<tr>
						<td><p></p></td>
						<td><input id="register_UFAD" type="submit" value="Register" onclick="press2();" /></td>
					</tr>
				</table>
			</div>
			<div id="manualLogin" style="display:none;"> 
				<!--
				<table>
					<tr>
						<td>Username:</td>
						<td><input id="userID_manual" name="userID_manual" type="text" value="<%= suggestedUserID %>" /></td>
					</tr>
					<tr>
						<td>Password:</td>
						<td><input id="password_manual" name="password_manual" type="password" value="Password" /></td>
					</tr>
					<tr>
						<td><p></p></td>
						<td><input id="login_manual" type="submit" value="Login" onclick="press1();" /></td>
					</tr>
				</table>
				-->
				<p>Currently Not Used.</p>
			</div>
			<div id="registration" style="display:none;">
				<!-- 
				<table>
					<tr>
						<td>Username:</td>
						<td><input id="userID_standard" name="userID" type="text" value="<%= suggestedUserID %>" /></td>
					</tr>
					<tr>
						<td>Password:</td>
						<td><input id="password_standard" name="password" type="text" value="Password" /></td>
					</tr>
					<tr>
						<td><p></p></td>
						<td><input id="register_standard" type="submit" value="Register" onclick="press3();" /></td>
					</tr>
				</table>
				-->
				<p>Currently Not Used.</p>
			</div>
		</div>
	</div>
</asp:Content>

