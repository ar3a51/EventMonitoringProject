<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/EventMonitorAdministrator.Master" Inherits="System.Web.Mvc.ViewPage<EventMonitoringAdmin.UserManagement.UserDetails>" %>


<asp:Content ID="ContentHead" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>
<asp:Content ID="ContentScript" ContentPlaceHolderID="Script" runat="server">
<script src="<%= Url.Content("~/Scripts/Dictionary.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            getLogList($("#machineCollection").val());
            getSuppressedList();

            $("#btnSubmit").click(function () {
                getLogList($("#machineCollection").val());
            });

            $("#btnSuppressed").click(function () {

                var logCollection = retrieveSelected("#divResult");
                //alert(logCollection[0].EventId);
                addSuppressedList("update", logCollection);
            });

            $("#btnModify").click(function () {

                var logCollection = retrieveSelected("#divSuppressedLog");
                deleteList(logCollection);
            });

        });

        function attachLinkHandler(sDomPath) {

            $(sDomPath).click(function (event) {
                event.preventDefault();

                var guid = $(this).attr("href");

                ajaxSend("/EventMonitoringAdmin/Event/search/" + guid,
                        "jquery",
                        false,
                        null,
                        function (jhqr, result) {
                            if (result == "success") {
                                $("#hdnInfo").html(jhqr);
                                $("#hdnInfo").dialog({
                                    autoOpen: true,
                                    show: "slide",
                                    hide: "explode",
                                    height: "600",
                                    width: "800",
                                    modal: true,
                                    beforeClose: function (event, ui) {

                                        $(this).html("");
                                    }
                                });
                            }


                        });

            });
        
        }

        function getLogList(machineName) {

            ajaxSend("/EventMonitoringAdmin/Event/geteventList?machineName=" + machineName,
                     "html",
                      false,
                      null,
                      function (jhqr, result) {

                          if (result == "success") {
                              $("#divResult").html(jhqr);
                              attachLinkHandler("#divResult Table tr td a");
                          }
                          else {
                              $("#idNotification").html(jhqr);
                              $("#idNotification").dialog("open");
                          }

                      });

         }

         function getSuppressedList(username) {

             ajaxSend("/EventMonitoringAdmin/user/getSuppressedLog",
                     "html",
                      false,
                      null,
                      function (jhqr, result) {

                          if (result == "success")
                              $("#divSuppressedLog").html(jhqr);
                          else {
                              $("#idNotification").html(jhqr);
                              $("#idNotification").dialog("open");
                          }

                      });

         }

        function addSuppressedList(action, logList) {



            ajaxSend("/EventMonitoringAdmin/User/saveSuppressedLog",
                     $.toDictionary({ operation: action, log: logList }),
                     "json",
                     true,
                     function (result) {

                         //alert(result);



                     },
                     function (jhqr, result) {
                         // alert(jhqr.responseText);
                         $("#divSuppressedLog").html(jhqr.responseText);
                     }
             );


         }

         function deleteList(logList) {

             ajaxSend("/EventMonitoringAdmin/User/deleteLog",
                     $.toDictionary(logList),
                     "json",
                     true,
                     function (result) {

                         //alert(result);



                     },
                     function (jhqr, result) {
                         // alert(jhqr.responseText);
                         //if (result == "success") {

                         //}
                        // if (jhqr.status == 200) {
                            $("#divSuppressedLog").html(jhqr.responseText);
                            /* for (var i = 0; i < logList.length; i++) {


                                 $("#divSuppressedLog table").find("tr #" + logList[i].eventId);
                             }*/
                        // }


                     }
             );

         }

         function retrieveSelected(divId) {
             var innerHtml = $(divId + " table tr :checked");
             var LogArray = new Array(innerHtml.size());
             var intCounter = 0;
             innerHtml.each(function () {
                 //alert($(this).val());

                 var eventNo = $(this).val();
                 var eventId = $(divId + " tr#" + eventNo + " td")[1];
                 var source = $(divId +" tr#" + eventNo + " td")[2];
                 var Username;

                 if ($(divId + "tr#" + eventNo + " td")[3] != null) {
                     Username = $(divId + "tr#" + eventNo + " td")[3].innerHTML;
                 }
                 else
                     Username = "";


                 var log = {
                     eventId: eventId.innerHTML,
                     source: source.innerHTML,
                     dtGenerated: "",
                     chUsername: Username,
                     type: ""
                 };

                 LogArray[intCounter] = log;
                 intCounter++;
             });

             return LogArray;
             //alert(innerHtml.size()); 

         }

    </script>
</asp:Content>
<asp:Content ID="ContentBodyHeader" ContentPlaceHolderID="HeaderPane" runat="server">
    <h1>Event Monitoring Management System</h1>
</asp:Content>
<asp:Content ID="ContentLeftPane" ContentPlaceHolderID="LeftPane" runat="server">
    <uc:SidebarUserControl ID="sidebarUc" runat="server" />
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="server">
    Choose machine:
    <select id="machineCollection" name="machineCollection">
        <% foreach(String machineName in Model.machines){ %>
            <option value="<%= machineName %>"><%= machineName %></option>
        <% } %>
    </select>
    <input type="button" id="btnSubmit" value="Search" />
    <div id="divResult" style="position:relative;
                               margin-top:40px;
                               border-width:1px; 
                               border-style:solid; 
                               width:800px; 
                               height:300px;
                               overflow:auto;">
   
    </div>

    <input type="button" id="btnSuppressed" value="Suppress" />

    <div id="divSuppressedLog" style="position:relative;
                                   margin-top:40px;
                                   border-width:1px;
                                   border-style:solid;
                                   width:800px;
                                   height:300px;
                                   overflow:auto;">
                                   
     </div>
     <input type="button" id="btnModify" value="Delete" /> 
     
     <div id="hdnInfo" title="Log Details" style="Display: none; width:800px; height:600px;"></div>                                      

</asp:Content>
