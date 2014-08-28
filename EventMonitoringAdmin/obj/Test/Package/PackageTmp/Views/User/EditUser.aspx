<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/EventMonitorAdministrator.Master" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditUser
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <table id="tblDetail">
        <tr>
            <td>Username:</td>
            <td><input type="text" id="txtUsername" name="txtUsername" value="<%=ViewData["myDetails"] %>" /></td>
        </tr>
        <tr>
            <td>First Name:</td>
            <td><input type="text" id="txtFName" name="txtFName" /></td>
        </tr>
        <tr>
            <td>Last Name:</td>
            <td><input type="text" id="txtLName" name="txtLName" /></td>
        </tr>
        <tr>
            <td>Primary E-mail Address:</td>
            <td><input type="text" id="txtEmailAddress" /></td>
        </tr>
         <tr>
            <td>Secondary E-mail Address:</td>
            <td><input type="text" id="txtEmailAddress2" /></td>
        </tr>
         <tr>
            <td>Server to monitor:</td>
            <td><%= Html.ListBox("selectMachine", (List<SelectListItem>)ViewData["selections"])%></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
         <tr>
            <td><input type="submit" value="Submit" id="btnSubmit" />&nbsp;<input type="reset" value="Reset" id="btnReset" /></td>
            
        </tr>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
         $( "input:submit, input:reset" ).button();

            $( "#idNotification" ).dialog({
			autoOpen: false,
			show: "slide",
			hide: "explode",
            modal: true
		});

           searchUser($("#txtUsername").val());
            


            /*$("#btnSearch").click(function () {
                

            });*/

            $("#btnSubmit").click(function () {
                submitData();

            });


        });
        function searchUser(userName) 
        {
            var data = getDetail();

            ajaxSend("/EventMonitoringAdmin/user/getUser",
                     data,
                     "json",
                     true,
                     function (result) {
                        
                        if(result.sFirstName == ""){
                             $("#idNotification").html("you are not registered with the system");
                             $("#idNotification").dialog("open");
                         }

                         $("#txtFName").val(result.sFirstname);
                         $("#txtLName").val(result.sLastname);
                         $("#txtEmailAddress").val(result.sPrimaryEmail);
                         $("#txtEmailAddress2").val(result.sSecondaryEmail);
                         $("#selectMachine").val(result.machines);
                     },
                     function(jhqr,result){
                     
                        //alert(result);
                        if (jhqr.statusText != "success")
                        { 
                            $("#idNotification").html(jhqr.responseText);
                            $("#idNotification").dialog("open");
                        }
                     
                     });


         }

         function submitData() {
            if(!validateEntry())
                return;

             var data = getDetail();

             $("#idNotification").html("<img src='<%= VirtualPathUtility.ToAbsolute("~/Theme/Images/ajax-loader.gif")%>' id='image' />");
             $("#idNotification").dialog("open");

             ajaxSend(
                    "/EventMonitoringAdmin/user/modify",
                    data,
                    "json",
                    true,
                    function (result) {
                        //alert(result.stringResultType);
                        $("#idNotification").html(result.stringResultMessage).find("img").addClass("image");
                        //$("#idNotification").dialog();
                        
                    });
         }

          function getDetail() {
                     var userName = $("#txtUsername").val();
                     var firstName = $("#txtFName").val();
                     var lastName = $("#txtLName").val();
                     var primaryEmail = $("#txtEmailAddress").val();
                     var secondaryEmail = $("#txtEmailAddress2").val();
                     var machineCollections = $("#selectMachine").val();


                     return {
                         userName: userName,
                         firstName: firstName,
                         lastName: lastName,
                         primaryEmail: primaryEmail,
                         secondaryEmail: secondaryEmail,
                         machineCollections: machineCollections
                     };

                 }

        function validateEntry()
        {
            var errorMsg = "";

            if($("#txtFName").val() == "")
            {
                 /*$("#idNotification").html("Please enter your secondary e-mail address.");
                  $("#idNotification").dialog("open");
                  return false;*/
                  errorMsg += "<li>Please enter your First name.</li>"
            }

             if($("#txtLName").val() == "")
            {
                 /*$("#idNotification").html("Please enter your secondary e-mail address.");
                  $("#idNotification").dialog("open");
                  return false;*/
                  errorMsg += "<li>Please enter your last name.</li>"
            }

            if($("#txtEmailAddress").val() == "")
            {
                 /*$("#idNotification").html("Please enter your secondary e-mail address.");
                  $("#idNotification").dialog("open");
                  return false;*/
                  errorMsg += "<li>Please enter your primary e-mail address.</li>"
            }

            if($("#txtEmailAddress2").val() == "")
            {
                 
                  
                  errorMsg += "<li>Please enter your secondary e-mail address.</li>"
            }

            if (errorMsg != "")
            {

                  $("#idNotification").html("The following needs to be fixed:<ul>" + errorMsg + "</li>");
                  $("#idNotification").dialog("open");
                  return false;
                
            }
            else
                return true;

        }
    
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeaderPane" runat="server">
        <h1>Edit the current user</h1>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="LeftPane" runat="server">
    <uc:SidebarUserControl ID="sidebarUc" runat="server" />
</asp:Content>
