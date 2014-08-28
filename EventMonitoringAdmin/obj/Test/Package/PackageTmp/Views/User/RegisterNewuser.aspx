<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/EventMonitorAdministrator.Master" Inherits="System.Web.Mvc.ViewPage" %>




<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Register New user
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    

    <% //Html.BeginForm("add", "user"); %>
    <table id="tblDetail">
        <tr>
            <td>Username:</td>
            <td><input type="text" id="txtUsername" readonly="readonly" name="txtUsername" value="<%=ViewData["username"] %>" /></td>
        </tr>
        <tr>
            <td>First Name:</td>
            <td><input type="text" id="txtFName" readonly="readonly" name="txtFName" value="<%=ViewData["firstName"] %>" /></td>
        </tr>
        <tr>
            <td>Last Name:</td>
            <td><input type="text" id="txtLName" readonly="readonly" name="txtLName" value="<%=ViewData["lastName"] %>" /></td>
        </tr>
        <tr>
            <td>Primary E-mail Address:</td>
            <td><input type="text" id="txtEmailAddress" readonly="readonly" value="<%=ViewData["mail"] %>" /></td>
        </tr>
         <tr>
            <td>Secondary E-mail Address:</td>
            <td><input type="text" id="txtEmailAddress2" /></td>
        </tr>
         <tr>
            <td>Server to monitor:</td>
            <td>
                <%=Html.ListBox("selectMachine", (List<SelectListItem>)ViewData["selections"])%>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
         <tr>
            <td><input type="submit" value="Submit" id="btnSubmit" />&nbsp;<input type="reset" value="Reset" id="btnReset" /></td>
            
        </tr>
    </table>
    <% //Html.EndForm(); %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $( "input:submit, input:reset" ).button();
            
            $("#idNotification").dialog({
                autoOpen: false,
                show: "slide",
                hide: "explode",
                modal: true
            });


            $("#btnSubmit").click(function () {

               if(!validateEntry())
                 return;

                var user = getDetail();

                  $("#idNotification").html("<img src='<%= VirtualPathUtility.ToAbsolute("~/Theme/Images/ajax-loader.gif") %>' id='image' />");
                  $("#idNotification").dialog("open");

                ajaxSend("/EventMonitoringAdmin/user/add",
                         user,
                         "json",
                         true,
                         function (data) {

                             //alert(data.stringResultType);
                             $("#idNotification").html(data.stringResultMessage).find("img").addClass("image");

                         });

            });
          });

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
                  errorMsg += "<li>Please enter your Firstname.</li>"
            }

             if($("#txtLName").val() == "")
            {
                 /*$("#idNotification").html("Please enter your secondary e-mail address.");
                  $("#idNotification").dialog("open");
                  return false;*/
                  errorMsg += "<li>Please enter your primary e-mail address.</li>"
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

                  $("#idNotification").html("The following needs to be fixed:<ul>" + errorMsg + "</ul>");
                  $("#idNotification").dialog("open");
                  return false;
                
            }
            else
                return true;
        }

    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeaderPane" runat="server">
    <h1>Registering New User</h1>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="LeftPane" runat="server">
    <uc:SidebarUserControl ID="sidebarUc" runat="server" />
</asp:Content>
