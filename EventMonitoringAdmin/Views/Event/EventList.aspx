<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Data.DataTable>" %>


    <table style="width:100%;" border="1" cellpadding="0" cellspacing="0">
    <tr>
        <th>Select</th>
        <% foreach(System.Data.DataColumn column in Model.Columns) { %>
           <th><%= column.ColumnName.ToString()  %></th> 

        <% } %>
    </tr>
    <%  
        int iCounter = 0;
        foreach(System.Data.DataRow row in Model.Rows) { 
    %>
        <tr id="<%=iCounter.ToString() %>">
        <td><input type="checkbox" id="chkBox" value="<%=iCounter.ToString() %>"/></td>
        <% foreach(System.Data.DataColumn column in Model.Columns){ %>
            <td id="<%=column.ColumnName %>"><%= row[column.ColumnName].ToString() %></td>
        <%} %>
        </tr>
     <% iCounter++; } %>
     </table>
