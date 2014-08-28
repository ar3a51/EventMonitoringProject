<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


      <div id="divSideBar" class="divSideBar">
      <h2>Navigation</h2>
           <ul>
            <li><span class="linkItem"><%=Html.ActionLink("Home","Index") %></span></li>
            <li><span class="linkItem"><%=Html.ActionLink("Register New User", "RegisterNewUser")%></span></li>
            <li><span class="linkItem"><%=Html.ActionLink("Modify Settings", "editUser")%></span></li>
            </ul>
      </div>


      <!--

<table id="sidebar" class="sidebar" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td><h3>Navigation</h3></td>
    </tr>
    <tr>
        <td><%=Html.ActionLink("Home","Index") %></td>
    </tr>
    <tr>
        <td><%=Html.ActionLink("Register New User", "RegisterNewUser")%></td>
    </tr>
    <tr>
        <td><%=Html.ActionLink("Modify Settings", "editUser")%></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>
-->


